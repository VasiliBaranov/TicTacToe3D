using System;
using System.Collections.Generic;
using TicTacToe3D.GameInfo;
using TicTacToe3D.AI;
using TicTacToe3D.GameServer.Events;
using TicTacToe3D.GameServer.GameMasterSteps;
using TicTacToe3D.GameServer.Interfaces;

namespace TicTacToe3D.GameServer.GameMasterSteps
{
    public class TurnStep : NotTerminatingGameMasterStep
    {
        #region Fields

        private List<IGameObserver> _gameObserversThatConfirmedCurrentTurn;
        private CyclicEnumerator<PlayerInformation> _cyclicPlayersEnumerator;

        #endregion

        #region Constructors

        public TurnStep(ExtendedGameInfo extendedGameInfo)
            : base(extendedGameInfo)
        {

        }

        #endregion

        #region IGameMasterStep Members

        public override void StartOperation()
        {
            _gameObserversThatConfirmedCurrentTurn = new List<IGameObserver>();

            //so this is the first turn
            if (_cyclicPlayersEnumerator == null)
            {
                ArrangePlayersSequence(GameInformation, ExtendedGameInfo.PlayersSequence);

                InitializeCyclicPlayersEnumeratorForTheFirstTurn();
            }

                //this is not the first turn
            else
            {
                //simply move the enumerator to the next element
                _cyclicPlayersEnumerator.MoveNext();
            }

            PlayerInformation playerInfoToMakeTurn = _cyclicPlayersEnumerator.Current;

            PlayerInformation startingPlayerInfo = ExtendedGameInfo.PlayersSequence[0];
            PlayerInformation notStartingPlayerInfo = ExtendedGameInfo.PlayersSequence[1];

            //if there are no empty cells
            if (!GameAssistant.HasEmptyCells(GameInformation.GameField))
            {
                //terminate the game
                TerminateGame();
                return;
            }

            bool startingPlayerWins = GameAssistant.SideWins(GameInformation.GameField,
                                                             GameInformation.GameRules,
                                                             startingPlayerInfo.Side,
                                                             new List<Cell>());

            bool notStartingPlayerWins = GameAssistant.SideWins(GameInformation.GameField,
                                                                GameInformation.GameRules,
                                                                notStartingPlayerInfo.Side,
                                                                new List<Cell>());

            //if a not starting player wins
            if (notStartingPlayerWins)
            {
                //terminate the game
                TerminateGame();
                return;
            }

            //if a starting player wins
            if (startingPlayerWins)
            {
                //if a not starting player should have a turn after the starting player wins
                if (GameInformation.GameRules.NotStartingSideWillHaveATurnAfterTheStartingSideVictory)
                {
                    //if the current player is the not starting player 
                    //(note that we have already moved the cyclic enumerator to the next player info in the playing sequence),
                    //so the last turn of the not starting player has not been made yet
                    if (playerInfoToMakeTurn.Equals(notStartingPlayerInfo))
                    {
                        //make a turn of the not starting player
                        MakeTurnOfAPlayerWithSuchPlayerInfo(playerInfoToMakeTurn);

                    }
                        //if the current player is the starting player 
                        //(note that we have already moved the cyclic enumerator to the next player info in the playing sequence),
                        //so the last turn of the not starting player has already been made
                    else
                    {
                        //terminate the game
                        TerminateGame();
                        return;
                    }

                }
                    //if a not starting player should NOT have a turn after the starting player wins
                else
                {
                    //terminate the game
                    TerminateGame();
                    return;
                }
            }

                //there are no winners
            else
            {
                //make turn of a player with currnet player info
                MakeTurnOfAPlayerWithSuchPlayerInfo(playerInfoToMakeTurn);
            }
        }

        #endregion

        private void ModifyParticipantsCellsAfterTheTurn(Cell cell, Side side)
        {
            foreach (IPlayer player in Players)
            {
                player.ModifyCell(cell, side);
            }
            foreach (IGameObserver gameObserver in GameObservers)
            {
                gameObserver.ModifyCell(cell, side);
            }
        }

        private bool AllGameObserversConfirmedTurn()
        {
            return (_gameObserversThatConfirmedCurrentTurn.Count == GameObservers.Count);
        }

        private void MoveToTheNextTurnStep()
        {
            GMStepOperationCompletedEventArgs e = new GMStepOperationCompletedEventArgs(this);
            OnGMStepOperationCompleted(e);
        }

        private void TerminateGame()
        {
            GMStepOperationCompletedEventArgs e = new GMStepOperationCompletedEventArgs(new TerminationStep(ExtendedGameInfo));
            OnGMStepOperationCompleted(e);
        }

        private void MakeTurnOfAPlayerWithSuchPlayerInfo(PlayerInformation playerInfo)
        {
            IPlayer playerToMakeTurn = GetPlayerWithSuchPlayerInfo(playerInfo);
            playerToMakeTurn.CurrentPlayerInfo = playerInfo;
            playerToMakeTurn.MakeTurn();
        }

        private IPlayer GetPlayerWithSuchPlayerInfo(PlayerInformation playerInfo)
        {
            foreach (IPlayer player in Players)
            {
                if (player.AvailablePlayerInfos.Contains(playerInfo))
                {
                    return player;
                }
            }

            return null;
        }

        #region IGameObserver event handling methods

        private void HandleTurnConfirmedEvent(object sender, EventArgs e)
        {
            IGameObserver gameObserver = sender as IGameObserver;
            _gameObserversThatConfirmedCurrentTurn.Add(gameObserver);

            if (AllGameObserversConfirmedTurn())
            {
                MoveToTheNextTurnStep();
            }
        }

        #endregion

        #region IPlayer event handling methods

        private void HandleTurnEndEvent(object sender, TurnMadeEventArgs e)
        {
            IPlayer player = (IPlayer)sender;
            ExtendedGameInfo.GameInfo.GameField.SetCellState(e.RecentlyModifiedCell, (CellState)player.CurrentPlayerInfo.Side);
            ModifyParticipantsCellsAfterTheTurn(e.RecentlyModifiedCell, player.CurrentPlayerInfo.Side);
        }

        #endregion

        protected override void SubscribeToPlayerEvents(IPlayer player)
        {
            player.TurnMade += HandleTurnEndEvent;
            base.SubscribeToPlayerEvents(player);
        }

        protected override void UnsubscribeFromPlayerEvents(IPlayer player)
        {
            player.TurnMade -= HandleTurnEndEvent;
            base.UnsubscribeFromPlayerEvents(player);
        }

        protected override void SubscribeToGameObserverEvents(IGameObserver gameObserver)
        {
            gameObserver.TurnConfirmed += HandleTurnConfirmedEvent;
            base.SubscribeToGameObserverEvents(gameObserver);
        }

        protected override void UnsubscribeFromGameObserverEvents(IGameObserver gameObserver)
        {
            gameObserver.TurnConfirmed -= HandleTurnConfirmedEvent;
            base.UnsubscribeFromGameObserverEvents(gameObserver);
        }

        private void ArrangePlayersSequence(GameInformation gameInfo, List<PlayerInformation> playersSequence)
        {
            playersSequence.Clear();
            PlayerInformation playerX;
            PlayerInformation playerO;

            if (gameInfo.Players[0].Side == Side.X)
            {
                playerX = GameInformation.Players[0];
                playerO = GameInformation.Players[1];
            }
            else
            {
                playerX = GameInformation.Players[1];
                playerO = GameInformation.Players[0];
            }

            if (gameInfo.GameRules.StartingSide == Side.X)
            {
                ExtendedGameInfo.PlayersSequence.Add(playerX);
                ExtendedGameInfo.PlayersSequence.Add(playerO);
            }
            else
            {
                playersSequence.Add(playerO);
                playersSequence.Add(playerX);
            }
        }

        private void InitializeCyclicPlayersEnumeratorForTheFirstTurn()
        {
            IEnumerator<Turn> historyEnumerator = GameInformation.GameHistory.GetEnumerator();

            IEnumerator<PlayerInformation> playersEnumerator = ExtendedGameInfo.PlayersSequence.GetEnumerator();

            _cyclicPlayersEnumerator = new CyclicEnumerator<PlayerInformation>(playersEnumerator);
            _cyclicPlayersEnumerator.Reset();
            _cyclicPlayersEnumerator.MoveNext();

            historyEnumerator.Reset();
            //so the history is not empty
            if (historyEnumerator.MoveNext())
            {
                //determine on the game history who should make the turn

                //and now we'll move the enumerators simultaneously until we reach the end of the history
                while (historyEnumerator.MoveNext())
                {
                    _cyclicPlayersEnumerator.MoveNext();
                }

                //and we should move the enumerator to the next element once more, as now it
                //points to the last player, which has alerady made the turn
                _cyclicPlayersEnumerator.MoveNext();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TicTacToe3D.GameServer.Events;
using TicTacToe3D.GameServer.Interfaces;
using TicTacToe3D.GameStages.BaseWorkflowStage;
using TicTacToe3D.GeneralWorkflow.Events;
using TicTacToe3D.GeneralWorkflow.Interfaces;
using TicTacToe3D.HumanParticipants;
using TicTacToe3D.GameInfo;
using TicTacToe3D.AI;
using TicTacToe3D.GameServer;
using TicTacToe3D.GameStatisticsStage;

namespace TicTacToe3D.GameStages.GameStage
{
    public class GameStageDirector: BaseWorkflowStep
    {
        private IGameMaster _gameMaster;
        private readonly GameInformation _gameInfo;
        private readonly HumanPlayer _gameView;
        private Form _parentWinForm;

        public GameStageDirector(GameInformation gameInfo)
        {
            _gameInfo = gameInfo;

            CheckGameInfo();

            _gameView = new HumanPlayer();
            _gameView.Visible = false;
        }

        private void AddComputerPlayersToGameMaster()
        {
            IPlayer currentPlayer;
            foreach (PlayerInformation playerInfo in _gameInfo.Players)
            {
                if (playerInfo.PlayerType == PlayerType.Computer)
                {
                    currentPlayer = ComputerPlayerFactory.CreateComputerPlayer(playerInfo);

                    currentPlayer.AvailablePlayerInfos.Add(playerInfo);
                    currentPlayer.CurrentPlayerInfo = playerInfo;


                    _gameMaster.AddPlayer(currentPlayer);
                }
            }
        }

        private void AddHumanPlayersToGameMaster()
        {
            List<PlayerInformation> humanPlayerInfos = new List<PlayerInformation>();

            foreach (PlayerInformation playerInfo in _gameInfo.Players)
            {
                if (playerInfo.PlayerType == PlayerType.Human)
                {
                    humanPlayerInfos.Add(playerInfo);
                }
            }

            _gameView.AvailablePlayerInfos = humanPlayerInfos;
            _gameView.CurrentPlayerInfo = humanPlayerInfos[0];

            _gameMaster.AddPlayer(_gameView);
            //_gameViewPresenter.GameEnded += new EventHandler<GameEndedEventArgs>(HandleGameEndedEvent);
        }

        private void CheckGameInfo()
        {
            if (_gameInfo.GameType != GameType.SingleComputer)
            {
                throw new NotImplementedException();
            }

            bool containsHumanPlayers = false;
            foreach (PlayerInformation playerInfo in _gameInfo.Players)
            {
                if (playerInfo.PlayerType == PlayerType.Human)
                {
                    containsHumanPlayers = true;
                }
                if (playerInfo.PlayerType == PlayerType.NeuralNetwork)
                {
                    throw new NotImplementedException("neural networks are not supported");
                }
            }
            if (!containsHumanPlayers)
            {
                throw new NotImplementedException("At least one human player should participate");
            }
        }

        private void HandleGameEndedEvent(object sender, GameEndedEventArgs e)
        {
            _gameMaster.GameEnded -= HandleGameEndedEvent;

            IWorkflowStep gameStatisticsStep = GameStatisticsStageFactory.CreateGameStatisticsStage(e.GameInformation);

            OperationCompletedEventArgs operationCompletedEventArgs = new OperationCompletedEventArgs(gameStatisticsStep);
            OnOperationCompleted(operationCompletedEventArgs);
        }

        #region IWorkflowStep Members

        public override void Show(Form parentWinForm)
        {
            parentWinForm.Controls.Add(_gameView);
            _parentWinForm = parentWinForm;

            _gameView.Visible = true;

            CenterForm(_parentWinForm);
        }

        public override void StartOperation()
        {
            _gameMaster = GameMasterFactory.CreateGameMaster(_gameInfo);

            _gameMaster.GameEnded += HandleGameEndedEvent;

            AddComputerPlayersToGameMaster();

            AddHumanPlayersToGameMaster();
        }

        public override void Hide()
        {
            _gameView.Visible = false;
            _parentWinForm.Controls.Remove(_gameView);
        }

        #endregion
    }
}
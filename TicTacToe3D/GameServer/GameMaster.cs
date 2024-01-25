using System;
using System.Collections.Generic;
using TicTacToe3D.GameInfo;
using TicTacToe3D.GameServer.Events;
using TicTacToe3D.GameServer.Interfaces;

namespace TicTacToe3D.GameServer
{
    public class GameMaster : IGameMaster
    {
        #region Fields

        private IGameMasterStep _currentStep;
        private readonly ExtendedGameInfo _extendedGameInfo;

        #endregion

        public GameMaster(ExtendedGameInfo extendedGameInfo, IGameMasterStep firstGameMasterStep)
        {
            _extendedGameInfo = extendedGameInfo;

            _currentStep = firstGameMasterStep;

            ProcessCurrentGameMasterStep();
        }

        #region IGameMaster Members

        public GameInformation GameInfo
        {
            get
            {
                return _extendedGameInfo.GameInfo;
            }
        }

        public void AddPlayer(IPlayer player)
        {
            if (_currentStep != null)
            {
                _currentStep.AddPlayer(player);
            }
        }

        public void RemovePlayer(IPlayer player)
        {
            if (_currentStep != null)
            {
                _currentStep.RemovePlayer(player);
            }
        }

        public void AddGameObserver(IGameObserver gameObserver)
        {
            if (_currentStep != null)
            {
                _currentStep.AddGameObserver(gameObserver);
            }
        }

        public void RemoveGameObserver(IGameObserver gameObserver)
        {
            if (_currentStep != null)
            {
                _currentStep.RemoveGameObserver(gameObserver);
            }
        }

        public void ChangePlayerObject(List<PlayerInformation> playerInfos, IPlayer newPlayer)
        {
            if (_currentStep != null)
            {
                _currentStep.ChangePlayerObject(playerInfos, newPlayer);
            }
        }

        public event EventHandler<GameEndedEventArgs> GameEnded;

        #endregion

        private void HandleOperationCompletedEvent(object sender, GMStepOperationCompletedEventArgs e)
        {
            _currentStep.UnsubscribeFromParticipantsEvents();
            _currentStep.GMStepOperationCompleted -= HandleOperationCompletedEvent;
            _currentStep = e.NextStep;

            if (_currentStep == null && GameEnded != null)
            {
                GameEnded(this, new GameEndedEventArgs(_extendedGameInfo.GameInfo));

                return;
            }
            if (_currentStep == null && GameEnded == null)
            {
                return;
            }

            ProcessCurrentGameMasterStep();
        }

        private void ProcessCurrentGameMasterStep()
        {
            _currentStep.GMStepOperationCompleted += HandleOperationCompletedEvent;
            _currentStep.SubscribeToParticipantsEvents();
            _currentStep.StartOperation();
        }
    }
}

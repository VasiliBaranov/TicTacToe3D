using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TicTacToe3D.GameInfo;
using TicTacToe3D.GameStages.GameBuildingStage.Events;
using TicTacToe3D.GameStages.GameBuildingStage.Interfaces;
using TicTacToe3D.GameStages.GameBuildingStage.PlayerInfoListController;
using TicTacToe3D.GameStages.GameBuildingStage.PlayerInfoListController.Interfaces;

namespace TicTacToe3D.GameStages.GameBuildingStage
{
    public partial class PlayerInfoListCollector : UserControl, IPlayerInfoListCollector
    {
        private readonly List<PlayerInformationCollector> _playerInfoCollectors = new List<PlayerInformationCollector>();
        private readonly List<PlayerInformation> _playerInfoList = new List<PlayerInformation>();
        private readonly IPlayerInfoListController _controller;

        public List<PlayerInformation> PlayerInfoList
        {
            get
            {
                return _playerInfoList;
            }
        }

        public PlayerInfoListCollector()
        {
            InitializeComponent();

            _playerInfoCollectors.Add(playerInformationCollector1);
            _playerInfoCollectors.Add(playerInformationCollector2);

            List<IPlayerInfoCollector> playerInfoCollectorInterfaces = new List<IPlayerInfoCollector>();

            foreach (PlayerInformationCollector playerInfoCollector in _playerInfoCollectors)
            {
                playerInfoCollectorInterfaces.Add(playerInfoCollector);
            }

            _controller = PlayerInfoListControllerFactory.CreatePlayerInfoCollectionAssistant(playerInfoCollectorInterfaces);

        }

        #region IPlayerInfoCollectorsDirector Members


        public event EventHandler PlayerInfoListChanged;

        public void StartInfoCollection()
        {
            Enabled = true;

            _controller.SetDefaultCollectorsAvailableListsAndProperties();

            FillInPlayersInformationList();

            SubscribeToCollectorsEvents();

            foreach (PlayerInformationCollector playerInfoCollector in _playerInfoCollectors)
            {
                playerInfoCollector.StartInfoCollection();
            }
        }

        public void StopInfoCollection()
        {
            Enabled = false;

            UnsubscribeFromCollectorsEvents();

            foreach (PlayerInformationCollector playerInfoCollector in _playerInfoCollectors)
            {
                playerInfoCollector.StopInfoCollection();
            }
        }

        #endregion

        private void SetProperDescription()
        {

        }

        private void SubscribeToCollectorsEvents()
        {
            foreach (PlayerInformationCollector playerInfoCollector in _playerInfoCollectors)
            {
                playerInfoCollector.PlayerInformationChangedByUser += 
                    HandlePlayerInformationChangedEvent;
            }
        }

        private void UnsubscribeFromCollectorsEvents()
        {
            foreach (PlayerInformationCollector playerInfoCollector in _playerInfoCollectors)
            {
                playerInfoCollector.PlayerInformationChangedByUser -= 
                    HandlePlayerInformationChangedEvent;
            }
        }

        private void FillInPlayersInformationList()
        {
            _playerInfoList.Clear();

            foreach (PlayerInformationCollector playerInfoCollector in _playerInfoCollectors)
            {
                _playerInfoList.Add(playerInfoCollector.PlayerInformation);
            }
        }

        private void HandlePlayerInformationChangedEvent(object sender, PlayerInfoChangedEventArgs e)
        {
            IPlayerInfoCollector changedPlayerInfoCollector = sender as IPlayerInfoCollector;

            _controller.ApplyRestrictionsToCollectorsValuesAndLists(changedPlayerInfoCollector, e.ChangedPropertyName);

            FillInPlayersInformationList();

            SetProperDescription();

            if (PlayerInfoListChanged != null)
            {
                PlayerInfoListChanged(this, new EventArgs());
            }
        }
    }
}
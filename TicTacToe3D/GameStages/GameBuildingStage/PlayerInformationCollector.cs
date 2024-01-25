using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TicTacToe3D.GameInfo;
using TicTacToe3D.GameStages.GameBuildingStage;
using TicTacToe3D.GameStages.GameBuildingStage.Events;
using TicTacToe3D.GameStages.GameBuildingStage.Interfaces;

namespace TicTacToe3D.GameStages.GameBuildingStage
{
    public partial class PlayerInformationCollector : UserControl, IPlayerInfoCollector
    {
        private List<Side> _availableSides = new List<Side>();
        private List<PlayerDifficulty> _availablePlayerDifficulties = new List<PlayerDifficulty>();
        private List<PlayerType> _availablePlayerTypes = new List<PlayerType>();
        private PlayerInformation _playerInformation;

        public PlayerInformationCollector()
        {
            InitializeComponent();

            SubscribeToControlsEvents();
        }

        private static void SetItemsInComboBox<T>(ComboBox comboBox, IEnumerable<T> items)
        {
            comboBox.Items.Clear();
            foreach (T item in items)
            {
                comboBox.Items.Add(item);
            }
        }

        private void SubscribeToControlsEvents()
        {
            playerDifficultyComboBox.SelectedIndexChanged += HandlePlayerInformationChangedEvent;
            playerNameTextBox.TextChanged += HandlePlayerInformationChangedEvent;
            playerTypeComboBox.SelectedIndexChanged += HandlePlayerInformationChangedEvent;
            playerSideComboBox.SelectedIndexChanged += HandlePlayerInformationChangedEvent;
        }

        private void UnsubscribeFromControlsEvents()
        {
            playerDifficultyComboBox.SelectedIndexChanged -= HandlePlayerInformationChangedEvent;
            playerNameTextBox.TextChanged -= HandlePlayerInformationChangedEvent;
            playerTypeComboBox.SelectedIndexChanged -= HandlePlayerInformationChangedEvent;
            playerSideComboBox.SelectedIndexChanged -= HandlePlayerInformationChangedEvent;
        }

        #region IPLayerInfoCollector Members

        public PlayerInformation PlayerInformation
        {
            get
            {
                return _playerInformation;
            }
            set
            {
                _playerInformation = value;

                UnsubscribeFromControlsEvents();

                playerDifficultyComboBox.SelectedItem = _playerInformation.Difficulty;
                playerSideComboBox.SelectedItem = _playerInformation.Side;
                playerTypeComboBox.SelectedItem = _playerInformation.PlayerType;
                playerNameTextBox.Text = _playerInformation.Name;

                SubscribeToControlsEvents();
            }
        }

        public event EventHandler<PlayerInfoChangedEventArgs> PlayerInformationChangedByUser;

        public List<Side> AvailableSides
        {
            get
            {
                return _availableSides;
            }
            set
            {
                _availableSides = value ?? new List<Side>();

                SetItemsInComboBox(playerSideComboBox, _availableSides);
            }
        }

        public List<PlayerDifficulty> AvailablePlayerDifficulties
        {
            get
            {
                return _availablePlayerDifficulties;
            }
            set
            {
                _availablePlayerDifficulties = value ?? new List<PlayerDifficulty>();

                SetItemsInComboBox(playerDifficultyComboBox, _availablePlayerDifficulties);
            }
        }

        public List<PlayerType> AvailablePlayerTypes
        {
            get
            {
                return _availablePlayerTypes;
            }
            set
            {
                _availablePlayerTypes = value ?? new List<PlayerType>();

                SetItemsInComboBox(playerTypeComboBox, _availablePlayerTypes);
            }
        }

        public void StartInfoCollection()
        {
            Enabled = true;
        }

        public void StopInfoCollection()
        {
            Enabled = false;
        }

        #endregion

        protected void OnPlayerInformationChanged(PlayerInfoChangedEventArgs e)
        {
            if (PlayerInformationChangedByUser != null)
            {
                PlayerInformationChangedByUser(this, e);
            }
        }

        private void HandlePlayerInformationChangedEvent(object sender, EventArgs e)
        {
            PlayerInfoChangedEventArgs eventArgs;
            if (sender == playerDifficultyComboBox)
            {
                _playerInformation.Difficulty = (PlayerDifficulty)playerDifficultyComboBox.SelectedItem;
                eventArgs = new PlayerInfoChangedEventArgs(PlayerInfoPropertyName.Difficulty);
            }
            else if (sender == playerNameTextBox)
            {
                _playerInformation.Name = playerNameTextBox.Text;
                eventArgs = new PlayerInfoChangedEventArgs(PlayerInfoPropertyName.Name);
            }
            else if (sender == playerTypeComboBox)
            {
                _playerInformation.PlayerType = (PlayerType)playerTypeComboBox.SelectedItem;
                eventArgs = new PlayerInfoChangedEventArgs(PlayerInfoPropertyName.PlayerType);
            }
            else
            {
                _playerInformation.Side = (Side)playerSideComboBox.SelectedItem;
                eventArgs = new PlayerInfoChangedEventArgs(PlayerInfoPropertyName.Side);
            }

            OnPlayerInformationChanged(eventArgs);
        }
    }
}
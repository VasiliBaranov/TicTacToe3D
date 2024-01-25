using System;

namespace TicTacToe3D.GameStages.GameBuildingStage.Events
{
    public class PlayerInfoChangedEventArgs: EventArgs
    {
        private readonly PlayerInfoPropertyName _changedPropertyName;

        public PlayerInfoPropertyName ChangedPropertyName
        {
            get
            {
                return _changedPropertyName;
            }
        }

        public PlayerInfoChangedEventArgs(PlayerInfoPropertyName changedPropertyName)
        {
            _changedPropertyName = changedPropertyName;
        }
    }
}
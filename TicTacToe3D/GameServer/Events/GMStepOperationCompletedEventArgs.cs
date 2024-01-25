using System;
using TicTacToe3D.GameServer.GameMasterSteps;

namespace TicTacToe3D.GameServer.Events
{
    /// <summary>
    /// Provides data for the GMStepOperationCompleted Event of a GameMaster step.
    /// </summary>
    public class GMStepOperationCompletedEventArgs : EventArgs
    {
        private readonly GameMasterStep _nextStep;

        /// <summary>
        /// Gets the next game master step
        /// </summary>
        public GameMasterStep NextStep
        {
            get
            {
                return _nextStep;
            }
        }

        /// <summary>
        /// Initializes a new instance of the GMStepOperationCompletedEventArgs class using the specified next step information
        /// </summary>
        public GMStepOperationCompletedEventArgs(GameMasterStep nextStep)
        {
            _nextStep = nextStep;
        }
    }
}
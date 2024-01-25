using TicTacToe3D.GameInfo;
using TicTacToe3D.GameServer.Interfaces;

namespace TicTacToe3D.AI
{
    public static class ComputerPlayerFactory
    {
        /// <summary>
        /// Creates a computer player according to the player info
        /// </summary>
        /// <param name="playerInfo">Player info which is used to create a computer player</param>
        /// <returns>Computer player with available players list and current player info NOT updated</returns>
        public static IPlayer CreateComputerPlayer(PlayerInformation playerInfo)
        {
            PlayerDifficulty difficulty = playerInfo.Difficulty;

            if (difficulty == PlayerDifficulty.Easy)
            {
                return new EasyComputerPlayer();
            }
            if (difficulty == PlayerDifficulty.Normal)
            {
                return new NormalComputerPlayer();
            }
            return new HardComputerPlayer();
        }
    }
}

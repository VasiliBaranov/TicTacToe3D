using TicTacToe3D.GameInfoSerialization.Interfaces;

namespace TicTacToe3D.GameInfoSerialization
{
    public static class GameInfoSerializerFactory
    {
        public static IGameInfoSerializer CreateGameInfoSerializer()
        {
            return new GameInfoSerializer();
        }
    }
}

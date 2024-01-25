using System.Drawing;
using GraphicsFramework.World;
using TicTacToe3D.GameInfo;
using TicTacToe3D.GraphicsAdapters.AdvancedGraphics;

namespace TicTacToe3D.GraphicsAdapters.AdvancedGraphics
{
    public static class GraphicalObjectsFactory
    {
        public static IGraphicalObject CreateGameSpace(double cellSize, Color drawingColor)
        {
            return new GameSpace(cellSize, drawingColor);
        }

        public static IGraphicalObject CreateGameObject(double cellSize, Side side)
        {
            if (side == Side.X)
            {
                return new FigureX(cellSize);
            }
            return new FigureO(cellSize);
        }
    }
}
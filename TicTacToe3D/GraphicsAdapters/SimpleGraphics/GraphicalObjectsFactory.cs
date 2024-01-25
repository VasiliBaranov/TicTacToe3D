using System.Drawing;
using TicTacToe3D.GameInfo;
using TicTacToe3D.GraphicsAdapters.SimpleGraphics;
using TicTacToe3D.SimpleGraphicsFramework;
namespace TicTacToe3D.GraphicsAdapters.SimpleGraphics
{
    public static class GraphicalObjectsFactory
    {
        public static IGraphicalObject CreateGameSpace(double cellSize, Color drawingColor)
        {
            return new GameSpace(cellSize, drawingColor);
        }

        public static IGraphicalObject CreateGameObject(double cellSize, Side side, Color drawingColor)
        {
            if (side == Side.X)
            {
                return new FigureX(cellSize, drawingColor);
            }
            else
            {
                return new FigureO(cellSize, drawingColor);
            }
        }
    }
}
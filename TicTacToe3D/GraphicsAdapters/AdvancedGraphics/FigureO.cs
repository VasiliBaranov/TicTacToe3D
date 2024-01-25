using Rectangle = GraphicsFramework.BaseObjects.Rectangle;

namespace TicTacToe3D.GraphicsAdapters.AdvancedGraphics
{
    public class FigureO : Rectangle
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_cellSize">Cell _size in pixels</param>
        public FigureO(double _cellSize)
            : base(_cellSize / 10.0 * 1.5, _cellSize / 10.0, @"GraphicsAdapters/AdvancedGraphics/Images/FigureO.png")
        {

        }
    }
}
using System.Drawing;
using GraphicsFramework.LinearAlgebra;
using TicTacToe3D.SimpleGraphicsFramework;

namespace TicTacToe3D.GraphicsAdapters.SimpleGraphics
{
    public class GameSpace : GraphicalObject
    {
        private readonly double _cellSize;
        private readonly Pen _drawingPen;


        public GameSpace(double cellSize, Color drawingColor)
            : base(24)
        {
            _cellSize = cellSize;
            _drawingPen = new Pen(drawingColor);

            AssignDefaultMesh();
        }

        /// <summary> 
        /// Initializes and assigns the default Mesh
        /// </summary> 
        protected override void AssignDefaultMesh()
        {
            //inner lines

            //между х-плоскостями(параллельно х)

            //z=0.5*cellSize

            //beta=0.5*cellSize
            Mesh[0] = new AffinePoint(-1.5 * _cellSize, 0.5 * _cellSize, 0.5 * _cellSize);
            Mesh[1] = new AffinePoint(1.5 * _cellSize, 0.5 * _cellSize, 0.5 * _cellSize);

            //beta=-0.5*cellSize
            Mesh[2] = new AffinePoint(-1.5 * _cellSize, -0.5 * _cellSize, 0.5 * _cellSize);
            Mesh[3] = new AffinePoint(1.5 * _cellSize, -0.5 * _cellSize, 0.5 * _cellSize);

            //z=-0.5*cellSize

            //beta=0.5*cellSize
            Mesh[4] = new AffinePoint(-1.5 * _cellSize, 0.5 * _cellSize, -0.5 * _cellSize);
            Mesh[5] = new AffinePoint(1.5 * _cellSize, 0.5 * _cellSize, -0.5 * _cellSize);

            //beta=-0.5*cellSize
            Mesh[6] = new AffinePoint(-1.5 * _cellSize, -0.5 * _cellSize, -0.5 * _cellSize);
            Mesh[7] = new AffinePoint(1.5 * _cellSize, -0.5 * _cellSize, -0.5 * _cellSize);

            //между beta-плоскостями(параллельно beta)

            //z=0.5*cellSize

            //alpha=0.5*cellSize
            Mesh[8] = new AffinePoint(0.5 * _cellSize, -1.5 * _cellSize, 0.5 * _cellSize);
            Mesh[9] = new AffinePoint(0.5 * _cellSize, 1.5 * _cellSize, 0.5 * _cellSize);

            //alpha=-0.5*cellSize
            Mesh[10] = new AffinePoint(-0.5 * _cellSize, -1.5 * _cellSize, 0.5 * _cellSize);
            Mesh[11] = new AffinePoint(-0.5 * _cellSize, 1.5 * _cellSize, 0.5 * _cellSize);

            //z=-0.5*cellSize

            //alpha=0.5*cellSize
            Mesh[12] = new AffinePoint(0.5 * _cellSize, -1.5 * _cellSize, -0.5 * _cellSize);
            Mesh[13] = new AffinePoint(0.5 * _cellSize, 1.5 * _cellSize, -0.5 * _cellSize);

            //alpha=-0.5*cellSize
            Mesh[14] = new AffinePoint(-0.5 * _cellSize, -1.5 * _cellSize, -0.5 * _cellSize);
            Mesh[15] = new AffinePoint(-0.5 * _cellSize, 1.5 * _cellSize, -0.5 * _cellSize);

            //между z-плоскостями(параллельно z)

            //beta=0.5*cellSize

            //alpha=0.5*cellSize
            Mesh[16] = new AffinePoint(0.5 * _cellSize, 0.5 * _cellSize, -1.5 * _cellSize);
            Mesh[17] = new AffinePoint(0.5 * _cellSize, 0.5 * _cellSize, 1.5 * _cellSize);

            //alpha=-0.5*cellSize
            Mesh[18] = new AffinePoint(-0.5 * _cellSize, 0.5 * _cellSize, -1.5 * _cellSize);
            Mesh[19] = new AffinePoint(-0.5 * _cellSize, 0.5 * _cellSize, 1.5 * _cellSize);

            //beta=-0.5*cellSize

            //alpha=0.5*cellSize
            Mesh[20] = new AffinePoint(0.5 * _cellSize, -0.5 * _cellSize, -1.5 * _cellSize);
            Mesh[21] = new AffinePoint(0.5 * _cellSize, -0.5 * _cellSize, 1.5 * _cellSize);

            //alpha=-0.5*cellSize
            Mesh[22] = new AffinePoint(-0.5 * _cellSize, -0.5 * _cellSize, -1.5 * _cellSize);
            Mesh[23] = new AffinePoint(-0.5 * _cellSize, -0.5 * _cellSize, 1.5 * _cellSize);
        }

        /// <summary> 
        /// Draws the figure on the form by GraphPanel object
        /// </summary>
        protected override void DrawProectedMeshPoints(Graphics graphics)
        {
            int i;

            //drawing inner lines
            for (i = 0; i < NumberOfVertices; i += 2)
            {
                graphics.DrawLine(_drawingPen, ProectedMesh[i], ProectedMesh[i + 1]);
            }
        }
    }
}
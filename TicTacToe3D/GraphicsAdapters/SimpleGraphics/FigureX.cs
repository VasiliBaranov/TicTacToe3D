using System.Drawing;
using GraphicsFramework.LinearAlgebra;
using TicTacToe3D.SimpleGraphicsFramework;

namespace TicTacToe3D.GraphicsAdapters.SimpleGraphics
{
    public class FigureX : GraphicalObject
    {
        private readonly double _size;

        /// <summary> 
        /// 0.5 ширины балки
        /// </summary>
        private readonly double _width;

        /// <summary> 
        /// Proportion between the cellSize and the _size of the zero
        /// </summary>
        //private const double _scale;

        private readonly Brush _drawingBrush = Brushes.DarkBlue;
        private readonly Pen _drawingPen = Pens.DarkBlue;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cellSize">Cell _size in pixels</param>
        /// <param name="drawingColor">Color of the drawing.</param>
        public FigureX(double cellSize, Color drawingColor)
            : base(8)
        {
            _size = cellSize / 10;
            _width = _size / 5;

            _drawingBrush = new SolidBrush(drawingColor);
            _drawingPen = new Pen(drawingColor);

            AssignDefaultMesh();
        }

        /// <summary> 
        /// Initializes and assigns the default Mesh
        /// </summary> 
        protected override void AssignDefaultMesh()
        {
            //рисуем каждую балку в отдельности. Обход по часовой стрелке, если смотреть вдоль OY, начиная с 
            //верхнего крайнего справа угла
            //the outer boundaries
            Mesh[0] = new AffinePoint(_size + _width, 0, _size);
            Mesh[1] = new AffinePoint(-_size + _width, 0, -_size);
            Mesh[2] = new AffinePoint(-_size - _width, 0, -_size);
            Mesh[3] = new AffinePoint(_size - _width, 0, _size);

            //inner boundaries
            Mesh[4] = new AffinePoint(-_size + _width, 0, _size);
            Mesh[5] = new AffinePoint(_size + _width, 0, -_size);
            Mesh[6] = new AffinePoint(_size - _width, 0, -_size);
            Mesh[7] = new AffinePoint(-_size - _width, 0, _size);
        }

        protected override void DrawProectedMeshPoints(Graphics graphics)
        {
            // Additional Mesh proection
            Point[] _additionalMeshProection = new Point[4];

            int i;

            //filling the area within the outer boundaries
            for (i = 0; i < 4; i++)
            {
                _additionalMeshProection[i] = ProectedMesh[i];
            }

            graphics.FillClosedCurve(_drawingBrush, _additionalMeshProection);

            if (ProectedMesh[0].Y == ProectedMesh[2].Y ||
                ProectedMesh[0].X == ProectedMesh[2].X)
            {
                graphics.DrawClosedCurve(_drawingPen, _additionalMeshProection);
            }

            for (i = 0; i < 4; i++)
            {
                _additionalMeshProection[i] = ProectedMesh[i + 4];
            }

            graphics.FillClosedCurve(_drawingBrush, _additionalMeshProection);

            if (ProectedMesh[0].Y == ProectedMesh[2].Y ||
                ProectedMesh[0].X == ProectedMesh[2].X)
            {
                graphics.DrawClosedCurve(_drawingPen, _additionalMeshProection);
            }
        }
    }
}
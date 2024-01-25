using System.Drawing;
using GraphicsFramework.LinearAlgebra;
using TicTacToe3D.SimpleGraphicsFramework;

namespace TicTacToe3D.GraphicsAdapters.SimpleGraphics
{
    public class FigureO : GraphicalObject
    {
        /// <summary> 
        /// The _height of the zero
        /// </summary>
        private readonly double _height;

        /// <summary> 
        /// The _width of the zero
        /// </summary>
        private readonly double _width;

        /// <summary> 
        /// The inner _height of the zero
        /// </summary>
        private readonly double _innerHeight;

        /// <summary> 
        /// The inner _width of the zero
        /// </summary>
        private readonly double _innerWidth;

        /// <summary> 
        /// The proportion between the cellSize and the _size of the zero
        /// </summary>
        //private const double _scale;

        private readonly Brush _drawingBrush;
        private readonly Pen _drawingPen;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_cellSize">Cell _size in pixels</param>
        /// <param name="drawingColor">Color of the drawing.</param>
        public FigureO(double _cellSize, Color drawingColor)
            : base(8)
        {
            _width = _cellSize / 10;
            _height = _width * 1.5;
            _innerHeight = _height * 0.5;
            _innerWidth = _width * 0.5;

            _drawingBrush = new SolidBrush(drawingColor);
            _drawingPen = new Pen(drawingColor);

            AssignDefaultMesh();
        }

        /// <summary> 
        /// Initializes and assigns the default Mesh
        /// </summary> 
        protected override void AssignDefaultMesh()
        {
            //обход-по часовой стрелки: север(oz),восток(ox),юг,запад, если смотреть вдоль OY
            //the outer boundaries
            Mesh[0] = new AffinePoint(0, 0, _height);
            Mesh[1] = new AffinePoint(_width, 0, 0);
            Mesh[2] = new AffinePoint(0, 0, -_height);
            Mesh[3] = new AffinePoint(-_width, 0, 0);

            //inner boundaries
            Mesh[4] = new AffinePoint(0, 0, _innerHeight);
            Mesh[5] = new AffinePoint(_innerWidth, 0, 0);
            Mesh[6] = new AffinePoint(0, 0, -_innerHeight);
            Mesh[7] = new AffinePoint(-_innerWidth, 0, 0);
        }

        /// <summary> 
        /// Draws the figure on the form by GraphPanel object
        /// </summary>
        protected override void DrawProectedMeshPoints(Graphics graphics)
        {
            //Additional Mesh proection
            Point[] _additionalMeshProection = new Point[6];

            //drawing the first curve

            //part of the the outer circle
            _additionalMeshProection[0] = ProectedMesh[0]; //north
            _additionalMeshProection[1] = ProectedMesh[1]; //east
            _additionalMeshProection[2] = ProectedMesh[2]; //south

            //part of the the inner circle
            _additionalMeshProection[3] = ProectedMesh[6]; //south
            _additionalMeshProection[4] = ProectedMesh[5]; //east
            _additionalMeshProection[5] = ProectedMesh[4]; //north

            graphics.FillClosedCurve(_drawingBrush, _additionalMeshProection);

            if (ProectedMesh[0].Y == ProectedMesh[2].Y ||
                ProectedMesh[0].X == ProectedMesh[2].X)
            {
                graphics.DrawClosedCurve(_drawingPen, _additionalMeshProection);
            }

            //drawing the second curve

            //part of the the outer circle
            _additionalMeshProection[0] = ProectedMesh[0]; //north 
            _additionalMeshProection[1] = ProectedMesh[3]; //west
            _additionalMeshProection[2] = ProectedMesh[2]; //south

            //part of the the inner circle
            _additionalMeshProection[3] = ProectedMesh[6]; //south
            _additionalMeshProection[4] = ProectedMesh[7]; //west
            _additionalMeshProection[5] = ProectedMesh[4]; //north

            graphics.FillClosedCurve(_drawingBrush, _additionalMeshProection);

            if (ProectedMesh[0].Y == ProectedMesh[2].Y ||
                ProectedMesh[0].X == ProectedMesh[2].X)
            {
                graphics.DrawClosedCurve(_drawingPen, _additionalMeshProection);
            }
        }
    }
}
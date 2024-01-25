using System.Drawing;
using GraphicsFramework.LinearAlgebra;
using TicTacToe3D.SimpleGraphicsFramework;

namespace TicTacToe3D.SimpleGraphicsFramework
{
    /// <summary> 
    /// This class is the base class for all graphical objects used in the program( ObjectX, ObjectO and Framework)
    /// </summary>
    public abstract class GraphicalObject : IGraphicalObject
    {
        /// <summary> 
        /// Number of points used to draw the object
        /// </summary> 
        private readonly int _numberOfVertices;

        protected int NumberOfVertices
        {
            get
            {
                return _numberOfVertices;
            }
        }

        /// <summary> 
        /// A Framework for the game (it's an array of affine points)
        /// </summary> 
        private AffinePoint[] _mesh;

        protected AffinePoint[] Mesh
        {
            get
            {
                return _mesh;
            }
            set
            {
                _mesh = value;
            }
        }

        /// <summary> 
        /// An array of mesh proection
        /// </summary> 
        private Point[] _proectedMesh;

        protected Point[] ProectedMesh
        {
            get
            {
                return _proectedMesh;
            }
        }

        public GraphicalObject(int numberOfVertices)
        {
            _numberOfVertices = numberOfVertices;

            //initializing these arrays. 
            _mesh = new AffinePoint[_numberOfVertices];
            _proectedMesh = new Point[_numberOfVertices]; //and here all the Point objects are also created

            //AssignDefaultMesh();
        }

        /// <summary> 
        /// Assigns the default Mesh
        /// </summary> 
        abstract protected void AssignDefaultMesh();


        private void ProectMesh(Matrix proectionMatrix)
        {
            AffinePoint _proectedVertex;

            for (int i = 0; i < _numberOfVertices; i++)
            {
                _proectedVertex = proectionMatrix * _mesh[i];

                //экраном будет служить проекция на XOY, знак - для OY, т.к. ось 
                //OY на форме направлена вниз
                _proectedMesh[i].X = (int)_proectedVertex[0];
                _proectedMesh[i].Y = -(int)_proectedVertex[1];
            }
        }

        /// <summary> 
        /// Draws the figure on the form by _graphics object
        /// </summary>
        abstract protected void DrawProectedMeshPoints(Graphics graphics);

        #region IGraphicalObject Members

        public void Draw(Matrix proectionMatrix, Graphics graphics)
        {
            ProectMesh(proectionMatrix);

            DrawProectedMeshPoints(graphics);
        }

        public void Transform(Matrix transformationMatrix)
        {
            for (int i = 0; i < _numberOfVertices; i++)
            {
                _mesh[i] = transformationMatrix * _mesh[i];
            }
        }

        #endregion
    }
}
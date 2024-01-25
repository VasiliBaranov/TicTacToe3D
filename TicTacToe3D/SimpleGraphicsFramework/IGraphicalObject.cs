using System.Drawing;
using GraphicsFramework.LinearAlgebra;

namespace TicTacToe3D.SimpleGraphicsFramework
{
    /// <summary>
    /// Defines methods for a gaphical object.
    /// </summary>
    public interface IGraphicalObject
    {
        void Draw(Matrix proectionMatrix, Graphics graphics);

        void Transform(Matrix transformationMatrix);
    }
}
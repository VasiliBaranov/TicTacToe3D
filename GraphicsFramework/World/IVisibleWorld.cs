using System.Drawing;

namespace GraphicsFramework.World
{
    /// <summary>
    /// Represents an interface for the visible world,
    /// i.e. an objects that will be passed through the whole graphics pipeline
    /// and which is capable of storing all the information needed for each graphics pipeline step.
    /// </summary>
    public interface IVisibleWorld : IWorld
    {
        Bitmap Picture { get; }
    }
}

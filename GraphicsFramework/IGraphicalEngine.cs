namespace GraphicsFramework
{
    /// <summary>
    /// Defines methods for drawing a world.
    /// </summary>
    public interface IGraphicalEngine
    {
        /// <summary>
        /// Clears the viewport.
        /// </summary>
        void Clear();

        /// <summary>
        /// Draws this instance.
        /// </summary>
        void Draw();
    }
}
using System;
using System.Collections.Generic;
using GraphicsFramework.Core;

namespace GraphicsFramework.World
{
    /// <summary>
    /// Represents an interface for the world.
    /// </summary>
    public interface IWorld : ICloneable
    {
        /// <summary>
        /// Gets or sets the camera.
        /// </summary>
        /// <value>The camera.</value>
        Camera Camera { get; set; }

        /// <summary>
        /// Adds the graphical object to the world.
        /// </summary>
        /// <param name="graphicalObject">The graphical object.</param>
        void AddGraphicalObject(IGraphicalObject graphicalObject);

        /// <summary>
        /// Removes the graphical object to the world.
        /// </summary>
        /// <param name="graphicalObject">The graphical object.</param>
        void RemoveGraphicalObject(IGraphicalObject graphicalObject);

        /// <summary>
        /// Adds the light source to the world.
        /// </summary>
        /// <param name="lightSource">The light source.</param>
        void AddLightSource(LightSource lightSource);

        /// <summary>
        /// Removes the light source to the world.
        /// </summary>
        /// <param name="lightSource">The light source.</param>
        void RemoveLightSource(LightSource lightSource);

        //anything else)

        /// <summary>
        /// Gets the graphical objects.
        /// </summary>
        /// <value>The graphical objects.</value>
        List<IGraphicalObject> GraphicalObjects
        {
            get;
        }

        /// <summary>
        /// Gets the light sources.
        /// </summary>
        /// <value>The light sources.</value>
        List<LightSource> LightSources
        {
            get;
        }

        #region Events

        /// <summary>
        /// Occurs when graphical object has been added.
        /// </summary>
        event EventHandler<ItemEventArgs<IGraphicalObject>> GraphicalObjectAdded;

        /// <summary>
        /// Occurs when graphical object has been removed.
        /// </summary>
        event EventHandler<ItemEventArgs<IGraphicalObject>> GraphicalObjectRemoved;

        /// <summary>
        /// Occurs when light source has been added.
        /// </summary>
        event EventHandler<ItemEventArgs<LightSource>> LightSourceAdded;

        /// <summary>
        /// Occurs when light source has been removed.
        /// </summary>
        event EventHandler<ItemEventArgs<LightSource>> LightSourceRemoved;

        #endregion

    }
}

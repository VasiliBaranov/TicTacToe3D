using System;
using System.Collections.Generic;
using GraphicsFramework.Core;

namespace GraphicsFramework.World
{
    /// <summary>
    /// Represents a physical world, which then can be rendered.
    /// </summary>
    public class World : IWorld
    {
        #region Fields

        private Camera camera;

        private List<IGraphicalObject> graphicalObjects = new List<IGraphicalObject>();

        private List<LightSource> lightSources = new List<LightSource>();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="World"/> class.
        /// </summary>
        public World()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="World"/> class.
        /// </summary>
        /// <param name="camera">The camera.</param>
        /// <param name="graphicalObjects">The graphical objects.</param>
        /// <param name="lightSources">The light sources.</param>
        public World(Camera camera, List<IGraphicalObject> graphicalObjects, List<LightSource> lightSources)
        {
            this.camera = camera;
            this.graphicalObjects = graphicalObjects;
            this.lightSources = lightSources;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the camera.
        /// </summary>
        /// <value>The camera.</value>
        public Camera Camera
        {
            get { return camera; }
            set { camera = value; }
        }

        /// <summary>
        /// Gets the graphical objects.
        /// </summary>
        /// <value>The graphical objects.</value>
        public List<IGraphicalObject> GraphicalObjects
        {
            get
            {
                return graphicalObjects;
            }
        }

        /// <summary>
        /// Gets the light sources.
        /// </summary>
        /// <value>The light sources.</value>
        public List<LightSource> LightSources
        {
            get
            {
                return lightSources;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the graphical object to the world.
        /// </summary>
        /// <param name="graphicalObject">The graphical object.</param>
        public void AddGraphicalObject(IGraphicalObject graphicalObject)
        {
            graphicalObjects.Add(graphicalObject);
            FireEvent(GraphicalObjectAdded, graphicalObject);
        }

        /// <summary>
        /// Removes the graphical object to the world.
        /// </summary>
        /// <param name="graphicalObject">The graphical object.</param>
        public void RemoveGraphicalObject(IGraphicalObject graphicalObject)
        {
            graphicalObjects.Remove(graphicalObject);
            FireEvent(GraphicalObjectRemoved, graphicalObject);
        }

        /// <summary>
        /// Adds the light source to the world.
        /// </summary>
        /// <param name="lightSource">The light source.</param>
        public void AddLightSource(LightSource lightSource)
        {
            lightSources.Add(lightSource);
            FireEvent(LightSourceAdded, lightSource);
        }

        /// <summary>
        /// Removes the light source to the world.
        /// </summary>
        /// <param name="lightSource">The light source.</param>
        public void RemoveLightSource(LightSource lightSource)
        {
            lightSources.Remove(lightSource);
            FireEvent(LightSourceRemoved, lightSource);
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public object Clone()
        {
            World clone = new World
                              {
                                  camera = (camera.Clone() as Camera),
                                  lightSources = lightSources.DeepClone(),
                                  graphicalObjects = graphicalObjects.DeepClone()
                              };
            return clone;
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when graphical object has been added.
        /// </summary>
        public event EventHandler<ItemEventArgs<IGraphicalObject>> GraphicalObjectAdded;

        /// <summary>
        /// Occurs when graphical object has been removed.
        /// </summary>
        public event EventHandler<ItemEventArgs<IGraphicalObject>> GraphicalObjectRemoved;

        /// <summary>
        /// Occurs when light source has been added.
        /// </summary>
        public event EventHandler<ItemEventArgs<LightSource>> LightSourceAdded;

        /// <summary>
        /// Occurs when light source has been removed.
        /// </summary>
        public event EventHandler<ItemEventArgs<LightSource>> LightSourceRemoved;

        #endregion

        #region Private Methods

        /// <summary>
        /// Fires the event.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventHandler">The event handler.</param>
        /// <param name="item">The item.</param>
        private void FireEvent<T>(EventHandler<ItemEventArgs<T>> eventHandler, T item)
        {
            EventHandler<ItemEventArgs<T>> temp = eventHandler;
            if(temp!=null)
            {
                ItemEventArgs<T> eventArgs = new ItemEventArgs<T>(item);
                temp(this, eventArgs);
            }
        }

        #endregion

    }
}

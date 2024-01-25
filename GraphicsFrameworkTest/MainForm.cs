using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GraphicsFramework;
using GraphicsFramework.LinearAlgebra;
using GraphicsFramework.World;
using Rectangle=GraphicsFramework.BaseObjects.Rectangle;

namespace GraphicsFrameworkTest
{
    public partial class MainForm : Form
    {
        private IGraphicalEngine engine;
        private World world = new World();
        private Graphics graphics;

        private Vector3D displacementVector = new Vector3D(0, 0, 5);
        private Vector3D negDisplacementVector = new Vector3D(0, 0, -5);

        private string christmasTexturePath = "ChristmasRectangleTexture.png";
        private string chessTexturePath = "ChessRectangleTexture.bmp";

        public MainForm()
        {
            InitializeComponent();

            graphics = Graphics.FromHwnd(MainPanel.Handle);

            InitializeWorld();

            engine = new GraphicalEngine(graphics, world);
            engine.Draw();
        }

        private void InitializeWorld()
        {
            RectangleF clipBounds = graphics.VisibleClipBounds;
            CameraParameters cameraParameters = new CameraParameters(100, 1000, clipBounds.Width, clipBounds.Height);
            world.Camera = new Camera(cameraParameters);
            world.Camera.Move(new Vector3D(0, 0, 120));

            LightSource sun = new LightSource();
            world.LightSources.Add(sun);

            GraphicalObject chessRectangle = new Rectangle(clipBounds.Height / 5, clipBounds.Width / 5, chessTexturePath);
            world.AddGraphicalObject(chessRectangle);

            GraphicalObject christmasRectangle =
                new Rectangle(clipBounds.Height / 5, clipBounds.Width / 5, christmasTexturePath);
            world.AddGraphicalObject(christmasRectangle);
            christmasRectangle.Move(new Vector3D(0, 0, 10));
        }

        private void RotateCameraToTheLeft_Click(object sender, EventArgs e)
        {
            world.Camera.RotateRoundOriginOfCoordinatesRoundAxis(new Angle(-Math.PI / 10), Axis.X);
            engine.Draw();
        }

        private void RotateCameraToTheRightButton_Click(object sender, EventArgs e)
        {
            world.Camera.RotateRoundOriginOfCoordinatesRoundAxis(new Angle(Math.PI / 10), Axis.X);
            engine.Draw();
        }

        private void HandleZoomInClick(object sender, EventArgs e)
        {
            world.Camera.Move(displacementVector);
            engine.Draw();
        }

        private void HandleZoomOutClick(object sender, EventArgs e)
        {

            world.Camera.Move(negDisplacementVector);
            engine.Draw();
        }
    }
}

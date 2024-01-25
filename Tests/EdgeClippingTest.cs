using GraphicsFramework.Edges;
using GraphicsFramework.LinearAlgebra;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class EdgeClippingTest
    {
        [Test]
        public void TestClippingOutputIndependence()
        {
            IEdgeClipper edgeClipper = new LiangBarskyEdgeClipper();

            AffinePoint start = new AffinePoint(0, 0, 0);
            AffinePoint end = new AffinePoint(0, 0, 2);
            Edge edge = new Edge { Start = start, End = end };

            edgeClipper.ClipEdge(edge);

            Assert.AreEqual(true, start.AlmostEqual(edge.Start));
            Assert.AreEqual(true, end.AlmostEqual(edge.End));
        }

        [Test]
        public void TestEdgeClippingByOZ()
        {
            IEdgeClipper edgeClipper = new LiangBarskyEdgeClipper();

            AffinePoint start = new AffinePoint(0, 0, 0);
            AffinePoint end = new AffinePoint(0, 0, 2);
            Edge edge = new Edge {Start = start, End = end};

            edgeClipper.ClipEdge(edge);

            Edge clippedEdge = edgeClipper.ClippedEdge;
            bool edgeCrossesCvv = edgeClipper.EdgeCrossesCvv;

            Assert.AreEqual(true, edgeCrossesCvv);

            AffinePoint expectedClippedStart = new AffinePoint(0, 0, 0);
            AffinePoint expectedClippedEnd = new AffinePoint(0, 0, 1);

            Assert.AreEqual(true, clippedEdge.Start.AlmostEqual(expectedClippedStart));
            Assert.AreEqual(true, clippedEdge.End.AlmostEqual(expectedClippedEnd));
        }

        [Test]
        public void TestEdgeClippingByOX()
        {
            IEdgeClipper edgeClipper = new LiangBarskyEdgeClipper();

            AffinePoint start = new AffinePoint(0, 0, 0);
            AffinePoint end = new AffinePoint(2, 0, 0);
            Edge edge = new Edge { Start = start, End = end };

            edgeClipper.ClipEdge(edge);

            Edge clippedEdge = edgeClipper.ClippedEdge;
            bool edgeCrossesCvv = edgeClipper.EdgeCrossesCvv;

            Assert.AreEqual(true, edgeCrossesCvv);

            AffinePoint expectedClippedStart = new AffinePoint(0, 0, 0);
            AffinePoint expectedClippedEnd = new AffinePoint(1, 0, 0);

            Assert.AreEqual(true, clippedEdge.Start.AlmostEqual(expectedClippedStart));
            Assert.AreEqual(true, clippedEdge.End.AlmostEqual(expectedClippedEnd));
        }

        [Test]
        public void TestEdgeClippingByOY()
        {
            IEdgeClipper edgeClipper = new LiangBarskyEdgeClipper();

            AffinePoint start = new AffinePoint(0.5, 0, 0);
            AffinePoint end = new AffinePoint(0.5, 2, 0);
            Edge edge = new Edge { Start = start, End = end };

            edgeClipper.ClipEdge(edge);

            Edge clippedEdge = edgeClipper.ClippedEdge;
            bool edgeCrossesCvv = edgeClipper.EdgeCrossesCvv;

            Assert.AreEqual(true, edgeCrossesCvv);

            AffinePoint expectedClippedStart = new AffinePoint(0.5, 0, 0);
            AffinePoint expectedClippedEnd = new AffinePoint(0.5, 1, 0);

            Assert.AreEqual(true, clippedEdge.Start.AlmostEqual(expectedClippedStart));
            Assert.AreEqual(true, clippedEdge.End.AlmostEqual(expectedClippedEnd));
        }

        [Test]
        public void TestEdgeClippingNoIntersection()
        {
            IEdgeClipper edgeClipper = new LiangBarskyEdgeClipper();

            AffinePoint start = new AffinePoint(3, 1, 0);
            AffinePoint end = new AffinePoint(2, 0, 0);
            Edge edge = new Edge { Start = start, End = end };

            edgeClipper.ClipEdge(edge);

            Edge clippedEdge = edgeClipper.ClippedEdge;
            bool edgeCrossesCvv = edgeClipper.EdgeCrossesCvv;

            Assert.AreEqual(false, edgeCrossesCvv);

            Assert.AreEqual(true, clippedEdge.Start.AlmostEqual(start));
            Assert.AreEqual(true, clippedEdge.End.AlmostEqual(end));
        }

        [Test]
        public void TestEdgeClippingNoIntersectionInDifferentPlanes()
        {
            IEdgeClipper edgeClipper = new LiangBarskyEdgeClipper();

            AffinePoint start = new AffinePoint(4, 0, 0);
            AffinePoint end = new AffinePoint(0, 4, 0);
            Edge edge = new Edge { Start = start, End = end };

            edgeClipper.ClipEdge(edge);

            Edge clippedEdge = edgeClipper.ClippedEdge;
            bool edgeCrossesCvv = edgeClipper.EdgeCrossesCvv;

            Assert.AreEqual(false, edgeCrossesCvv);

            Assert.AreEqual(true, clippedEdge.Start.AlmostEqual(start));
            Assert.AreEqual(true, clippedEdge.End.AlmostEqual(end));
        }

        [Test]
        public void TestEdgeClippingFullIntersection()
        {
            IEdgeClipper edgeClipper = new LiangBarskyEdgeClipper();

            AffinePoint start = new AffinePoint(0, 0.9, 0);
            AffinePoint end = new AffinePoint(0.9, 0, 0);
            Edge edge = new Edge { Start = start, End = end };

            edgeClipper.ClipEdge(edge);

            Edge clippedEdge = edgeClipper.ClippedEdge;
            bool edgeCrossesCvv = edgeClipper.EdgeCrossesCvv;

            Assert.AreEqual(true, edgeCrossesCvv);

            Assert.AreEqual(true, clippedEdge.Start.AlmostEqual(start));
            Assert.AreEqual(true, clippedEdge.End.AlmostEqual(end));
        }
    }
}

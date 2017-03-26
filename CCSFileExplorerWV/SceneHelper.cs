using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Platform;

#pragma warning disable 0618

namespace CCSFileExplorerWV
{
    public static class SceneHelper
    {
        public class Vertex
        {
            public float X;
            public float Y;
            public float Z;
            public float U;
            public float V;
            public Vertex(float x, float y, float z, float u, float v)
            {
                X = x;
                Y = y;
                Z = z;
                U = u;
                V = v;
            }
        }

        public static Control ctrl;
        public static bool init = false;
        public static IGraphicsContext context;
        public static IWindowInfo winfo;
        public static float camDist = 30;
        public static float camHeight = 30;
        public static float zoomF = 1;
        public static Matrix4 rotation;
        public static bool doRotate = true;
        public static bool wireframe = false;
        public static Vertex[] vertices = new Vertex[0];

        public static void InitializeDevice(Control c)
        {
            ctrl = c; 
            winfo = Utilities.CreateWindowsWindowInfo(c.Handle);
            context = new GraphicsContext(GraphicsMode.Default, winfo);
            context.MakeCurrent(winfo);
            init = true;
            context.LoadAll();
            GL.ClearColor(0.1f, 0.2f, 0.5f, 0.0f);
            GL.Enable(EnableCap.DepthTest);
            GL.CullFace(CullFaceMode.FrontAndBack);
            Resize();
        }

        public static void Resize()
        {
            GL.Viewport(0, 0, ctrl.Width, ctrl.Height);
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, ctrl.Width / (float)ctrl.Height, 1.0f, 100000f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
        }

        public static void InitScene(List<float[]> triangles)
        {
            List<Vertex> result = new List<Vertex>();
            float minx, miny, minz, maxx, maxy, maxz, dx, dy, dz;
            minx = miny = minz = maxx = maxy = maxz = dx = dy = dz = 0;
            foreach (float[] v in triangles)
            {
                result.Add(new Vertex(v[0], v[2], v[1], v[3], v[4]));
                if (v[0] < minx) minx = v[0];
                if (v[0] > maxx) maxx = v[0];
                if (v[2] < miny) miny = v[2];
                if (v[2] > maxy) maxy = v[2];
                if (v[1] < minz) minz = v[1];
                if (v[1] > maxz) maxz = v[1];
            }
            camDist = (float)Math.Sqrt((maxx - minx) * (maxx - minx) + (maxy - miny) * (maxy - miny) + (maxz - minz) * (maxz - minz));
            camDist *= 1.5f;
            camDist += 1;
            camHeight = camDist;
            dx = -(minx + maxx) / 2;
            dy = -(miny + maxy) / 2;
            dz = -(minz + maxz) / 2;
            for (int i = 0; i < result.Count; i++)
            {
                Vertex tmp = result[i];
                tmp.X += dx;
                tmp.Y += dy;
                tmp.Z += dz;
                result[i] = tmp;
            }
            vertices = result.ToArray();
            SetRotation360(0);
        }
        
        public static void SetRotation360(float r)
        {
            rotation = Matrix4.CreateRotationY(r * (3.1415f / 180f));
        }

        public static void SetHeight(float h)
        {
            camHeight = camDist * h;
        }

        public static void SetZoomFactor(float f)
        {
            if (f <= 0.01f)
                f = 0.01f;
            if (f > 3)
                f = 3;
            zoomF = f;
        }

        public static void Render()
        {
            if (wireframe)
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            else
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Matrix4 cam = Matrix4.LookAt(new Vector3(0, camHeight, -camDist * zoomF), Vector3.Zero, Vector3.UnitY);
            Matrix4 model = rotation * cam;
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref model);
            GL.Begin(BeginMode.Triangles);
            for (int i = 0; i < vertices.Length / 3; i++)
            {
                Vertex v1 = vertices[i * 3];
                Vertex v2 = vertices[i * 3 + 1];
                Vertex v3 = vertices[i * 3 + 2];
                GL.Color3(1.0f, 1.0f, 0.0f); GL.Vertex3(v1.X, v1.Y, v1.Z);
                GL.Color3(1.0f, 0.0f, 0.0f); GL.Vertex3(v2.X, v2.Y, v2.Z);
                GL.Color3(0.2f, 0.9f, 1.0f); GL.Vertex3(v3.X, v3.Y, v3.Z);
            }
            GL.End();
            context.SwapBuffers();
        }
    }
}

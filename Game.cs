using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace DNoise
{
    public class Game : GameWindow
    {
        public List<Cube> cubes = new List<Cube>();
        Camera camera;

        float[] lightPos = { 5000f, 5000, 5000f };
        float[] lightDiffuse = { .25f, .26f, .42f };
        float[] lightAmbient = { .4f, .5f, .6f };


        public Game(List<Vector3> coordinates, float cubeSize, int width, int height)
            : base(width,height,GraphicsMode.Default,"3D Noise") {
            camera = new Camera(this);

            foreach (Vector3 v in coordinates) {
                cubes.Add(new Cube(v, cubeSize, this));
            }

            Run(60);
        }

        protected override void OnRenderFrame(FrameEventArgs e) {

            var view = Matrix4.LookAt(camera.position, camera.position + camera.front, camera.up);
            GL.LoadMatrix(ref view);
            GL.MatrixMode(MatrixMode.Modelview);

            GL.Light(LightName.Light0, LightParameter.Position, lightPos);
            GL.Light(LightName.Light0, LightParameter.Diffuse, lightDiffuse);
            GL.Light(LightName.Light0, LightParameter.Ambient, lightAmbient);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            //GL.Rotate(1f, 0, 1, 0);

            try { 
                foreach (Cube c in cubes) {
                    c.Render();
                }
            }
            catch(Exception e1) {}

            SwapBuffers();
            base.OnRenderFrame(e);
        }

        protected override void OnResize(EventArgs e) {

            GL.Viewport(0, 0, Width, Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            Matrix4 perspectiveMatrix =
                Matrix4.CreatePerspectiveFieldOfView(1, Width / Height, 1.0f, 1000.0f);
            GL.LoadMatrix(ref perspectiveMatrix);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.End();
            base.OnResize(e);
        }

        protected override void OnLoad(EventArgs e) {
            GL.Enable(EnableCap.DepthTest);
            GL.ClearColor(0, 0, 0, 0);
            GL.Enable(EnableCap.Fog);

            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.ColorMaterial);
            GL.Enable(EnableCap.Light0);

            GL.Fog(FogParameter.FogMode, (int)FogMode.Linear);
            GL.Hint(HintTarget.FogHint, HintMode.Nicest);
            GL.Fog(FogParameter.FogColor, new float[] {64/100, 174/100, 230/100});

            GL.Fog(FogParameter.FogStart, (float)1000 / 112);
            GL.Fog(FogParameter.FogEnd, 510);
            base.OnLoad(e);
        }

        public void RemoveCube(Cube cube)
        {
            cubes.Remove(cube);
        }
    }
}

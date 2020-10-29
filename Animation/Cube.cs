using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace DNoise
{
    public class Cube
    {
        public Vector3 position;
        float size;
        bool canRender = true;

        public Cube(Vector3 position, float size) {
            this.position = position;
            this.size = size;
        }

        public void Render()
        {
            if (canRender) { 
                GL.Begin(BeginMode.Quads);
                GL.Color3((double)114 / 255, (double)179 / 255, (double)29 / 255);

                // Front
                GL.Normal3(0, 0, 1);
                GL.Vertex3(-size + position.X,  size + position.Y, size + position.Z);
                GL.Vertex3( size + position.X,  size + position.Y, size + position.Z);
                GL.Vertex3( size + position.X, -size + position.Y, size + position.Z);
                GL.Vertex3(-size + position.X, -size + position.Y, size + position.Z);

                // Back
                GL.Normal3(0, 0, -1);
                GL.Vertex3(-size + position.X,  size + position.Y, -size + position.Z);
                GL.Vertex3( size + position.X,  size + position.Y, -size + position.Z);
                GL.Vertex3( size + position.X, -size + position.Y, -size + position.Z);
                GL.Vertex3(-size + position.X, -size + position.Y, -size + position.Z);

                // Left
                GL.Normal3(-1, 0, 0);
                GL.Vertex3(-size + position.X,  size + position.Y, -size + position.Z);
                GL.Vertex3(-size + position.X,  size + position.Y,  size + position.Z);
                GL.Vertex3(-size + position.X, -size + position.Y,  size + position.Z);
                GL.Vertex3(-size + position.X, -size + position.Y, -size + position.Z);

                // Right
                GL.Normal3(1, 0, 0);
                GL.Vertex3(size + position.X,  size + position.Y, -size + position.Z);
                GL.Vertex3(size + position.X,  size + position.Y,  size + position.Z);
                GL.Vertex3(size + position.X, -size + position.Y,  size + position.Z);
                GL.Vertex3(size + position.X, -size + position.Y, -size + position.Z);

                // Top
                GL.Normal3(0, 1, 0);
                GL.Vertex3(-size + position.X, size + position.Y, -size + position.Z);
                GL.Vertex3(-size + position.X, size + position.Y,  size + position.Z);
                GL.Vertex3( size + position.X, size + position.Y,  size + position.Z);
                GL.Vertex3( size + position.X, size + position.Y, -size + position.Z);

                // Bottom
                GL.Normal3(0, -1, 0);
                GL.Vertex3(-size + position.X, -size + position.Y, -size + position.Z);
                GL.Vertex3(-size + position.X, -size + position.Y,  size + position.Z);
                GL.Vertex3( size + position.X, -size + position.Y,  size + position.Z);
                GL.Vertex3( size + position.X, -size + position.Y, -size + position.Z);
                GL.End();
            }
        }
    }
}

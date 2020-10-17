﻿using System;
using OpenTK;
using OpenTK.Input;

namespace DNoise
{
    public class Camera
    {
        public Vector3 position = new Vector3(-50, 20, 0);
        public Vector3 front = new Vector3(0.0f, 0.0f, -0.001f);
        public Vector3 up = new Vector3(0.0f, .01f, 0.0f);

        bool canRotate;
        
        // xRotation: Up/Down rotation
        // yRotation: Left/Right rotation
        float xRotation, yRotation;
        float speed = 15, sensitivity = .5f;

        Vector2 lastMousePosition;

        
        public Camera(GameWindow window) {
            window.KeyDown += keyDown;
            window.UpdateFrame += update;

            window.MouseDown += mouseDown;
            window.MouseUp += mouseUp;
            window.MouseMove += mouseMove;
        }

        private void update(object sender, FrameEventArgs e)
        {
            xRotation = clamp(xRotation,-89.9f,89.9f);

            front.X = (float)Math.Cos(MathHelper.DegreesToRadians(xRotation)) * (float)Math.Cos(MathHelper.DegreesToRadians(yRotation));
            front.Y = (float)Math.Sin(MathHelper.DegreesToRadians(xRotation));
            front.Z = (float)Math.Cos(MathHelper.DegreesToRadians(xRotation)) * (float)Math.Sin(MathHelper.DegreesToRadians(yRotation));

            front = Vector3.Normalize(front);
        }

        // ROTATING THE CAMERA
        private void mouseMove(object sender, MouseMoveEventArgs e)
        {
            if (canRotate) {
                xRotation += (lastMousePosition.Y - e.Y)*sensitivity;
                yRotation -= (lastMousePosition.X - e.X)*sensitivity;
            }
            lastMousePosition = new Vector2(e.X, e.Y);
        }

        // Checking if the right mouse button has been clicked or not
        private void mouseDown(object sender, MouseButtonEventArgs e) {
            if (e.Mouse.IsButtonDown(MouseButton.Right)) canRotate = true;
        }

        private void mouseUp(object sender, MouseButtonEventArgs e) {
            if (e.Mouse.IsButtonUp(MouseButton.Right)) canRotate = false;
        }

        // moving the camera
        private void keyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.W) position += front * speed;
            if (e.Key == Key.S) position -= front * speed;
        }

        // other function
        private float clamp(float value, float min, float max)
        {
            if (value > max) return max;
            if (value < min) return min;
            return value;
        }
    }
}

﻿using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using VoxelWorld.Classes.Engine;
using VoxelWorld.Classes.Render;
using VoxelWorld.Classes.Render.GUIClasses;

namespace VoxelWorld.Classes
{
    public class Camera
    {
        ColorRect crosshair = new ColorRect(0.0006f);
        Text2D text = new Text2D(new Vector2(-10, 9));

        public Camera()
        {
            Input.OnMouseMove += HandleMouseMove;
        }

        public float moveSpeed = 0.1f;

        public Vector3 rotation = new Vector3(0, 0, 0);
        public Vector3 position = new Vector3(0, 0, 0);

        public void Ready()
        {
            GL.Enable(EnableCap.DepthTest); // Включение буффера глубины

            GL.LoadIdentity();
            float aspectRatio = (float)Window.WindowWidth / Window.WindowHeight;
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90), aspectRatio, 0.1f, 600f);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
            GL.MatrixMode(MatrixMode.Modelview);

            text.Ready();
        }

        public void RenderProcess()
        {
            GL.ClearColor(new Color4(0.7f, 0.7f, 1f, 1.0f)); // Очищение экрана цветом...
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit); // Очищение буффера глубины

            // Рендер GUI
            crosshair.RenderProcess();
            text.RenderProcess();


            // Применение преобразовний
            GL.LoadIdentity();
            GL.Rotate(rotation.X, 1f, 0f, 0f);
            GL.Rotate(rotation.Y, 0f, 1f, 0f);
            GL.Rotate(rotation.Z, 0f, 0f, 1f);
            GL.Translate(-position);
        }

        public void PhysicsProcess()
        {
            // Ходьба
            float YAngle = -rotation.Y / 180f * (float)Math.PI;
            if (Input.IsKeyPressed(Input.KeyMovmentForward))
            {
                position.X -= (float)Math.Sin(YAngle) * moveSpeed;
                position.Z -= (float)Math.Cos(YAngle) * moveSpeed;
            }
            if (Input.IsKeyPressed(Input.KeyMovmentRight))
            {
                position.X += (float)Math.Sin(YAngle + (float)Math.PI * 0.5) * moveSpeed;
                position.Z += (float)Math.Cos(YAngle + (float)Math.PI * 0.5) * moveSpeed;
            }
            if (Input.IsKeyPressed(Input.KeyMovmentLeft))
            {
                position.X += (float)Math.Sin(YAngle - (float)Math.PI * 0.5) * moveSpeed;
                position.Z += (float)Math.Cos(YAngle - (float)Math.PI * 0.5) * moveSpeed;
            }
            if (Input.IsKeyPressed(Input.KeyMovmentBackward)) 
            {
                position.X += (float)Math.Sin(YAngle) * moveSpeed;
                position.Z += (float)Math.Cos(YAngle) * moveSpeed;
            }
            // Прыжок и красться
            if (Input.IsKeyPressed(Input.KeyJump)){
                position.Y += moveSpeed;
            }
            if (Input.IsKeyPressed(Input.KeyCrouch)){
                position.Y -= moveSpeed;
            }

        }
        private void HandleMouseMove(Vector2 mouseRelative)
        {
            // Вращение камерой
            rotation.X += mouseRelative.Y * Input.mouseSensitivity;
            if (rotation.X > 90.0f) rotation.X = 90.0f; // Ограничение от -90 до 90 градусов
            else if (rotation.X < -90.0f) rotation.X = -90.0f;

            rotation.Y += mouseRelative.X * Input.mouseSensitivity;
        }

        public void OnResizeWindow(EventArgs e)
        {
            float aspectRatio = (float)Window.WindowWidth / Window.WindowHeight;
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90), aspectRatio, 0.1f, 1000f);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
            GL.MatrixMode(MatrixMode.Modelview);
        }
    }
}

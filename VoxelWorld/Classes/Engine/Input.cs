using System;
using OpenTK;
using OpenTK.Input;

namespace VoxelWorld.Classes.Engine
{
    class Input
    {
        // Player Control
        public static readonly Key KeyMovmentForward = Key.W;
        public static readonly Key KeyMovmentBackward = Key.S;
        public static readonly Key KeyMovmentLeft = Key.A;
        public static readonly Key KeyMovmentRight = Key.D;
        public static readonly Key KeyJump = Key.Space;
        public static readonly Key KeyCrouch = Key.LShift;
        public static readonly Key KeyRun = Key.LControl;
        public static readonly MouseButton KeyPlaceBlock = MouseButton.Right;
        public static readonly MouseButton KeyRemoveBlock = MouseButton.Left;
        //Misc Control
        public static readonly Key KeyDebugWireframe = Key.F8;

        public static readonly Key KeyA = Key.A;
        public static readonly Key KeyB = Key.B;
        public static readonly Key KeyC = Key.C;
        public static readonly Key KeyD = Key.D;
        public static readonly Key KeyE = Key.E;
        public static readonly Key KeyF = Key.F;
        public static readonly Key KeyG = Key.G;
        public static readonly Key KeyH = Key.H;
        public static readonly Key KeyI = Key.I;
        public static readonly Key KeyJ = Key.J;
        public static readonly Key KeyK = Key.K;
        public static readonly Key KeyL = Key.L;
        public static readonly Key KeyM = Key.M;
        public static readonly Key KeyN = Key.N;
        public static readonly Key KeyO = Key.O;
        public static readonly Key KeyP = Key.P;
        public static readonly Key KeyQ = Key.Q;
        public static readonly Key KeyR = Key.R;
        public static readonly Key KeyS = Key.S;
        public static readonly Key KeyT = Key.T;
        public static readonly Key KeyU = Key.U;
        public static readonly Key KeyV = Key.V;
        public static readonly Key KeyW = Key.W;
        public static readonly Key KeyX = Key.X;
        public static readonly Key KeyY = Key.Y;
        public static readonly Key KeyZ = Key.Z;

        private static KeyboardState currentKeyboardState;
        private static KeyboardState previousKeyboardState;


        public static readonly float mouseSensitivity = 0.2f;

        public delegate void MouseMoveHandler(Vector2 delta);
        public static event MouseMoveHandler OnMouseMove;

        private static MouseState currentMouseState;
        private static MouseState previousMouseState;

        public static void Update()
        {
            // Обновление состояния клавиатуры
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            // Вычисление delta позиции мыши
            Vector2 mouseRelative = new Vector2(currentMouseState.X - previousMouseState.X, currentMouseState.Y - previousMouseState.Y);

            // Вызов события OnMouseMove
            OnMouseMove?.Invoke(mouseRelative);
        }

        
        public static bool IsActionPressed(Key key)
        {
            // Проверка, нажата ли клавиша
            return currentKeyboardState.IsKeyDown(key);
        }
        
        public static bool IsKeyPressed(Key key)
        {
            // Проверка, нажата ли клавиша
            return currentKeyboardState.IsKeyDown(key);
        }

        public static bool IsJustKeyPressed(Key key)
        {
            // Проверка, была ли клавиша нажата только что (в текущем кадре)
            return currentKeyboardState.IsKeyDown(key) && !previousKeyboardState.IsKeyDown(key);
        }
        public static bool IsKeyJustReleased(Key key)
        {
            // Проверка, была ли клавиша отпущена только что (в текущем кадре)
            return !currentKeyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyDown(key);
        }

        public static bool IsMouseButtonPressed(MouseButton button)
        {
            return currentMouseState.IsButtonDown(button);
        }

        public static bool IsMouseButtonJustPressed(MouseButton button)
        {
            return currentMouseState.IsButtonDown(button) && !previousMouseState.IsButtonDown(button);
        }

        public static bool IsMouseButtonJustReleased(MouseButton button)
        {
            return !currentMouseState.IsButtonDown(button) && previousMouseState.IsButtonDown(button);
        }

    }
}

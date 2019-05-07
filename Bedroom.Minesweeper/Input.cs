using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Bedroom.Minesweeper
{
    public static class Input
    {
        #region Private Fields

        private static Dictionary<Keys, List<Action>> axisSet;

        private static Dictionary<Keys, List<Action>> clickSet;

        private static KeyboardState currentKeyboardState;

        private static MouseState currentMouseState;

        #endregion Private Fields

        #region Public Delegates

        public delegate void MouseClickEventHandler(Point point);

        #endregion Public Delegates

        #region Public Events

        public static event MouseClickEventHandler LeftClicked;

        public static event MouseClickEventHandler MiddleClicked;

        public static event MouseClickEventHandler RightClicked;

        #endregion Public Events

        #region Public Methods

        public static void Init()
        {
            axisSet = new Dictionary<Keys, List<Action>>();
            clickSet = new Dictionary<Keys, List<Action>>();
        }

        public static void RegisterAxis(Keys key, Action action) => Register(key, action, axisSet);

        public static void RegisterClick(Keys key, Action action) => Register(key, action, clickSet);

        public static void UnregisterAxis(Keys key, Action action) => Unregister(key, action, axisSet);

        public static void UnregisterClick(Keys key, Action action) => Unregister(key, action, clickSet);

        public static void Update()
        {
            KeyboardState lastKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            MouseState lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            Keys[] currentKeys = currentKeyboardState.GetPressedKeys();
            for (int i = 0; i < currentKeys.Length; i++)
            {
                if (axisSet.ContainsKey(currentKeys[i]))
                    axisSet[currentKeys[i]].ForEach(a => a());

                if (clickSet.ContainsKey(currentKeys[i]) && lastKeyboardState.IsKeyDown(currentKeys[i]))
                    clickSet[currentKeys[i]].ForEach(a => a());
            }

            if (currentMouseState.LeftButton == ButtonState.Pressed
                && currentMouseState.LeftButton == lastMouseState.LeftButton)
                OnLeftClicked();

            if (currentMouseState.RightButton == ButtonState.Pressed
                && currentMouseState.RightButton == lastMouseState.RightButton)
                OnRightClicked();

            if (currentMouseState.MiddleButton == ButtonState.Pressed
                && currentMouseState.MiddleButton == lastMouseState.MiddleButton)
                OnMiddleClicked();
        }

        #endregion Public Methods

        #region Private Methods

        private static void OnLeftClicked()
        {
            LeftClicked?.Invoke(currentMouseState.Position);
        }

        private static void OnMiddleClicked()
        {
            MiddleClicked?.Invoke(currentMouseState.Position);
        }

        private static void OnRightClicked()
        {
            RightClicked?.Invoke(currentMouseState.Position);
        }

        private static void Register(Keys key, Action action, Dictionary<Keys, List<Action>> dataSet)
        {
            if (!dataSet.ContainsKey(key))
                dataSet.Add(key, new List<Action>());
            dataSet[key].Add(action);
        }

        private static void Unregister(Keys key, Action action, Dictionary<Keys, List<Action>> dataSet)
        {
            if (dataSet.ContainsKey(key))
            {
                dataSet[key].Remove(action);
                if (dataSet[key].Count == 0)
                    dataSet.Remove(key);
            }
        }

        #endregion Private Methods
    }
}
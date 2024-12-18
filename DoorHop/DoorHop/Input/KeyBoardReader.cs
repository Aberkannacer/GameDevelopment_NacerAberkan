using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorHop.Input
{
    class KeyBoardReader : IInputReader
    {
        private KeyboardState previousKeyboardState;

        public KeyBoardReader()
        {
            previousKeyboardState = Keyboard.GetState();
        }

        public Vector2 ReadInput()
        {
            KeyboardState state = Keyboard.GetState();
            Vector2 direction = Vector2.Zero;

            if (state.IsKeyDown(Keys.Left)) direction.X = -1;
            else if (state.IsKeyDown(Keys.Right)) direction.X = 1;


            return direction;
        }

        public bool IsJumpKeyPressed()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            return keyboardState.IsKeyDown(Keys.Space);
        }

        public bool IsAttackButtonPressed()
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();
            
            // Alleen true als de toets NET is ingedrukt (was niet ingedrukt in vorige frame)
            bool isPressed = currentKeyboardState.IsKeyDown(Keys.E) && 
                            !previousKeyboardState.IsKeyDown(Keys.E);
            
            previousKeyboardState = currentKeyboardState;
            return isPressed;
        }
    }
}



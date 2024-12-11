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
        public Vector2 ReadInput()
        {
            KeyboardState state = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();
            Vector2 direction = Vector2.Zero;

            if (state.IsKeyDown(Keys.Left)) direction.X = -1;
            else if (state.IsKeyDown(Keys.Right)) direction.X = 1;

            if (mouseState.RightButton == ButtonState.Pressed) ;

            return direction;
        }

        public bool IsJumpKeyPressed()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            //NOG AANPASSEN //Keyboard.GetState().IsKeyDown(Keys.Space)
            return keyboardState.IsKeyDown(Keys.Space);
        }

        public bool IsAttackButtonPressed()
        {
            MouseState mouseState = Mouse.GetState();
            return mouseState.LeftButton == ButtonState.Pressed;
        }
    }
}



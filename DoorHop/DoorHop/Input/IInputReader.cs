using Microsoft.Xna.Framework;

namespace DoorHop.Input
{
    public interface IInputReader
    {
        Vector2 ReadInput();
        bool IsJumpKeyPressed();
        bool IsAttackButtonPressed();
    }
}

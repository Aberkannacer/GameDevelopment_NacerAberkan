using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorHop.Input
{
    public interface IInputReader
    {
        Vector2 ReadInput();
        bool IsJumpKeyPressed();
    }
}

using DoorHop.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorHop.Players.Hero
{
    public class Hero : Player
    {
        public Hero(ContentManager content, IInputReader inputReader) : base(content, inputReader)
        {

        }

        protected override void LoadContent(ContentManager contentManager)
        {
            throw new NotImplementedException();
        }
    }

}

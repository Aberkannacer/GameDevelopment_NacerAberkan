using DoorHop.Animation;
using DoorHop.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DoorHop.Players.Hero
{
    public class Hero : Player
    {
        private Animatie animatie;
        public Hero(ContentManager content, IInputReader inputReader) : base(content, inputReader)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime, List<TileMap.CollisionTiles> tiles)
        {
            base.Update(gameTime, tiles);
        }

        protected override void CheckXCollision(List<TileMap.CollisionTiles> tiles)
        {
            base.CheckXCollision(tiles);
        }

        protected override void CheckYCollision(List<TileMap.CollisionTiles> tiles)
        {
            base.CheckYCollision(tiles);
        }

        protected override void HandleInput()
        {
            base.HandleInput();
        }

        protected override void LoadContent(ContentManager contentManager)
        {
            animatie = new Animatie(contentManager.Load<Texture2D>("Player"), true);
        }

        protected override void SetAnimationSpeed(float speed)
        {
            base.SetAnimationSpeed(speed);
        }

        protected override void SetJumpForce(float force)
        {
            base.SetJumpForce(force);
        }

        protected override void SetMoveSpeed(float speed)
        {
            base.SetMoveSpeed(speed);
        }
    }

}

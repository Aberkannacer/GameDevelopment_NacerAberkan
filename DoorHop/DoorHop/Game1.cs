using DoorHop.Players;
using DoorHop.Players.Enemys;
using DoorHop.Players.Heros;
using DoorHop.TileMap;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using System.Linq;
using DoorHop.Input;
using System.Diagnostics;
using DoorHop.Collectables;
using DoorHop.GameStates;
using Microsoft.Xna.Framework.Media;

namespace DoorHop
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch _spriteBatch;
    
        private Texture2D backgroundTexture;
        private Rectangle backgroundRect;


        private State currentState;
        private State nextState;

        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800; // Stel de breedte in
            graphics.PreferredBackBufferHeight = 480;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }
        

        protected override void Initialize()
        {
            base.Initialize();
            ChangeState(new MenuState(this, Content, graphics.GraphicsDevice));


        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            currentState = new MenuState(this, Content, GraphicsDevice);
            currentState.LoadContent();


            /*Tiles.Content = Content;*/

            backgroundTexture = Content.Load<Texture2D>("background");

            
            


        }
        protected override void Update(GameTime gameTime)
        {
            {
                if (nextState != null)
                {
                    currentState = nextState;
                    currentState.LoadContent();

                    nextState = null;
                }

                currentState.Update(gameTime);

                currentState.PostUpdate(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            _spriteBatch.Begin();

            currentState.Draw(gameTime, _spriteBatch);

            // Teken eerst de achtergrond
            _spriteBatch.Draw(backgroundTexture, backgroundRect, Color.White);

            


            _spriteBatch.End();

            base.Draw(gameTime);
        }

        
        public void ChangeState(State state)
        {
            nextState = state;
        }
        
    }
}
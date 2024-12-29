using DoorHop.Players.Heros;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace DoorHop.GameStates
{
    internal class GameWonState : State
    {
        //hero
        private Hero hero;
        //rectangle en texture
        private Texture2D gameWinTexture;
        private SpriteFont font;
        //timer
        private float timer;
        private float transitionTime;
        //song
        private Song backgroundMusic;
        public GameWonState(Game1 game, GraphicsDevice graphicsDevice,ContentManager content, Hero hero) : base(game, content)
        {
            this.hero = hero;
            this.game = game;
            transitionTime = 5.5f;
            backgroundRect = new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);
        }
        public override void LoadContent()
        {
            font = content.Load<SpriteFont>("MyFont");
            gameWinTexture = content.Load<Texture2D>("WonScreen");
            backgroundMusic = content.Load<Song>("BackgroundMusic");
        }
        public override void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer >= transitionTime)
            {
                BackgroundMusic();
                //terug naar menu
                game.ChangeState(new MenuState(game, content, game.GraphicsDevice));
            }
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(gameWinTexture, backgroundRect, Color.White);
            spriteBatch.DrawString(font, $"Total Score: {hero.TotalScore}", new Vector2(350, 20), Color.White);
        }
        public void BackgroundMusic()
        {
            MediaPlayer.Play(backgroundMusic);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.1f;
        }
    }
}

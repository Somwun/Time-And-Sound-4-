using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Time_And_Sound___4_
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _bomb, _boom;
        private bool visible = false;
        Rectangle bombRect, boomRect;
        SpriteFont timeFont;
        float seconds = 0, startTime = 0, time = 5;
        MouseState mouseState;
        SoundEffect boom;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        protected override void Initialize()
        {
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.ApplyChanges();
            bombRect = new Rectangle(0, 50, 800, 400);
            boomRect = new Rectangle(0, 0, 800, 500);
            base.Initialize();
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _bomb = Content.Load<Texture2D>("bomb");
            _boom = Content.Load<Texture2D>("explosionPic");
            boom = Content.Load<SoundEffect>("explosion");
            timeFont = Content.Load<SpriteFont>("Time");
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed) //When left click, reset timer
                startTime = (float)gameTime.TotalGameTime.TotalSeconds;
            seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;
            if (seconds >= time-0.1) //When timer stops
            {
                boom.Play();
                if (visible == false)
                    visible = true;
                else if (visible)
                    Exit();
                startTime = (float)gameTime.TotalGameTime.TotalSeconds;
            }
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            if (visible == false)
            {
                _spriteBatch.Draw(_bomb, bombRect, Color.White);
                _spriteBatch.DrawString(timeFont, (time - seconds).ToString("00.0"), new Vector2(270, 200), Color.Black);
            }
            else if (visible == true)
            {
                _spriteBatch.Draw(_boom, boomRect, Color.White);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
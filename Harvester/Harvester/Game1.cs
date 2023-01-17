using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Harvester.Sprites;
using System.Collections.Generic;

namespace Harvester
{

    public enum SpriteType
    {
        Farmer,
        Grim,
        Vampire,
        Combine,
        Tomato

    }
    public class Game1 : Game
    {
       
        // Textures
        Texture2D farmerTexture;
        Texture2D grimTexture;
        Texture2D vampireTexture;
        Texture2D combineRightTexture;
        Texture2D combineLeftTexture;
        Texture2D tomatoTexture;
        Texture2D fieldTexture;

        float farmerHealth;

        Vector2 fieldStretch;
        SpriteFont font;
        SpriteFont gameoverFont;
        bool gameover;
        string screenText;
        int score;
        float speedMultiplier;

        private List<Sprite> _sprites;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        

        public static int ScreenWidth = 1280;
        public static int ScreenHeight = 720;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = ScreenWidth;
            _graphics.PreferredBackBufferHeight = ScreenHeight;
            this.Window.AllowUserResizing = false;
            this.Window.Title = "Harvester";

            _graphics.ApplyChanges();
            base.Initialize();
        }

        void ResetVariables()
        {
            score = 0;
            farmerHealth = 1000;
            gameover = false;
            speedMultiplier = 1;
            foreach(Sprite sprite in _sprites)
            {
                sprite.Reset();
            }
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            farmerTexture = Content.Load<Texture2D>("Farmer");
            grimTexture = Content.Load<Texture2D>("grim reaper");
            vampireTexture = Content.Load<Texture2D>("vampire");
            combineRightTexture = Content.Load<Texture2D>("combine harvester");
            combineLeftTexture = Content.Load<Texture2D>("combine harvester left");
            tomatoTexture = Content.Load<Texture2D>("tomato");
            fieldTexture = Content.Load<Texture2D>("field");
            fieldStretch = new Vector2((float)ScreenWidth / (float)fieldTexture.Width,
                                       (float)ScreenHeight / (float)fieldTexture.Height);
            font = Content.Load<SpriteFont>("screen text");
            gameoverFont = Content.Load<SpriteFont>("game over");

            
            _sprites = new List<Sprite>();
            _sprites.Add(new Vampire(vampireTexture, ScreenWidth, ScreenHeight));
            for (int i = 1; i <= 3; i++)
            {
                _sprites.Add(new Tomato(tomatoTexture, ScreenWidth, ScreenHeight));
            }
            for (int i = 1; i <= 4; i++)
            {
                _sprites.Add(new Grim(grimTexture, ScreenWidth, ScreenHeight));
            }
            for (int i = 1; i <= 3; i++)
            {
                _sprites.Add(new Combine(combineRightTexture, ScreenWidth, ScreenHeight, combineLeftTexture));
            }
            _sprites.Add(new Farmer(farmerTexture, ScreenWidth, ScreenHeight));
            ResetVariables();

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            var kstate = Keyboard.GetState();

            if (!gameover){
                foreach (Sprite sprite in _sprites)
                {
                    sprite.Update(gameTime, _sprites, speedMultiplier);
                }
                score += (int)((float)gameTime.ElapsedGameTime.TotalSeconds * 1000);
                foreach (Sprite sprite in _sprites)
                {
                    if (sprite._type == SpriteType.Farmer)
                    {
                        foreach (Sprite sprite2 in _sprites)
                        {
                            if (sprite2._type == SpriteType.Grim || sprite2._type == SpriteType.Vampire || sprite2._type == SpriteType.Combine)
                            {
                                if (sprite.Rectangle.Intersects(sprite2.Rectangle))
                                {
                                    farmerHealth -= (float)gameTime.ElapsedGameTime.TotalSeconds * sprite2._collisionDamage;
                                }

                            }
                            if (sprite2._type == SpriteType.Tomato)
                            {
                                if (sprite.Rectangle.Intersects(sprite2.Rectangle))
                                {
                                    farmerHealth -= sprite2._collisionDamage;
                                    sprite2.Reset();
                                }
                            }
                        }
                    }
                }
                farmerHealth = Math.Max(farmerHealth, 0);
                if (farmerHealth <= 0) { gameover = true; }
                speedMultiplier += ((float)gameTime.ElapsedGameTime.TotalSeconds / 20);
            }

            screenText = "Farmer Health: " + ((int)farmerHealth).ToString() + "  score: " + score.ToString();

            if (kstate.IsKeyDown(Keys.Space) && gameover)
            {
                ResetVariables();
            }
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(fieldTexture, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, fieldStretch, SpriteEffects.None, 0f);
            _spriteBatch.DrawString(font, screenText, new Vector2(_graphics.PreferredBackBufferWidth * 1 / 5, 50), Color.Black);
            if (gameover) {
                _spriteBatch.DrawString(gameoverFont, "GAME OVER", new Vector2(_graphics.PreferredBackBufferWidth * 1 / 5, _graphics.PreferredBackBufferHeight * 1 / 4), Color.Red);
                _spriteBatch.DrawString(gameoverFont, "press space", new Vector2(_graphics.PreferredBackBufferWidth * 1 / 4, _graphics.PreferredBackBufferHeight * 2 / 4), Color.Red);
            }
            foreach(Sprite sprite in _sprites)
            {
                sprite.Draw(_spriteBatch);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

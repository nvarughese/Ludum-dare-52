using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Harvester.Sprites;
using System.Collections.Generic;

namespace Harvester
{
    public class Game1 : Game
    {
       
        // Textures
        Texture2D farmerTexture;
        Texture2D grimTexture;
        Texture2D vampireTexture;
        Texture2D harvesterRightTexture;
        Texture2D harvesterLeftTexture;
        Texture2D tomatoTexture;
        Texture2D fieldTexture;




        float farmerHealth;

        
        int grimStopBuffer;
        Vector2 harvester1Position;
        Vector2 harvester2Position;
        bool harvester1GoingRight;
        bool harvester2GoingRight;
        float harvester1Speed;
        float harvester2Speed;
        float harvesterDamage;
        Vector2 tomato1Position;
        Vector2 tomato2Position;
        Vector2 tomato3Position;
        float tomatoHeal;
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
            
            


            
            harvester1Position = new Vector2(_graphics.PreferredBackBufferWidth * 2 / 5, _graphics.PreferredBackBufferHeight * 1 / 3);
            harvester2Position = new Vector2(_graphics.PreferredBackBufferWidth * 4 / 5, _graphics.PreferredBackBufferHeight * 2 / 3);
            harvester1GoingRight = true;
            harvester2GoingRight = false;
            harvester1Speed = 10;
            harvester2Speed = 15;
            harvesterDamage = 400;
            tomato1Position = new Vector2(_graphics.PreferredBackBufferWidth * 1 / 5, _graphics.PreferredBackBufferHeight * 3 / 4);
            tomato2Position = new Vector2(_graphics.PreferredBackBufferWidth * 3 / 8, _graphics.PreferredBackBufferHeight * 5 / 8);
            tomato3Position = new Vector2(_graphics.PreferredBackBufferWidth * 7 / 8, _graphics.PreferredBackBufferHeight * 6 / 8);
            tomatoHeal = 100;
            score = 0;
            // should probably be inside farmer but easier to have it outside
            farmerHealth = 1000;
            gameover = false;
            
            speedMultiplier = 1;

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            farmerTexture = Content.Load<Texture2D>("Farmer");
            grimTexture = Content.Load<Texture2D>("grim reaper");
            vampireTexture = Content.Load<Texture2D>("vampire");
            harvesterRightTexture = Content.Load<Texture2D>("combine harvester");
            harvesterLeftTexture = Content.Load<Texture2D>("combine harvester left");
            tomatoTexture = Content.Load<Texture2D>("tomato");
            fieldTexture = Content.Load<Texture2D>("field");
            fieldStretch = new Vector2((float)ScreenWidth / (float)fieldTexture.Width,
                                       (float)ScreenHeight / (float)fieldTexture.Height);
            font = Content.Load<SpriteFont>("screen text");
            gameoverFont = Content.Load<SpriteFont>("game over");

            // Sprite objects
            Vampire vampire = new Vampire(vampireTexture, ScreenWidth, ScreenHeight);
            Farmer farmer = new Farmer(farmerTexture, ScreenWidth, ScreenHeight);
            Grim grim1 = new Grim(grimTexture, ScreenWidth, ScreenHeight);
            Grim grim2 = new Grim(grimTexture, ScreenWidth, ScreenHeight);
            Grim grim3 = new Grim(grimTexture, ScreenWidth, ScreenHeight);
            Grim grim4 = new Grim(grimTexture, ScreenWidth, ScreenHeight);

            _sprites = new List<Sprite>();
            _sprites.Add(vampire);
            _sprites.Add(farmer);
            _sprites.Add(grim1);
            _sprites.Add(grim2);
            _sprites.Add(grim3);
            _sprites.Add(grim4);

            ResetVariables();

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            var kstate = Keyboard.GetState();

            /*
            // move the farmer using the arrow keys
            if (kstate.IsKeyDown(Keys.Up)){
                farmerPosition.Y -= (float)gameTime.ElapsedGameTime.TotalSeconds * farmerSpeed;}
            if (kstate.IsKeyDown(Keys.Down)){
                farmerPosition.Y += (float)gameTime.ElapsedGameTime.TotalSeconds * farmerSpeed;}
            if (kstate.IsKeyDown(Keys.Left)){
                farmerPosition.X -= (float)gameTime.ElapsedGameTime.TotalSeconds * farmerSpeed;}
            if (kstate.IsKeyDown(Keys.Right)){
                farmerPosition.X += (float)gameTime.ElapsedGameTime.TotalSeconds * farmerSpeed;}

            // keep farmer within the screen
            if (farmerPosition.Y < farmerTexture.Height / 2){
                farmerPosition.Y = farmerTexture.Height / 2;}
            else if (farmerPosition.Y > _graphics.PreferredBackBufferHeight - farmerTexture.Height / 2){
                farmerPosition.Y = _graphics.PreferredBackBufferHeight - farmerTexture.Height / 2;}
            if (farmerPosition.X < farmerTexture.Width / 2){
                farmerPosition.X = farmerTexture.Width / 2;}
            else if (farmerPosition.X > _graphics.PreferredBackBufferWidth - farmerTexture.Width / 2){
                farmerPosition.X = _graphics.PreferredBackBufferWidth - farmerTexture.Width / 2;}
            */

            // should be done with classes but didn't have time
            // move grim1
            /*
            Random rand = new Random();
            grim1Direction.X += rand.Next(-5, 6);
            grim1Direction.Y += rand.Next(-5, 6);
            grim1Direction.X = (int) (grim1Direction.X * grimDampening);
            grim1Direction.Y = (int) (grim1Direction.Y * grimDampening);
            int direction_x = Math.Abs(grim1Direction.X) <= grimStopBuffer ? 0 : (int) grim1Direction.X;
            grim1Position.X += (direction_x * speedMultiplier);
            int direction_y = Math.Abs(grim1Direction.Y) <= grimStopBuffer ? 0 : (int) grim1Direction.Y;
            grim1Position.Y += (direction_y * speedMultiplier);

            // keep grim within the screen and bounce off walls
            if (grim1Position.Y < grimTexture.Height / 2){   
                grim1Position.Y = grimTexture.Height / 2;
                grim1Direction.Y = Math.Abs(grim1Direction.Y);}
            else if (grim1Position.Y > _graphics.PreferredBackBufferHeight - grimTexture.Height / 2)
            {   grim1Position.Y = _graphics.PreferredBackBufferHeight - grimTexture.Height / 2;
                grim1Direction.Y = -Math.Abs(grim1Direction.Y);}
            if (grim1Position.X < grimTexture.Width / 2){   
                grim1Position.X = grimTexture.Width / 2;
                grim1Direction.X = Math.Abs(grim1Direction.X);}
            else if (grim1Position.X > _graphics.PreferredBackBufferWidth - grimTexture.Width / 2)
            {   grim1Position.X = _graphics.PreferredBackBufferWidth - grimTexture.Width / 2;
                grim1Direction.X = -Math.Abs(grim1Direction.X);
            }
            */

            

            

            // move harvesters
            harvester1Position.X += harvester1GoingRight ? harvester1Speed * speedMultiplier : -harvester1Speed * speedMultiplier;
            harvester2Position.X += harvester2GoingRight ? harvester2Speed * speedMultiplier : -harvester2Speed * speedMultiplier;

            // keep harvesters within screen
            if (harvester1Position.X < harvesterRightTexture.Width / 2){
                harvester1Position.X = harvesterRightTexture.Width / 2;
                harvester1GoingRight = true;
            }
            else if (harvester1Position.X > _graphics.PreferredBackBufferWidth - harvesterRightTexture.Width / 2){
                harvester1Position.X = _graphics.PreferredBackBufferWidth - harvesterRightTexture.Width / 2;
                harvester1GoingRight = false;
            }
            if (harvester2Position.X < harvesterRightTexture.Width / 2)
            {
                harvester2Position.X = harvesterRightTexture.Width / 2;
                harvester2GoingRight = true;
            }
            else if (harvester2Position.X > _graphics.PreferredBackBufferWidth - harvesterRightTexture.Width / 2)
            {
                harvester2Position.X = _graphics.PreferredBackBufferWidth - harvesterRightTexture.Width / 2;
                harvester2GoingRight = false;
            }

            /*
             * take out all collision stuff for the moment, can put back in later

            // consider collisions and update farmer health
            Rectangle farmerRect = new Rectangle((int)farmerPosition.X, (int)farmerPosition.Y, farmerTexture.Width, farmerTexture.Height);
            Rectangle grim1Rect = new Rectangle((int)grim1Position.X, (int)grim1Position.Y, grimTexture.Width, grimTexture.Height);
            Rectangle grim2Rect = new Rectangle((int)grim2Position.X, (int)grim2Position.Y, grimTexture.Width, grimTexture.Height);
            Rectangle grim3Rect = new Rectangle((int)grim3Position.X, (int)grim3Position.Y, grimTexture.Width, grimTexture.Height);
            //Rectangle vampireRect = new Rectangle((int)vampire._position.X, (int)vampire._position.Y, vampireTexture.Width, vampireTexture.Height);
            Rectangle tomato1Rect = new Rectangle((int)tomato1Position.X, (int)tomato1Position.Y, tomatoTexture.Width, tomatoTexture.Height);
            Rectangle tomato2Rect = new Rectangle((int)tomato2Position.X, (int)tomato2Position.Y, tomatoTexture.Width, tomatoTexture.Height);
            Rectangle tomato3Rect = new Rectangle((int)tomato3Position.X, (int)tomato3Position.Y, tomatoTexture.Width, tomatoTexture.Height);
            Rectangle harvester1Rect = new Rectangle((int)harvester1Position.X, (int)harvester1Position.Y, harvesterRightTexture.Width, harvesterRightTexture.Height);
            Rectangle harvester2Rect = new Rectangle((int)harvester2Position.X, (int)harvester2Position.Y, harvesterRightTexture.Width, harvesterRightTexture.Height);
            if (farmerRect.Intersects(grim1Rect)){
                farmerHealth -= (float)gameTime.ElapsedGameTime.TotalSeconds * grimDamage;
            }
            if (farmerRect.Intersects(grim2Rect)){
                farmerHealth -= (float)gameTime.ElapsedGameTime.TotalSeconds * grimDamage;
            }
            if (farmerRect.Intersects(grim3Rect)){
                farmerHealth -= (float)gameTime.ElapsedGameTime.TotalSeconds * grimDamage;
            }
            if (farmerRect.Intersects(vampireRect)){
                farmerHealth -= (float)gameTime.ElapsedGameTime.TotalSeconds * vampireDamage;
            }
            if (farmerRect.Intersects(harvester1Rect)){
                farmerHealth -= (float)gameTime.ElapsedGameTime.TotalSeconds * harvesterDamage;
            }
            if (farmerRect.Intersects(harvester2Rect))
            {
                farmerHealth -= (float)gameTime.ElapsedGameTime.TotalSeconds * harvesterDamage;
            }
            if (farmerRect.Intersects(tomato1Rect)){
                farmerHealth += tomatoHeal;
                tomato1Position.X = rand.Next(tomatoTexture.Width / 2, _graphics.PreferredBackBufferWidth - tomatoTexture.Width / 2);
                tomato1Position.Y = rand.Next(tomatoTexture.Height / 2, _graphics.PreferredBackBufferHeight - tomatoTexture.Height / 2);
                Debug.WriteLine("tomato position: " + tomato1Position.X.ToString() + ", " + tomato1Position.Y.ToString());
            }
            if (farmerRect.Intersects(tomato2Rect)){
                farmerHealth += tomatoHeal;
                tomato2Position.X = rand.Next(tomatoTexture.Width / 2, _graphics.PreferredBackBufferWidth - tomatoTexture.Width / 2);
                tomato2Position.Y = rand.Next(tomatoTexture.Height / 2, _graphics.PreferredBackBufferHeight - tomatoTexture.Height / 2);
                Debug.WriteLine("tomato position: " + tomato2Position.X.ToString() + ", " + tomato2Position.Y.ToString());
            }
            if (farmerRect.Intersects(tomato3Rect)){
                farmerHealth += tomatoHeal;
                tomato3Position.X = rand.Next(tomatoTexture.Width / 2, _graphics.PreferredBackBufferWidth - tomatoTexture.Width / 2);
                tomato3Position.Y = rand.Next(tomatoTexture.Height / 2, _graphics.PreferredBackBufferHeight - tomatoTexture.Height / 2);
                Debug.WriteLine("tomato position: " + tomato3Position.X.ToString() + ", " + tomato3Position.Y.ToString());
            }
            */

            foreach (Sprite sprite in _sprites)
            {
                sprite.Update(gameTime, _sprites, speedMultiplier);
            }

            if (!gameover){
                score += (int)((float)gameTime.ElapsedGameTime.TotalSeconds * 1000);
            }
            speedMultiplier += ((float)gameTime.ElapsedGameTime.TotalSeconds / 20);

            screenText = "Farmer Health: " + ((int)farmerHealth).ToString() + "  score: " + score.ToString();

            

            if (farmerHealth < 0) { gameover = true; }
            if (kstate.IsKeyDown(Keys.Space) && gameover)
            {
                this.ResetVariables();
            }
            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(fieldTexture, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, fieldStretch, SpriteEffects.None, 0f);
            //_spriteBatch.Draw(grimTexture, grim1Position, null, Color.White, 0f, new Vector2(grimTexture.Width / 2, grimTexture.Height / 2), Vector2.One, SpriteEffects.None, 0f);
            //_spriteBatch.Draw(grimTexture, grim2Position, null, Color.White, 0f, new Vector2(grimTexture.Width / 2, grimTexture.Height / 2), Vector2.One, SpriteEffects.None, 0f);
            //_spriteBatch.Draw(grimTexture, grim3Position, null, Color.White, 0f, new Vector2(grimTexture.Width / 2, grimTexture.Height / 2), Vector2.One, SpriteEffects.None, 0f);
            //_spriteBatch.Draw(vampireTexture, vampirePosition, null, Color.White, 0f, new Vector2(vampireTexture.Width / 2, vampireTexture.Height / 2), Vector2.One, SpriteEffects.None, 0f);
            if (harvester1GoingRight){
                _spriteBatch.Draw(harvesterRightTexture, harvester1Position, null, Color.White, 0f, new Vector2(harvesterRightTexture.Width / 2, harvesterRightTexture.Height / 2), Vector2.One, SpriteEffects.None, 0f);
            } else {
                _spriteBatch.Draw(harvesterLeftTexture, harvester1Position, null, Color.White, 0f, new Vector2(harvesterRightTexture.Width / 2, harvesterRightTexture.Height / 2), Vector2.One, SpriteEffects.None, 0f);
            }
            if (harvester2GoingRight){
                _spriteBatch.Draw(harvesterRightTexture, harvester2Position, null, Color.White, 0f, new Vector2(harvesterRightTexture.Width / 2, harvesterRightTexture.Height / 2), Vector2.One, SpriteEffects.None, 0f);
            } else {
                _spriteBatch.Draw(harvesterLeftTexture, harvester2Position, null, Color.White, 0f, new Vector2(harvesterRightTexture.Width / 2, harvesterRightTexture.Height / 2), Vector2.One, SpriteEffects.None, 0f);
            }
            
            _spriteBatch.Draw(tomatoTexture, tomato1Position, null, Color.White, 0f, new Vector2(tomatoTexture.Width / 2, tomatoTexture.Height / 2), Vector2.One, SpriteEffects.None, 0f);
            _spriteBatch.Draw(tomatoTexture, tomato2Position, null, Color.White, 0f, new Vector2(tomatoTexture.Width / 2, tomatoTexture.Height / 2), Vector2.One, SpriteEffects.None, 0f);
            _spriteBatch.Draw(tomatoTexture, tomato3Position, null, Color.White, 0f, new Vector2(tomatoTexture.Width / 2, tomatoTexture.Height / 2), Vector2.One, SpriteEffects.None, 0f);
            //_spriteBatch.Draw(farmerTexture, farmerPosition, null, Color.White, 0f, new Vector2(farmerTexture.Width / 2, farmerTexture.Height / 2), Vector2.One, SpriteEffects.None, 0f);
            _spriteBatch.DrawString(font, screenText, new Vector2(_graphics.PreferredBackBufferWidth * 1 / 5, 50), Color.Black);
            if (farmerHealth <= 0) {
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

/*
 * todo
 * 
 * make vampire follow farmer
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Harvester.Sprites;
using System.Diagnostics;

namespace Harvester.Sprites
{
    internal class Farmer : Sprite
    {
        public Farmer(Texture2D texture, int screenWidth, int screenHeight) : base(texture, screenWidth, screenHeight)
        {
            _type = SpriteType.Farmer;
            _speed = 400;
            Reset();
        }


        public override void Update(GameTime gameTime, List<Sprite> sprites, float speedMultiplier)
        {
            Move(gameTime.ElapsedGameTime.TotalSeconds);

            KeepWithinBounds();
        }

        public void Move(double elapsedTime)
        {
            _currentKey = Keyboard.GetState();
            // move the farmer using the arrow keys
            if (_currentKey.IsKeyDown(Keys.Up))
            {
                Debug.WriteLine("pressed up button");
                _position.Y -= (float)elapsedTime * _speed;
            }
            if (_currentKey.IsKeyDown(Keys.Down))
            {
                _position.Y += (float)elapsedTime * _speed;
            }
            if (_currentKey.IsKeyDown(Keys.Left))
            {
                _position.X -= (float)elapsedTime * _speed;
            }
            if (_currentKey.IsKeyDown(Keys.Right))
            {
                _position.X += (float)elapsedTime * _speed;
            }
        }

        public void KeepWithinBounds()
        {
            if (_position.Y < _texture.Height / 2)
            {
                _position.Y = _texture.Height / 2;
            }
            else if (_position.Y > _screenHeight - _texture.Height / 2)
            {
                _position.Y = _screenHeight - _texture.Height / 2;
            }
            if (_position.X < _texture.Width / 2)
            {
                _position.X = _texture.Width / 2;
            }
            else if (_position.X > _screenWidth - _texture.Width / 2)
            {
                _position.X = _screenWidth - _texture.Width / 2;
            }
        }

        public override void Reset()
        {
            _position = new Vector2(_screenWidth / 2, _screenHeight / 2);
        }


    }
}

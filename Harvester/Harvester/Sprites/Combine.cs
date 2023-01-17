using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Harvester.Sprites;

namespace Harvester.Sprites
{
    internal class Combine : Sprite
    {
        private bool _combineGoingRight;
        Texture2D _rightTexture;
        Texture2D _leftTexture;
        public Combine(Texture2D rightTexture, int screenWidth, int screenHeight, Texture2D leftTexture) : base(rightTexture, screenWidth, screenHeight)
        {
            _type = SpriteType.Combine;
            _rightTexture = rightTexture;
            _leftTexture = leftTexture;
            Random rand = new Random();
            int isRight = rand.Next(0, 2);
            if (isRight == 1)
            {
                this._combineGoingRight = true;
                _texture = _rightTexture;
            }
            else
            {
                this._combineGoingRight = false;
                _texture = _leftTexture;
            }
            _collisionDamage = 400;
            Reset();
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites, float speedMultiplier)
        {
            // move harvesters
            _position.X += _combineGoingRight ? _speed * speedMultiplier : -_speed * speedMultiplier;
            _position.X += _combineGoingRight ? _speed * speedMultiplier : -_speed * speedMultiplier;

            // keep harvesters within screen
            if (_position.X < _texture.Width / 2)
            {
                _position.X = _texture.Width / 2;
                _combineGoingRight = true;
            }
            else if (_position.X > _screenWidth - _rightTexture.Width / 2)
            {
                _position.X = _screenWidth - _rightTexture.Width / 2;
                _combineGoingRight = false;
            }
        }

        public override void Reset()
        {
            Random rand = new Random();
            _speed = rand.Next(5, 15);
            _position.X = rand.Next(_texture.Width / 2, (_screenWidth - _texture.Width / 2) + 1);
            _position.Y = rand.Next(_texture.Height / 2, (_screenHeight - _texture.Height / 2) + 1);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, null, Color.White, 0f, new Vector2(_texture.Width / 2, _texture.Height / 2), Vector2.One, SpriteEffects.None, 0f);
        }
    }
}

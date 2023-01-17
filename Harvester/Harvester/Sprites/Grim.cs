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
    internal class Grim : Sprite
    {
        private float _grimDampening;
        private int _grimStopBuffer;
        public Grim(Texture2D texture, int screenWidth, int screenHeight) : base(texture, screenWidth, screenHeight)
        {
            _type = SpriteType.Grim;
            _collisionDamage = 400;
            _speed = 100;
            _grimDampening = (float)0.97;
            _grimStopBuffer = 7;
            Reset();
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites, float speedMultiplier)
        {
            Random rand = new Random();
            _direction.X += rand.Next(-5, 6);
            _direction.Y += rand.Next(-5, 6);
            _direction.X = (int)(_direction.X * _grimDampening);
            _direction.Y = (int)(_direction.Y * _grimDampening);
            int direction_x = Math.Abs(_direction.X) <= _grimStopBuffer ? 0 : (int)_direction.X;
            _position.X += (direction_x * speedMultiplier);
            int direction_y = Math.Abs(_direction.Y) <= _grimStopBuffer ? 0 : (int)_direction.Y;
            _position.Y += (direction_y * speedMultiplier);

            // keep grim within the screen and bounce off walls
            if (_position.Y < _texture.Height / 2)
            {
                _position.Y = _texture.Height / 2;
                _direction.Y = Math.Abs(_direction.Y);
            }
            else if (_position.Y > _screenHeight - _texture.Height / 2)
            {
                _position.Y = _screenHeight - _texture.Height / 2;
                _direction.Y = -Math.Abs(_direction.Y);
            }
            if (_position.X < _texture.Width / 2)
            {
                _position.X = _texture.Width / 2;
                _direction.X = Math.Abs(_direction.X);
            }
            else if (_position.X > _screenWidth - _texture.Width / 2)
            {
                _position.X = _screenWidth - _texture.Width / 2;
                _direction.X = -Math.Abs(_direction.X);
            }
        }

        public override void Reset()
        {
            Random rand = new Random();
            _direction = Vector2.Zero;
            _position.X = rand.Next(_texture.Width / 2, (_screenWidth - _texture.Width / 2) + 1);
            _position.Y = rand.Next(_texture.Height / 2, (_screenHeight - _texture.Height / 2) + 1);
        }

    }
}

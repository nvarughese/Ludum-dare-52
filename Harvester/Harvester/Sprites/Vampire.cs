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
    internal class Vampire : Sprite
    {

        
        public Vampire(Texture2D texture, int screenWidth, int screenHeight) : base(texture, screenWidth, screenHeight)
        {
            _collisionDamage = 200;
            _speed = 100;
            Reset();
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites, float speedMultiplier)
        {
            // move the vampire
            // surely a better way to do this
            Vector2 farmerPosition = Vector2.Zero;
            foreach (Sprite sprite in sprites) { 
                if(sprite is Farmer)
                {
                    farmerPosition.X = sprite._position.X;
                    farmerPosition.Y = sprite._position.Y;
                }
                else
                {
                    //return;
                    //throw new Exception("Vampire has not access to farmer sprite");
                }
            }
            _direction.X = _position.X < farmerPosition.X ? 1 : -1;
            _direction.Y = _position.Y < farmerPosition.Y ? 1 : -1;
            _position.X += _direction.X * speedMultiplier * (float)gameTime.ElapsedGameTime.TotalSeconds * _speed;
            _position.Y += _direction.Y * speedMultiplier * (float)gameTime.ElapsedGameTime.TotalSeconds * _speed;
        }

        

        public override void Reset()
        {
            
            _direction = Vector2.Zero;
            _position = new Vector2(_screenWidth * 3 / 5, _screenHeight * 1 / 4);
        }


    }
}

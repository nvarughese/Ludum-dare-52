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
    internal class Tomato : Sprite
    {
        public Tomato(Texture2D texture, int screenWidth, int screenHeight) : base(texture, screenWidth, screenHeight)
        {
            _type = SpriteType.Tomato;
            _collisionDamage = -100;
            Reset();
        }

        public override void Reset()
        {
            Random rand = new Random();
            _position.X = rand.Next(_texture.Width / 2, (_screenWidth - _texture.Width / 2) + 1);
            _position.Y = rand.Next(_texture.Height / 2, (_screenHeight - _texture.Height / 2) + 1);
        }


    }
}

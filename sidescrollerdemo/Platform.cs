using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace sidescrollerdemo
{
    class Platform : Sprite
    {
        public Platform(ContentManager content, string spriteName, Vector2 pos)
        {
              Load(content, spriteName);
              originalPosition = pos;
        }
    }
}

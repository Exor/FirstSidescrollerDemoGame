using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace sidescrollerdemo
{
    class Level1 : Level
    {
        public Level1(ContentManager content, float viewportWidth, float viewportHeight)
        {
            levelContent = content;
            viewport = new Vector2(viewportWidth, viewportHeight);
            CreatePlayer(0, 500);
            CreatePlatform(0, 550);
            CreatePlatform(200, 550);
            CreatePlatform(400, 550);
          //CreatePlatform(600, 550);
            CreatePlatform(500, 450);
            CreatePlatform(400, 350);
            CreateEnemy(400, 475);
        }
    }
}

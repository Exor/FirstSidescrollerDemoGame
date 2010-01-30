using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace sidescrollerdemo
{
    class Enemy : MovingSprite
    {
        //Rectangle? frame = new Rectangle(0, 0, 25, 25);

        private float movementSpeed = 50;
        //private bool isFalling;

        public Enemy(ContentManager content, string spriteName, Vector2 startPOS)
        {
            Load(content, spriteName);
            originalPosition = startPOS;
            currentPOS = startPOS;
        }

        public void Update(GameTime gameTime)
        {
            Walk((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        private void Walk(float gameTime)
        {
            if (movementSpeed > 0)
            {
                horizontalVelocity += movementSpeed * gameTime;
            }
            if (movementSpeed < 0)
            {
                horizontalVelocity -= movementSpeed * gameTime;
            }
        }

        internal override void Death()
        {
            isDead = true;
        }
    }
}

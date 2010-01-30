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

        private float movementSpeed = -6000f;
        //private bool isFalling;

        public Enemy(ContentManager content, string spriteName, Vector2 startPOS)
        {
            Load(content, spriteName);
            originalPosition = startPOS;
            currentPOS = startPOS;
            verticalGravity = 500f;
            horizontalVelocity = movementSpeed;

        }

        public void Update(GameTime gameTime)
        {
            Bounce();
            CalculateGravityPhysics((float)gameTime.ElapsedGameTime.TotalSeconds);
            Walk((float)gameTime.ElapsedGameTime.TotalSeconds);
            
        }

        private void Walk(float gameTime)
        {
            horizontalVelocity = movementSpeed * gameTime;
            currentPOS.X += horizontalVelocity * gameTime;
        }

        private void Bounce()
        {
            if (horizontalVelocity == 0)
            {
                movementSpeed = -movementSpeed;
            }
        }

        internal override void Death()
        {
            isDead = true;
        }
    }
}

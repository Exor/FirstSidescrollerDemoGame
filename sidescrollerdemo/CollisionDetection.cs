using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace sidescrollerdemo
{
    static class CollisionDetection
    {
        public static void ResolvePlatformCollisions(MovingSprite sprite, SortedList<int, Platform> platforms)
        {
            foreach (KeyValuePair<int, Platform> platform in platforms)
            {
                RectangleHelper playerRec = new RectangleHelper(sprite.currentPOS.X, sprite.currentPOS.Y, sprite.getWidth(), sprite.getHeight());
                RectangleHelper platformRec = new RectangleHelper(platform.Value.originalPosition.X, platform.Value.originalPosition.Y, platform.Value.getWidth(), platform.Value.getHeight());

                Vector2 overlap = RectangleHelper.Collide(playerRec, platformRec);

                PushSprite(sprite, overlap);
            }
        }

        private static void PushSprite(MovingSprite sprite, Vector2 overlap)
        {
            if (overlap.Y < overlap.X)
            {
                //push player down
                if (sprite.verticalVelocity < 0)
                {
                    sprite.currentPOS.Y += overlap.Y;
                    sprite.verticalVelocity = 0;
                    
                }

                //push player up
                else if (sprite.verticalVelocity > 0)
                {
                    sprite.currentPOS.Y -= overlap.Y;
                    sprite.verticalVelocity = 0;
                    sprite.isOnGround = true;
                }
            }

            if (overlap.X < overlap.Y)
            {
                //push player right
                if (sprite.horizontalVelocity < 0)
                {
                    sprite.currentPOS.X += overlap.X;
                    //sprite.horizontalVelocity = 0;
                    
                }

                //push player left
                if (sprite.horizontalVelocity > 0)
                {
                    sprite.currentPOS.X -= overlap.X;
                    //sprite.horizontalVelocity = 0;
                    
                }
            }
        }

        public static void CalculateScreenBoundries(MovingSprite sprite, Vector2 viewport)
        {
            //left
            if (sprite.currentPOS.X < 0)
            {
                sprite.currentPOS.X = 0;
                sprite.horizontalVelocity = 0;
            }
            //top
            if (sprite.currentPOS.Y < 0)
            {
                sprite.currentPOS.Y = 0;
                sprite.verticalVelocity = 0;
            }
            //right
            if (sprite.currentPOS.X > viewport.X - sprite.getWidth())
            {
                sprite.currentPOS.X = viewport.X - sprite.getWidth();
                sprite.horizontalVelocity = 0;
            }
            //bottom
            if (sprite.currentPOS.Y > viewport.Y)
            {
                sprite.Death();
            }
        }
    }
}

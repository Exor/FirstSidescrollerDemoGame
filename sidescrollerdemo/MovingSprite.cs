using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace sidescrollerdemo
{
    abstract class MovingSprite : Sprite
    {
        public Vector2 currentPOS;

        protected float verticalLaunchVelocity;
        protected float verticalGravity;
        public float verticalVelocity;

        public bool isOnGround { get; set; }
        public bool isDead { get; set; }

        protected bool isMovingLeft = false;
        protected bool isMovingRight = false;

        protected float horizontalAcceleration;
        protected float horizontalFriction;
        public float horizontalVelocity;
        protected float horizontalMaxVelocity;

        internal abstract void Death();

        internal void CalculateGravityPhysics(float elapsed)
        {
            //if (!isOnGround)
            //{//calculate effect of gravity
                verticalVelocity += verticalGravity * elapsed;
                currentPOS.Y += verticalVelocity * elapsed;
            //}
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace sidescrollerdemo
{
    class Player : MovingSprite
    {
        KeyboardState oldState;
        KeyboardState newState;

        private int lives;

        public Rectangle? frame = new Rectangle(0, 0, 28, 50);
        private float animateFPS = 1;
        private float timeSinceLastAnimation = 0;
        private int currentFrame = 0;

        public bool buttonLeft { get; set; }
        public bool buttonRight { get; set; }
        public bool buttonJump { get; set; }

        public Player(ContentManager content, string spriteName, float startingX, float startingY)
        {
            horizontalAcceleration = 510f;
            horizontalFriction = 300f;
            horizontalVelocity = 0f;
            horizontalMaxVelocity = 1000f;

            verticalLaunchVelocity = -500f;
            verticalGravity = 1000f;
            verticalVelocity = 0f;

            Load(content, spriteName);
            isDead = false;
            isOnGround = false;
            buttonLeft = false;
            buttonRight = false;
            buttonJump = false;
            frames = 2;
            lives = 1;
            originalPosition.X = startingX;
            originalPosition.Y = startingY;

            currentPOS = originalPosition;
        }

        private void CalculateRunningPhysics(float elapsed)
        {
            if (buttonRight)
            {
                horizontalVelocity = MathHelper.Clamp(horizontalVelocity + horizontalAcceleration * elapsed, -horizontalMaxVelocity, horizontalMaxVelocity);
            }
            if (buttonLeft)
            {
                horizontalVelocity = MathHelper.Clamp(horizontalVelocity - horizontalAcceleration * elapsed, -horizontalMaxVelocity, horizontalMaxVelocity);
            }

            if (isMovingRight)
            {
                horizontalVelocity = MathHelper.Clamp(horizontalVelocity - horizontalFriction * elapsed, 0, horizontalMaxVelocity);
            }
            else if (isMovingLeft)
            {
                horizontalVelocity = MathHelper.Clamp(horizontalVelocity + horizontalFriction * elapsed, -horizontalMaxVelocity, 0);
            }

            currentPOS.X += horizontalVelocity * elapsed;
        }

        private void SetMovementIndicators()
        {
            if (horizontalVelocity > 0)
            {
                isMovingRight = true;
                isMovingLeft = false;
            }
            if (horizontalVelocity < 0)
            {
                isMovingLeft = true;
                isMovingRight = false;
            }
            if (horizontalVelocity == 0)
            {
                isMovingLeft = false;
                isMovingRight = false;
            }
            if (buttonJump)
            {
                verticalVelocity = verticalLaunchVelocity;
                isOnGround = false;
            }
        }

        public void Update(GameTime gameTime)
        {
            GetNewKeyboardState();
            ResolveMovement();
            SetMovementIndicators();
            CalculateGravityPhysics((float)gameTime.ElapsedGameTime.TotalSeconds);
            CalculateRunningPhysics((float)gameTime.ElapsedGameTime.TotalSeconds);
            Animate((float)gameTime.ElapsedGameTime.TotalSeconds, 2);
        }

        private void Animate(float elapsed, int numberOfFrames)
        {
            timeSinceLastAnimation += elapsed;
            if (timeSinceLastAnimation > animateFPS)
            {
                currentFrame++;
                currentFrame = currentFrame % numberOfFrames;
                frame = new Rectangle(0, currentFrame * 50, 28, 50);
                timeSinceLastAnimation -= animateFPS;
            }
        }

        private void ResolveMovement()
        {
            buttonJump = false;

            if (newState.IsKeyDown(Keys.Right))
            {
                buttonRight = true;
            }
            else if (oldState.IsKeyDown(Keys.Right))
            {
                buttonRight = false;
            }
            if (newState.IsKeyDown(Keys.Left))
            {
                buttonLeft = true;
            }
            else if (oldState.IsKeyDown(Keys.Left))
            {
                buttonLeft = false;
            }
            if (newState.IsKeyDown(Keys.Up) && !oldState.IsKeyDown(Keys.Up) && isOnGround)
            {
                buttonJump = true;
            }
            oldState = newState;
        }

        private void GetNewKeyboardState()
        {
            newState = Keyboard.GetState();
        }

        internal override void Death()
        {
            lives--;
            if (lives > 0)
            {
                Respawn();
            }
            //Game Over here
            isDead = true;
        }

        private void Respawn()
        {
            currentPOS = originalPosition;
            isOnGround = false;
            buttonLeft = false;
            buttonRight = false;
            buttonJump = false;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace sidescrollerdemo
{
    abstract class Sprite
    {
        Texture2D sprite;
        public Vector2 originalPosition;
        protected int frames;

        protected void Load(ContentManager content, string spriteName)
        {
            frames = 1;
            sprite = content.Load<Texture2D>(spriteName);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 pos)
        {
            spriteBatch.Draw(sprite, pos, Color.White);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 pos, Rectangle? spriteFrame)
        {
            spriteBatch.Draw(sprite, pos, spriteFrame, Color.White);
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(sprite, originalPosition, Color.White);
        }

        public int getWidth()
        {
            return sprite.Width;
        }

        public int getHeight()
        {
            return sprite.Height / frames;
        }
    }
}

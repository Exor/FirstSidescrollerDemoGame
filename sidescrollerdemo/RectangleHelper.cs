using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace sidescrollerdemo
{
    class RectangleHelper
    {
        private float X;
        private float Y;
        private float Width;
        private float Height;

        public RectangleHelper(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public static Vector2 Collide(RectangleHelper r1, RectangleHelper r2)
        {
            //find the center of each rectangle
            Vector2 r1Center = new Vector2(r1.X + r1.Width / 2, r1.Y + r1.Height / 2);
            Vector2 r2Center = new Vector2(r2.X + r2.Width / 2, r2.Y + r2.Height / 2);

            //distance from one rectangle to the other
            float xDistance = Math.Abs(r1Center.X - r2Center.X);
            float yDistance = Math.Abs(r1Center.Y - r2Center.Y);

            //Minimum distance from one rectangle to the other without colliding
            float xMinDistance = r1.Width / 2 + r2.Width / 2;
            float yMinDistance = r1.Height / 2 + r2.Height / 2;

            //if they do not collide return vector2.zero
            if (xDistance >= xMinDistance || yDistance >= yMinDistance)
                return Vector2.Zero;

            //else return the amount of overlap
            return new Vector2(xMinDistance - xDistance, yMinDistance - yDistance);
        }

    }
}

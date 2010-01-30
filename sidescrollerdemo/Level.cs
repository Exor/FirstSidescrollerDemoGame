using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;

namespace sidescrollerdemo
{
    class Level
    {
        private Player player;
        private SortedList<int, Platform> platforms = new SortedList<int,Platform>();
        private SortedList<int, Enemy> enemies = new SortedList<int, Enemy>();
        protected ContentManager levelContent;
        protected Vector2 viewport;

        public void CreatePlatform(float x, float y)
        {
            Platform platform = new Platform(levelContent, "platform", new Vector2(x, y));
            platforms.Add(platforms.Count, platform);
        }

        public void CreateEnemy(float x, float y)
        {
            Enemy enemy = new Enemy(levelContent, "enemy", new Vector2(x, y));
            enemies.Add(enemies.Count, enemy);
        }

        public void CreatePlayer(float x, float y)
        {
            player = new Player(levelContent, "character", x, y);
        }

        public void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            foreach (KeyValuePair<int, Enemy> enemy in enemies)
            {
                enemy.Value.Update(gameTime);
                CollisionDetection.ResolvePlatformCollisions(enemy.Value, platforms);
                CollisionDetection.CalculateScreenBoundries(enemy.Value, viewport);
            }
            CollisionDetection.ResolvePlatformCollisions(player, platforms);
            CollisionDetection.CalculateScreenBoundries(player, viewport);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            player.Draw(spriteBatch, player.currentPOS, player.frame);
            foreach (KeyValuePair<int, Platform> platform in platforms)
            {
                platform.Value.Draw(spriteBatch);
            }
            foreach (KeyValuePair<int, Enemy> enemy in enemies)
            {
                enemy.Value.Draw(spriteBatch, enemy.Value.currentPOS);
            }
        }

        public bool EndLevel()
        {
            if (player.isDead)
            {
                return true;
            }
            return false;
        }
    }
}

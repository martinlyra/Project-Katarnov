using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov
{
    class SpriteBatchRenderer
    {
        SpriteBatch spriteBatch;

        readonly Queue<Entity> drawQueue = new Queue<Entity>();

        readonly Game1 _game;

        public SpriteBatchRenderer(Game1 game)
        {
            _game = game;

            spriteBatch = new SpriteBatch(_game.GraphicsDevice);
        }

        public void QueryForDrawing()
        {
            drawQueue.Clear();

            foreach (var o in _game.entityManager.entities.Values)
            {
                if (o.ShouldDraw())
                    drawQueue.Enqueue(o);
            }
        }

        public void ProcessDrawing()
        {
            spriteBatch.Begin();
            while (drawQueue.Count > 0)
            {
                var e = drawQueue.Dequeue();
                e.Draw();
                spriteBatch.Draw(e.sprite.texture, new Vector2(e.position.X * 32, e.position.Y * 32), Color.White);
            }
            spriteBatch.End();
        }
    }
}

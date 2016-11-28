using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov
{
    struct DrawCall
    {
        public Vector3 position;
        public Sprite sprite;

        public DrawCall(Vector3 position, Sprite sprite)
        {
            this.position = position;
            this.sprite = sprite;
        }
    }

    class SpriteBatchRenderer
    {
        SpriteBatch spriteBatch;

        readonly Queue<DrawCall> drawQueue = new Queue<DrawCall>();

        readonly Dictionary<string, Sprite> loadedSprites = new Dictionary<string, Sprite>();

        readonly Game1 _game;

        Vector2 center;
        Vector2 scale = new Vector2(1.0f);

        public SpriteBatchRenderer(Game1 game)
        {
            _game = game;

            spriteBatch = new SpriteBatch(_game.GraphicsDevice);
            CalculateCenter();
        }

        public void QueryForDrawing()
        {
            drawQueue.Clear();

            foreach (var o in EntityManager.entities.Values)
            {
                if (o.ShouldDraw())
                    drawQueue.Enqueue(new DrawCall(
                            new Vector3(o.position.Xf, o.position.Yf, o.position.Zf),
                            Assets.GetSprite(o.spritePath)
                        ));
            }
        }

        protected Vector2 DrawPosition(DrawCall call)
        {
            return Vector2.Multiply(new Vector2(
                (call.position.X - _game.gameView.position.X)* 32, 
                (call.position.Y - _game.gameView.position.Y)* 32
                ),
                scale
                );
        }

        protected void CalculateCenter()
        {
            var center = _game.GraphicsDevice.Viewport.Bounds.Center.ToVector2();
            this.center = Vector2.Divide(new Vector2(
                    -center.X + 16,
                    -center.Y + 16
                ),scale);
        }

        public void ProcessDrawing()
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            while (drawQueue.Count > 0)
            {
                var dCall = drawQueue.Dequeue();
                spriteBatch.Draw(
                    texture: dCall.sprite.texture,
                    position: DrawPosition(dCall),
                    origin: center,
                    scale: scale,
                    color:      Color.White
                    );
            }
            spriteBatch.End();
        }
    }
}

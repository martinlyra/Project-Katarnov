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

        readonly List<string> spritesToLoad = new List<string>()
        {
            "Content/Object/lattice.png",
            "Content/Turf/plating.png",
            "Content/Turf/steel_dirty.png",
            "Content/Turf/wall.png",
            "Content/Turf/wall_reinforced.png"
        };
        readonly Dictionary<string, Sprite> loadedSprites = new Dictionary<string, Sprite>();

        readonly Game1 _game;

        public SpriteBatchRenderer(Game1 game)
        {
            _game = game;

            spriteBatch = new SpriteBatch(_game.GraphicsDevice);

            InstantizeSprites();
        }

        void InstantizeSprites()
        {
            foreach (var file in spritesToLoad)
                loadedSprites.Add(file, Sprite.LoadFile(
                        spriteBatch.GraphicsDevice,
                        file
                    ));
        }

        public void QueryForDrawing()
        {
            drawQueue.Clear();

            foreach (var o in _game.entityManager.entities.Values)
            {
                if (o.ShouldDraw())
                    drawQueue.Enqueue(new DrawCall(
                            new Vector3(o.position.X,o.position.Y, o.position.Z),
                            loadedSprites[o.spritePath]
                        ));
            }
        }

        protected Vector2 DrawPosition(DrawCall call)
        {
            return new Vector2(
                (call.position.X - _game.gameView.position.X)* 32, 
                (call.position.Y - _game.gameView.position.Y)* 32
                );
        }

        public void ProcessDrawing()
        {
            spriteBatch.Begin();
            while (drawQueue.Count > 0)
            {
                var dCall = drawQueue.Dequeue();
                spriteBatch.Draw(
                    texture:    dCall.sprite.texture,
                    position:   DrawPosition(dCall),
                    color:      Color.White
                    );
            }
            spriteBatch.End();
        }
    }
}

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectKatarnov
{
    class ActorObject
    {
        public Vector3 position;
        public Vector2 bounds;

        public Sprite sprite;

        public bool density;
        public bool opaque;

        public bool active;
        public bool enabled;

        public ActorObject()
        {

        }

        public ActorObject(Sprite sprite)
        {
            this.sprite = sprite;
        }

        public ActorObject(Sprite sprite, Vector3 position)
        {
            this.sprite = sprite;
            this.position = position;
        }

        public void Initialize()
        {

        }

        public void Update()
        {

        }

        public void Draw()
        {

        }
    }
}

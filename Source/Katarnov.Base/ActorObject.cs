using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov
{
    public class ActorObject : Entity
    {

        public ActorObject() : base()
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

        public override void Initialize()
        {

        }

        public override void Update()
        {

        }

        public override void Draw()
        {

        }
    }
}

using Katarnov.Module;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov
{
    public abstract class Entity : IEntity
    {
        public readonly EntityIdentifier identifier = new EntityIdentifier();
        public EntityDefine defineInfo;
        public EntityDefine overrideDefineInfo;

        public Vector3 position;
        public Vector2 bounds;

        public Sprite sprite;

        public string spritePath;

        public bool density;
        public bool opaque;

        public bool active;
        public bool enabled;

        public Entity()
        {
            Global.gameInstance.entityManager.Add(this);
        }

        public virtual void Initialize()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void Draw()
        {

        }

        public virtual bool NeedsUpdate()
        {
            return active;
        }

        public virtual bool ShouldDraw()
        {
            return enabled || spritePath != null;
        }
    }
}

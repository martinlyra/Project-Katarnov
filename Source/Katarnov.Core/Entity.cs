using Katarnov.Module;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov
{

    public enum Direction
    {
        NORTH,
        NORTHEAST,
        EAST,
        SOUTHEAST,
        SOUTH,
        SOUTHWEST,
        WEST,
        NORTHWEST
    }

    public abstract class Entity : IEntity
    {
        public readonly EntityIdentifier identifier = new EntityIdentifier();

        // TODO: Phase out these two below
        public EntityDefine defineInfo;
        public EntityDefine overrideDefineInfo;

        public Vector3 position;
        public Vector2 bounds;

        // TODO: This thing isn't in use, make it useful, someday
        public Sprite sprite;

        public Direction direction = Direction.SOUTH;
        public string spritePath;

        public bool density;
        public bool opaque;

        public bool active;
        public bool enabled;

        public Entity()
        {
            EntityManager.Add(this);
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


        // For quaternion rotations (true rotation)
        public virtual void LookAt()
        {

        }

        // For visual (false visual rotation)
        public virtual void FaceAt(Vector3 other)
        {

        }

        public virtual void MoveTo(Vector3 destination)
        {

        }
    }
}

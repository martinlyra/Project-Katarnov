using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov.Module.Core.Turf
{
    abstract class Flooring
    {
        private string name;
        private string description;
        private string descriptor;

        private string spritePath;
        private string spriteState;

        private TurfFlags flags;
        private bool paintable;

        public string Name
        {
            get
            {
                return name;
            }

            protected set
            {
                name = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }

            protected set
            {
                description = value;
            }
        }

        public string Descriptor
        {
            get
            {
                return descriptor;
            }

            set
            {
                descriptor = value;
            }
        }

        public string SpritePath
        {
            get
            {
                return spritePath;
            }

            protected set
            {
                spritePath = value;
            }
        }

        public string SpriteState
        {
            get
            {
                return spriteState;
            }

            protected set
            {
                spriteState = value;
            }
        }

        public TurfFlags Flags
        {
            get
            {
                return flags;
            }

            protected set
            {
                flags = value;
            }
        }

        public bool Paintable
        {
            get
            {
                return paintable;
            }

            protected set
            {
                paintable = value;
            }
        }
    }

    class TiledFlooring : Flooring
    {
        public TiledFlooring()
        {
            Name = "floor";
            Description = "Scuffed from the passage of countless greyshirts.";
            Descriptor = "tiles";

            SpritePath = "Content/Turf";
            SpriteState = "floor_steel.png";

            Paintable = true;
            Flags = TurfFlags.RemoveableCrowbar | TurfFlags.CanBreak | TurfFlags.CanBurn;
        }
    }

    class DarkTiledFlooring : TiledFlooring
    {
        public DarkTiledFlooring()
        {
            Description = "How omnious.";
            SpriteState = "floor_dark.png";
        }
    }

    class WhiteTiledFlooring : TiledFlooring
    {
        public WhiteTiledFlooring()
        {
            Description = "How sterile.";
            SpriteState = "floor_white.png";
        }
    }

    class WoodenFlooring : Flooring
    {
        public WoodenFlooring()
        {
            Name = "wooden floor";
            Description = "Polished redwood planks.";
            Descriptor = "planks";

            SpritePath = "Content/Turf";
            SpriteState = "floor_wood";

            Flags = TurfFlags.CanBreak | TurfFlags.CanBurn | TurfFlags.IsFragile | TurfFlags.RemoveableScrewdriver;
        }
    }

    class ReinforcedFlooring : Flooring
    {
        public ReinforcedFlooring()
        {
            Name = "reinforced floor";
            Name = "Heavily reinforced with steel rods.";

            SpritePath = "Content/Turf";
            SpriteState = "floor_reinforced";

            Flags = TurfFlags.AcidImmune | TurfFlags.RemoveableWrench;
            Paintable = true;
        }
    }
}

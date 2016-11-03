using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Katarnov
{
    public static class Assets
    {
        private const string contentDirectory = "Content";
        private const string modulesDirectory = "Modules";

        private static readonly Dictionary<string, Sprite> loadedSprites = new Dictionary<string, Sprite>();

        internal static void Initialize()
        {
            loadedSprites.Add("err_missing",
                new Sprite(Texture2DExt.AsColorRectangle(new Rectangle(0,0,32,32), Color.Purple)));
        }

        internal static void ImportSprites()
        {
            ModuleManager.ImportedTypes.ToList().ForEach(
                type =>
                {
                    var t = type.Value;
                    if (t.Inherits<Entity>() && !t.IsAbstract)
                    {
                        var e = t.InstantizeAs<Entity>();
                        if (!loadedSprites.ContainsKey(e.spritePath))
                            loadedSprites.Add(e.spritePath, Sprite.LoadFile
                                (Global.gameInstance.GraphicsDevice, e.spritePath)
                                );
                    }
                }
                );
            Global.gameInstance.entityManager.Reset();
            Game1.HasLoaded = true;
        }

        internal static Sprite GetSprite(string name)
        {
            try     { return loadedSprites[name]; }
            catch   { return loadedSprites["err_missing"]; }
        }

        internal static Dictionary<string, Sprite> ImportedSprites
        {
            get
            {
                return loadedSprites;
            }
        }
    }
}

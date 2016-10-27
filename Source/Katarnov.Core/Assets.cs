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
            return loadedSprites[name];
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

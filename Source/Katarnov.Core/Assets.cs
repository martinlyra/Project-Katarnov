using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
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

            foreach (var f in Directory.EnumerateFiles(@"Content\Turf"))
            {
                var file = f;
                loadedSprites.Add(file.Replace('\\','/'), Sprite.LoadFile(Global.gameInstance.GraphicsDevice, file));
            }

            foreach (var f in loadedSprites)
                Console.WriteLine(f.Key);
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
                        if (e.spritePath == null)
                            return;
                        if (!loadedSprites.ContainsKey(e.spritePath))
                            loadedSprites.Add(e.spritePath, Sprite.LoadFile
                                (Global.gameInstance.GraphicsDevice, e.spritePath)
                                );
                    }
                }
                );
            EntityManager.Reset();
            Game1.HasLoaded = true;
        }

        internal static Sprite GetSprite(string name)
        {
            try     { return loadedSprites[name]; }
            catch (Exception e)  { PrintTrace(name); return loadedSprites["err_missing"]; }
        }

        internal static void PrintTrace(string name)
        {
            foreach (var i in loadedSprites)
                Console.WriteLine(i.Key);
            Console.WriteLine($">> {name}");
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

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov
{
    public class Sprite
    {
        public List<Sprite> spriteList = new List<Sprite>();

        public Texture2D texture;

        public Sprite(string filepath)
        {

        }

        public Sprite(Texture2D texture)
        {
            this.texture = texture;
        }

        public static Sprite LoadFile(GraphicsDevice device, string file)
        {
            try
            {
                FileStream fileStream = new FileStream(file, FileMode.Open);
                Texture2D spriteAtlas = Texture2D.FromStream(device, fileStream);
                fileStream.Dispose();

                Debug.Assert(spriteAtlas != null);

                return new Sprite(spriteAtlas);
            }
            catch
            {
                return new Sprite(Texture2DExt.AsColorRectangle(
                    new Rectangle(0,0,32,32), Color.Blue));
            }
        }

        /*
        public static Sprite LoadFromFile(string path)
        {
            FileStream fileStream = new FileStream("Content/sprites/sprite_atlas.png", FileMode.Open);
            Texture2D spriteAtlas = Texture2D.FromStream(graphicsDevice, fileStream);
            fileStream.Dispose();
        }
        */
    }
}

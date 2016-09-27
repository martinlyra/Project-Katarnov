using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectKatarnov
{
    class Sprite
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

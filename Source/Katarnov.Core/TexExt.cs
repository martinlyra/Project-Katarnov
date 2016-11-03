using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov
{
    public static class Texture2DExt
    {
        public static Texture2D AsColorRectangle(Rectangle rect, Color color)
        {
            Color[] rtex = Enumerable.Range(0, rect.Width * rect.Height)
                .Select(a => color).ToArray();
            var texture = 
                new Texture2D(Global.gameInstance.GraphicsDevice, rect.Width, rect.Height);
            texture.SetData(rtex);
            return texture;
        }
    }
}

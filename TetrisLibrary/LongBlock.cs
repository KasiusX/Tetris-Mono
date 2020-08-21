using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisLibrary
{
    public class LongBlock : BlockModel
    {
        public LongBlock(int windowWidth, int windowHeight, int startingY, Texture2D sprite)
        {
            Width = 40;
            Height = 200;
            X = windowWidth / 2 - Width / 2;
            Y = startingY;
            Sprite = sprite;
        }
    }
}

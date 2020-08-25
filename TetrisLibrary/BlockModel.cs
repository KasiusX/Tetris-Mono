using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisLibrary
{
    public class BlockModel
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height{ get; set; }
        public int Speed { get; set; } = 40;
        public Texture2D Sprite { get; set; }
        public BlockModel(int windowWidth, int windowHeight, int startingY, Texture2D sprite, int width, int height)
        {
            Width = width;
            Height = height;
            X = 25+4*40;
            Y = startingY;
            Sprite = sprite;
        }
        public void MoveDown()
        {
            Y += Speed;
        }

        public void MoveRight()
        {
            X += 40;
        }

        public void MoveLeft()
        {
            X -= 40;
        }
    }
}

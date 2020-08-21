using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisLibrary
{
    public abstract class BlockModel
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height{ get; set; }
        public int Speed { get; set; } = 1;
        public Texture2D Sprite { get; set; }        
            public void MoveDown()
        {
            Y += Speed;
        }
    }
}

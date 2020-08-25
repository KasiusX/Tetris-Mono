using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisLibrary
{
    public class BlockManager
    {
        private readonly int windowWidht;
        private readonly int windowHeight;
        private readonly int startingY;
        Random random = new Random();
        HitboxManager manager = new HitboxManager();

        public BlockManager(int windowWidht,int windowHeight, int startingY)
        {
            this.windowWidht = windowWidht;
            this.windowHeight = windowHeight;
            this.startingY = startingY;
        }
        public BlockModel GenerateRandomBlock()
        {
           char choice = char.Parse(random.Next(0,0).ToString());
            switch (choice)
            {
                case '0':
                    return new BlockModel(startingY,BlockType.leftLShape, Color.Orange,80,120);
                case '1':
                    return new BlockModel(startingY, BlockType.rightLShape, Color.DarkGreen,80,120);
                case '2':
                    return new BlockModel(startingY, BlockType.leftZShape, Color.LimeGreen,120,80);
                case '3':
                    return new BlockModel(startingY, BlockType.rightZShape, Color.DarkRed, 120, 80);
                case '4':
                    return new BlockModel(startingY, BlockType.longBlock, Color.AliceBlue,40,160);
                case '5':
                    return new BlockModel(startingY, BlockType.squareBlock, Color.Yellow, 80, 80);
                case '6':
                    return new BlockModel(startingY, BlockType.TShapeBlock, Color.Purple, 120, 80);                
                default:
                    return null;
                    
            }
        }
    }
}

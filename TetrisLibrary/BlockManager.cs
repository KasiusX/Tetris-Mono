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
        public HitboxManager manager = new HitboxManager();

        public BlockManager(int windowWidht,int windowHeight, int startingY)
        {
            this.windowWidht = windowWidht;
            this.windowHeight = windowHeight;
            this.startingY = startingY;
        }
        public BlockModel GenerateRandomBlock()
        {
           char choice = char.Parse(random.Next(7).ToString());
            switch (choice)
            {
                case '0':
                    return new BlockModel(BlockType.leftLShape, Color.BlueViolet,80,120);
                case '1':
                    return new BlockModel( BlockType.rightLShape, Color.HotPink,80,120);
                case '2':
                    return new BlockModel( BlockType.leftZShape, Color.LightPink,120,80);
                case '3':
                    return new BlockModel( BlockType.rightZShape, Color.LightBlue, 120, 80);
                case '4':
                    return new BlockModel( BlockType.longBlock, Color.LightSkyBlue,40,160);
                case '5':
                    return new BlockModel( BlockType.squareBlock, Color.AliceBlue, 80, 80);
                case '6':
                    return new BlockModel( BlockType.TShapeBlock, Color.MediumPurple, 120, 80);                
                default:
                    return null;
                    
            }
        }
    }
}

using Microsoft.Xna.Framework;
using System;

namespace TetrisLibrary
{
    public static class BlockGenerator
    {
        static Random random = new Random();
                
        public static BlockModel GenerateRandomBlock()
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

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
        private readonly Texture2D squareBlock;
        private readonly Texture2D longBlock;
        private readonly Texture2D tShapeBlock;
        private readonly Texture2D leftLShapeBlock;
        private readonly Texture2D rightLShapeBlock;
        private readonly Texture2D leftZShapeBlock;
        private readonly Texture2D rightZShapeBlock;
        Random random = new Random();

        public BlockManager(int windowWidht,int windowHeight, int startingY,
            Texture2D squareBlock, Texture2D longBlock, Texture2D tShapeBlock, 
            Texture2D leftLShapeBlock, Texture2D rightLShapeBlock, 
            Texture2D leftZShapeBlock, Texture2D rightZShapeBlock)
        {
            this.windowWidht = windowWidht;
            this.windowHeight = windowHeight;
            this.startingY = startingY;
            this.squareBlock = squareBlock;
            this.longBlock = longBlock;
            this.tShapeBlock = tShapeBlock;
            this.leftLShapeBlock = leftLShapeBlock;
            this.rightLShapeBlock = rightLShapeBlock;
            this.leftZShapeBlock = leftZShapeBlock;
            this.rightZShapeBlock = rightZShapeBlock;
        }
        public BlockModel GenerateRandomBlock()
        {
           char choice = char.Parse(random.Next(0,7).ToString());
            switch (choice)
            {
                case '0':
                    return new BlockModel(windowWidht, windowHeight, startingY, squareBlock,80,80);
                case '1':
                    return new BlockModel(windowWidht, windowWidht, startingY, longBlock,40,200);
                case '2':
                    return new BlockModel(windowWidht, windowWidht, startingY, tShapeBlock,120,80);
                case '3':
                    return new BlockModel(windowWidht, windowWidht, startingY, leftLShapeBlock,120,80);
                case '4':
                    return new BlockModel(windowWidht, windowWidht, startingY, rightLShapeBlock,120,80);
                case '5':
                    return new BlockModel(windowWidht, windowWidht, startingY, leftZShapeBlock,120,80);
                case '6':
                    return new BlockModel(windowWidht, windowWidht, startingY, rightZShapeBlock,120,80);                
                default:
                    return null;
                    
            }
        }
    }
}

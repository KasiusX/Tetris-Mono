using Microsoft.Xna.Framework;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisLibrary
{
    public class HitboxManager
    {        
        public List<Rectangle> GetHitBoxes(BlockModel block)
        {
            List<Rectangle> output = new List<Rectangle>();
            Rectangle rec = new Rectangle(0, 0, 40, 40);
            switch (block.Type)
            {
                case BlockType.leftLShape:
                    output = GetHitboxesLeftLShape(block, rec);
                    break;

                case BlockType.rightLShape:
                    output = GetHitboxesRightLShape(block, rec);
                    break;

                case BlockType.leftZShape:
                    output = GetHitboxesLeftZShape(block, rec);
                    break;

                case BlockType.rightZShape:
                    output = GetHitboxesRightZShape(block, rec);
                    break;

                case BlockType.longBlock:
                    output = GetHitboxesLongBlock(block,rec);                    
                    break;

                case BlockType.squareBlock:
                    output = GetHitboxesSquareBlock(block, rec);
                    break;

                case BlockType.TShapeBlock:
                    output = GetHitboxesTShapeBlock(block, rec);
                    break;

                default:
                    break;
            }
            return output;
        }

        private List<Rectangle> GetHitboxesLeftLShape(BlockModel block, Rectangle rec)
        {
            List<Rectangle> output = new List<Rectangle>();
            switch (block.RotatinForm)
            {
                case 1:
                    rec.X = 40;
                    rec.Y = 0;
                    output.Add(rec);

                    rec.X = 40;
                    rec.Y = 40;
                    output.Add(rec);

                    rec.X = 40;
                    rec.Y = 80;
                    output.Add(rec);

                    rec.X = 0;
                    rec.Y = 80;
                    output.Add(rec);
                    break;
                case 2:                    
                    rec.X = 0;
                    rec.Y = 0;
                    output.Add(rec);

                    rec.X = 0;
                    rec.Y = 40;
                    output.Add(rec);

                    rec.X = 40;
                    rec.Y = 40;
                    output.Add(rec);

                    rec.X = 80;
                    rec.Y = 40;
                    output.Add(rec);
                    break;
                case 3:
                    rec.X = 40;
                    rec.Y = 0;
                    output.Add(rec);

                    rec.X = 0;
                    rec.Y = 0;
                    output.Add(rec);

                    rec.X = 0;
                    rec.Y = 40;
                    output.Add(rec);

                    rec.X = 0;
                    rec.Y = 80;
                    output.Add(rec);
                    break;
                case 4:
                    rec.X = 0;
                    rec.Y = 0;
                    output.Add(rec);

                    rec.X = 40;
                    rec.Y = 0;
                    output.Add(rec);

                    rec.X = 80;
                    rec.Y = 0;
                    output.Add(rec);

                    rec.X = 80;
                    rec.Y = 40;
                    output.Add(rec);
                    break;
            }
            return output;
        }

        private List<Rectangle> GetHitboxesRightLShape(BlockModel block, Rectangle rec)
        {
            List<Rectangle> output = new List<Rectangle>();
            switch (block.RotatinForm)
            {
                case 1:
                    rec.X = 0;
                    rec.Y = 0;
                    output.Add(rec);

                    rec.X = 0;
                    rec.Y = 40;
                    output.Add(rec);

                    rec.X = 0;
                    rec.Y = 80;
                    output.Add(rec);

                    rec.X = 40;
                    rec.Y = 80;
                    output.Add(rec);
                    break;
                case 2:
                    rec.X = 0;
                    rec.Y = 0;
                    output.Add(rec);

                    rec.X = 0;
                    rec.Y = 40;
                    output.Add(rec);

                    rec.X = 40;
                    rec.Y = 0;
                    output.Add(rec);

                    rec.X = 80;
                    rec.Y = 0;
                    output.Add(rec);
                    break;
                case 3:
                    rec.X = 0;
                    rec.Y = 0;
                    output.Add(rec);

                    rec.X = 40;
                    rec.Y = 0;
                    output.Add(rec);

                    rec.X = 40;
                    rec.Y = 40;
                    output.Add(rec);

                    rec.X = 40;
                    rec.Y = 80;
                    output.Add(rec);
                    break;
                case 4:
                    rec.X = 0;
                    rec.Y = 40;
                    output.Add(rec);

                    rec.X = 40;
                    rec.Y = 40;
                    output.Add(rec);

                    rec.X = 80;
                    rec.Y = 40;
                    output.Add(rec);

                    rec.X = 80;
                    rec.Y = 0;
                    output.Add(rec);
                    break;
            }
            return output;
        }
        private List<Rectangle> GetHitboxesLeftZShape(BlockModel block, Rectangle rec)
        {
            List<Rectangle> output = new List<Rectangle>();
            switch (block.RotatinForm)
            {
                case 1: 
                case 3:
                    rec.X = 0;
                    rec.Y = 0;
                    output.Add(rec);

                    rec.X = 40;
                    rec.Y = 0;
                    output.Add(rec);

                    rec.X = 40;
                    rec.Y = 40;
                    output.Add(rec);

                    rec.X = 80;
                    rec.Y = 40;
                    output.Add(rec);
                    break;

                case 2:
                case 4:
                    rec.X = 40;
                    rec.Y = 0;
                    output.Add(rec);

                    rec.X = 40;
                    rec.Y = 40;
                    output.Add(rec);

                    rec.X = 0;
                    rec.Y = 40;
                    output.Add(rec);

                    rec.X = 0;
                    rec.Y = 80;
                    output.Add(rec);
                    break;                
            }
            return output;
        }
        private List<Rectangle> GetHitboxesRightZShape(BlockModel block, Rectangle rec)
        {
            List<Rectangle> output = new List<Rectangle>();
            switch (block.RotatinForm)
            {
                case 1: 
                case 3:
                    rec.X = 0;
                    rec.Y = 40;
                    output.Add(rec);

                    rec.X = 40;
                    rec.Y = 40;
                    output.Add(rec);

                    rec.X = 40;
                    rec.Y = 0;
                    output.Add(rec);

                    rec.X = 80;
                    rec.Y = 0;
                    output.Add(rec);
                    break;

                case 2: 
                case 4:
                    rec.X = 0;
                    rec.Y = 0;
                    output.Add(rec);

                    rec.X = 0;
                    rec.Y = 40;
                    output.Add(rec);

                    rec.X = 40;
                    rec.Y = 40;
                    output.Add(rec);

                    rec.X = 40;
                    rec.Y = 80;
                    output.Add(rec);
                    break;
            }
            return output;
        }
        private List<Rectangle> GetHitboxesLongBlock(BlockModel block, Rectangle rec)
        {
            List<Rectangle> output = new List<Rectangle>();
            switch (block.RotatinForm)
            {
                case 1:
                case 3:
                    rec.X = 0;
                    rec.Y = 0;
                    output.Add(rec);

                    rec.X = 0;
                    rec.Y = 40;
                    output.Add(rec);

                    rec.X = 0;
                    rec.Y = 80;
                    output.Add(rec);

                    rec.X = 0;
                    rec.Y = 120;
                    output.Add(rec);
                    break;

                case 2:
                case 4:
                    rec.X = 0;
                    rec.Y = 0;
                    output.Add(rec);

                    rec.X = 40;
                    rec.Y = 0;
                    output.Add(rec);

                    rec.X = 80;
                    rec.Y = 0;
                    output.Add(rec);

                    rec.X = 120;
                    rec.Y = 0;
                    output.Add(rec);
                    break;
            }
            return output;
        }
        private List<Rectangle> GetHitboxesSquareBlock(BlockModel block, Rectangle rec)
        {
            List<Rectangle> output = new List<Rectangle>();
            switch (block.RotatinForm)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    rec.X = 0;
                    rec.Y = 0;
                    output.Add(rec);

                    rec.X = 40;
                    rec.Y = 0;
                    output.Add(rec);

                    rec.X = 0;
                    rec.Y = 40;
                    output.Add(rec);

                    rec.X = 40;
                    rec.Y = 40;
                    output.Add(rec);
                    break;                                        
            }
            return output;
        }
        private List<Rectangle> GetHitboxesTShapeBlock(BlockModel block, Rectangle rec)
        {
            List<Rectangle> output = new List<Rectangle>();
            switch (block.RotatinForm)
            {
                case 1:
                    rec.X = 40;
                    rec.Y = 0;
                    output.Add(rec);

                    rec.X = 0;
                    rec.Y = 40;
                    output.Add(rec);

                    rec.X = 40;
                    rec.Y = 40;
                    output.Add(rec);

                    rec.X = 80;
                    rec.Y = 40;
                    output.Add(rec);
                    break;
                case 2:
                    rec.X = 0;
                    rec.Y = 0;
                    output.Add(rec);

                    rec.X = 0;
                    rec.Y = 40;
                    output.Add(rec);

                    rec.X = 0;
                    rec.Y = 80;
                    output.Add(rec);

                    rec.X = 40;
                    rec.Y = 40;
                    output.Add(rec);
                    break;
                case 3:
                    rec.X = 0;
                    rec.Y = 0;
                    output.Add(rec);

                    rec.X = 40;
                    rec.Y = 0;
                    output.Add(rec);

                    rec.X = 80;
                    rec.Y = 0;
                    output.Add(rec);

                    rec.X = 40;
                    rec.Y = 40;
                    output.Add(rec);
                    break;
                case 4:
                    rec.X = 0;
                    rec.Y = 40;
                    output.Add(rec);

                    rec.X = 40;
                    rec.Y = 0;
                    output.Add(rec);

                    rec.X = 40;
                    rec.Y = 40;
                    output.Add(rec);

                    rec.X = 40;
                    rec.Y = 80;
                    output.Add(rec);
                    break;
            }
            return output;
        }
        public void RotateHitBox(BlockModel block)
        {
            AddRotationForm(block);
            block.HitBox = GetHitBoxes(block);
        }

        private void AddRotationForm(BlockModel block)
        {
            if (block.RotatinForm == 4)
                block.RotatinForm = 1;
            else
                block.RotatinForm += 1;
        }

        

        

    }
}

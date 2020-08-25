using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisLibrary
{
    internal class HitboxManager
    {
        //TODO - vratí hitboxi už i s jednotlivými x a y
        public List<Rectangle> GetHitBoxes(BlockType type,BlockModel block)
        {
            List<Rectangle> output = new List<Rectangle>();
            switch (type)
            {
                case BlockType.leftLShape:
                    Rectangle rec = new Rectangle(0,0,40,40);
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

                case BlockType.rightLShape:
                    
                    break;

                case BlockType.leftZShape:
                    break;

                case BlockType.rightZShape:
                    break;

                case BlockType.longBlock:
                    break;

                case BlockType.squareBlock:
                    break;

                case BlockType.TShapeBlock:
                    break;

                default:
                    break;
            }
            return output;
        }

        //private List<Rectangle> AddRectangle(int count)
        //{
        //    List<Rectangle> output = new List<Rectangle>();
        //    Rectangle defRec = new Rectangle(0, 0, 40, 40);
        //    for (int i = 0; i < count; i++)
        //    {
        //        output.Add(defRec);
        //    }
        //    return output;
        //}

    }
}

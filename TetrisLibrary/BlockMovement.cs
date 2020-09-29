using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Net.Cache;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TetrisLibrary
{
    public static class BlockMovement
    {
        public static event EventHandler<BlockModel> BlockStoped;
        public static event EventHandler<string> BlockMoved;
           
        public static bool MoveBlockDown(this BlockModel block,List<BlockModel> droppedBlocks)
        {
            int previousY = block.Y;
            block.Y += block.SquareWidth;
            if (block.DoesColideWithDroppedBlocks(droppedBlocks))
            {
                block.Y = previousY;
                BlockStoped.Invoke(null, block);
                return false;
            }
            else
            {
                BlockMoved.Invoke(null, "down");
                return true;
            }
        }

        public static void MoveBlockRight(this BlockModel block, List<BlockModel> droppedBlocks)
        {
            int previousX = block.X;
            block.X += block.SquareWidth; 
            if (block.DoesColideWithDroppedBlocks(droppedBlocks))
            {
                block.X = previousX;
            }
            else
            {
                BlockMoved.Invoke(null, "right");
            }
        }

        public static void MoveBlockLeft(this BlockModel block, List<BlockModel> droppedBlocks)
        {
            int previousX = block.X;
            block.X -= block.SquareWidth;
            if (block.DoesColideWithDroppedBlocks(droppedBlocks))
            {
                block.X = previousX;
            }
            else
            {
                BlockMoved.Invoke(null, "left");
            }
        }

        public static void MoveBlockAbsoluteDown(this BlockModel block, List<BlockModel> droppedBlocks)
        {
            while (true)
            {
                if (!block.MoveBlockDown(droppedBlocks) || block.Y + block.Height == 950)
                    break;
            }
        }

        //foreach rectengle in Hitbox checked if it is above the row that was deleted if yes it move it down
        public static void DropDownRectangles(this BlockModel block,int rowY)
        {
            for (int i = 0; i < block.Hitbox.Count; i++)
            {
                Rectangle rectangleHitboxToDrop = block.Hitbox[i];
                if (rectangleHitboxToDrop.Y < rowY)
                {
                    rectangleHitboxToDrop.Y += block.SquareWidth;
                }
                block.Hitbox[i] = rectangleHitboxToDrop;
            }

        }
    }
}

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
    public class BlockMovement
    {
        public event EventHandler<BlockModel> BlockStoped;
        public event EventHandler<string> BlockMoved;
           
        public bool MoveBlockDown(List<BlockModel> droppedBlocks, BlockModel block)
        {
            int previousY = block.Y;
            block.Y += block.SquareWidth;
            if (block.DoesColideWithDroppedBlocks(droppedBlocks))
            {
                block.Y = previousY;
                BlockStoped.Invoke(this, block);
                return false;
            }
            else
            {
                BlockMoved.Invoke(this, "down");
                return true;
            }
        }

        public void MoveBlockRight(List<BlockModel> droppedBlocks,BlockModel block)
        {
            int previousX = block.X;
            block.X += block.SquareWidth; 
            if (block.DoesColideWithDroppedBlocks(droppedBlocks))
            {
                block.X = previousX;
            }
            else
            {
                BlockMoved.Invoke(this, "right");
            }
        }

        public void MoveBlockLeft(List<BlockModel> droppedBlocks, BlockModel block)
        {
            int previousX = block.X;
            block.X -= block.SquareWidth;
            if (block.DoesColideWithDroppedBlocks(droppedBlocks))
            {
                block.X = previousX;
            }
            else
            {
                BlockMoved.Invoke(this, "left");
            }
        }

        public void MoveBlockAbsoluteDown(List<BlockModel> droppedBlocks,BlockModel block)
        {
            while (true)
            {
                if (!MoveBlockDown(droppedBlocks,block) || block.Y + block.Height == 950)
                    break;
            }
        }        
    }
}

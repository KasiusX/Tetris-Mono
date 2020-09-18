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

        public void CheckForMove(KeyboardState state, BlockModel blockToMove,List<BlockModel> droppedBlocks, bool moveLeft, bool moveRight, bool moveDown)
        {
            if (state.IsKeyDown(Keys.A) && blockToMove.X > 25 && moveLeft)
            {                   
                    MoveLeft(droppedBlocks, blockToMove);              
            }
            if ((state.IsKeyDown(Keys.D) && blockToMove.X + blockToMove.Width < 425) && moveRight)
            {
                MoveRight(droppedBlocks, blockToMove);
                
            }
            if (state.IsKeyDown(Keys.S) && moveDown)
            {
                MoveDown(droppedBlocks, blockToMove);                
            }
        }

         public void CheckForRotation(KeyboardState state, BlockModel blockToMove, bool rotate, HitboxManager manager, List<BlockModel> droppedBlocks)
        {
            if(state.IsKeyDown(Keys.W) && rotate && blockToMove != null)
            {
                blockToMove.Rotate(manager, droppedBlocks);                
            }
        }

        public void CheckForAbsoluteDown(KeyboardState state, BlockModel blockToMove, bool dropDown, List<BlockModel> droppedBlocks)
        {
            if(state.IsKeyDown(Keys.Space) && dropDown && blockToMove != null)
            {
                MoveAbsoluteDown(droppedBlocks, blockToMove);
            }

        }

        public bool MoveDown(List<BlockModel> droppedBlocks, BlockModel block)
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

        public void MoveRight(List<BlockModel> droppedBlocks,BlockModel block)
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

        public void MoveLeft(List<BlockModel> droppedBlocks, BlockModel block)
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

        public void MoveAbsoluteDown(List<BlockModel> droppedBlocks,BlockModel block)
        {
            while (true)
            {
                if (!MoveDown(droppedBlocks,block) || block.Y + block.Height == 950)
                    break;
            }
        }        
    }
}

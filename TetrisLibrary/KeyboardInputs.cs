using Microsoft.Xna.Framework.Input;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisLibrary
{
    public class KeyboardInputs
    {           
        public void CheckForMove(KeyboardState state, BlockModel blockToMove, List<BlockModel> droppedBlocks, bool moveLeft, bool moveRight, bool moveDown)
        {
            if (state.IsKeyDown(Keys.A) && blockToMove.X > 25 && moveLeft)
            {
                blockToMove.MoveBlockLeft(droppedBlocks);
            }
            if ((state.IsKeyDown(Keys.D) && blockToMove.X + blockToMove.Width < 425) && moveRight)
            {
                blockToMove.MoveBlockRight(droppedBlocks);

            }
            if (state.IsKeyDown(Keys.S) && moveDown)
            {
                blockToMove.MoveBlockDown(droppedBlocks);
            }
        }        

        public void CheckForRotation(KeyboardState state, BlockModel blockToRotate, bool rotate, HitboxManager manager, List<BlockModel> droppedBlocks)
        {
            if (state.IsKeyDown(Keys.W) && rotate && blockToRotate != null)
            {
                blockToRotate.RotateBlock(manager, droppedBlocks);
            }
        }

        public void CheckForAbsoluteDown(KeyboardState state, BlockModel blockToMove, bool dropDown, List<BlockModel> droppedBlocks)
        {
            if (state.IsKeyDown(Keys.Space) && dropDown && blockToMove != null)
            {
                blockToMove.MoveBlockAbsoluteDown(droppedBlocks);
            }

        }
    }
}

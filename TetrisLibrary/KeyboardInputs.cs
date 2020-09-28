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
        BlockMovement blockMovement;
        BlockRotation blockRotation;

        public KeyboardInputs(BlockMovement movement, BlockRotation rotation)
        {
            blockMovement = movement;
            blockRotation = rotation;
        }
        public void CheckForMove(KeyboardState state, BlockModel blockToMove, List<BlockModel> droppedBlocks, bool moveLeft, bool moveRight, bool moveDown)
        {
            if (state.IsKeyDown(Keys.A) && blockToMove.X > 25 && moveLeft)
            {
                blockMovement.MoveBlockLeft(droppedBlocks, blockToMove);
            }
            if ((state.IsKeyDown(Keys.D) && blockToMove.X + blockToMove.Width < 425) && moveRight)
            {
                blockMovement.MoveBlockRight(droppedBlocks, blockToMove);

            }
            if (state.IsKeyDown(Keys.S) && moveDown)
            {
                blockMovement.MoveBlockDown(droppedBlocks, blockToMove);
            }
        }        

        public void CheckForRotation(KeyboardState state, BlockModel blockToRotate, bool rotate, HitboxManager manager, List<BlockModel> droppedBlocks)
        {
            if (state.IsKeyDown(Keys.W) && rotate && blockToRotate != null)
            {
                blockRotation.RotateBlock(manager, droppedBlocks, blockToRotate);
            }
        }

        public void CheckForAbsoluteDown(KeyboardState state, BlockModel blockToMove, bool dropDown, List<BlockModel> droppedBlocks)
        {
            if (state.IsKeyDown(Keys.Space) && dropDown && blockToMove != null)
            {
                blockMovement.MoveBlockAbsoluteDown(droppedBlocks, blockToMove);
            }

        }
    }
}

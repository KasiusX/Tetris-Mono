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
        BlockMovement movement;

        public KeyboardInputs(BlockMovement movement)
        {
            this.movement = movement;
        }
        public void CheckForMove(KeyboardState state, BlockModel blockToMove, List<BlockModel> droppedBlocks, bool moveLeft, bool moveRight, bool moveDown)
        {
            if (state.IsKeyDown(Keys.A) && blockToMove.X > 25 && moveLeft)
            {
                movement.MoveBlockLeft(droppedBlocks, blockToMove);
            }
            if ((state.IsKeyDown(Keys.D) && blockToMove.X + blockToMove.Width < 425) && moveRight)
            {
                movement.MoveBlockRight(droppedBlocks, blockToMove);

            }
            if (state.IsKeyDown(Keys.S) && moveDown)
            {
                movement.MoveBlockDown(droppedBlocks, blockToMove);
            }
        }        

        public void CheckForRotation(KeyboardState state, BlockModel blockToMove, bool rotate, HitboxManager manager, List<BlockModel> droppedBlocks)
        {
            if (state.IsKeyDown(Keys.W) && rotate && blockToMove != null)
            {
                blockToMove.Rotate(manager, droppedBlocks);
            }
        }

        public void CheckForAbsoluteDown(KeyboardState state, BlockModel blockToMove, bool dropDown, List<BlockModel> droppedBlocks)
        {
            if (state.IsKeyDown(Keys.Space) && dropDown && blockToMove != null)
            {
                movement.MoveBlockAbsoluteDown(droppedBlocks, blockToMove);
            }

        }
    }
}

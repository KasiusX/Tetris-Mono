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
        
        public void CheckForMove(KeyboardState state, BlockModel block,List<BlockModel> droppedBlocks, bool moveLeft, bool moveRight, bool moveDown)
        {
            if (state.IsKeyDown(Keys.A) && block.X > 25 && moveLeft)
            {                
                    block.MoveLeft(droppedBlocks);              
            }
            if ((state.IsKeyDown(Keys.D) && block.X + block.Width < 425) && moveRight)
            {
                block.MoveRight(droppedBlocks);
                
            }
            if (state.IsKeyDown(Keys.S) && moveDown)
            {
                block.MoveDown(droppedBlocks);
                
            }
        }

         public void CheckForRotation(KeyboardState state, BlockModel block, bool rotate, HitboxManager manager, List<BlockModel> droppedBlocks)
        {
            if(state.IsKeyDown(Keys.W) && rotate && block != null)
            {
                block.Rotate(manager, droppedBlocks);                
            }
        }

        

    }
}

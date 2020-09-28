using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TetrisLibrary
{
    public static class BlockColider
    {
        public static bool DoesBlockColideWithBlock(this BlockModel firstBlock, BlockModel secondBlock)
        {
            foreach (Rectangle thisRectangleHitbox in firstBlock.Hitbox)
            {
                foreach (Rectangle newRectangleHitbox in secondBlock.Hitbox)
                {
                    if (thisRectangleHitbox.Intersects(newRectangleHitbox))
                        return true;
                }
            }
            return false;
        }

        public static bool DoesColideWithDroppedBlocks(this BlockModel block, List<BlockModel> droppedBlocks)
        {
            foreach (BlockModel droppedBlock in droppedBlocks)
            {
                if (droppedBlock.DoesBlockColideWithBlock(block))
                    return true;
            }
            return false;
        }
    }
}

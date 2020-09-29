using System;
using System.Collections.Generic;
 
namespace TetrisLibrary
{
    public static class BlockRotation
    {
        public static event EventHandler<BlockModel> Rotated;
        public static void RotateBlock(this BlockModel blockToRotation,HitboxManager manager, List<BlockModel> droppedBlocks)
        {
            BlockModel backup = blockToRotation;
            int x = blockToRotation.X;
            int y = blockToRotation.Y;

            blockToRotation.ChangePositionOn(0, 0);
            manager.GetRotatedHitbox(blockToRotation);
            blockToRotation.ChangePositionOn(x, y);

            SwitchWideHeight(blockToRotation);
            while(blockToRotation.IsAwayFromField())
            {
                blockToRotation.MoveBlockLeft(droppedBlocks);
            }
            Rotated.Invoke(null, blockToRotation);

            if (blockToRotation.DoesColideWithDroppedBlocks(droppedBlocks))
               LoadBackup(backup, blockToRotation);                        
        }

        private static void LoadBackup(BlockModel backup,BlockModel blockToRotation)
        {
            blockToRotation.X = backup.X;
            blockToRotation.Y = backup.Y;
            blockToRotation.Hitbox = backup.Hitbox;
            blockToRotation.RotationForm = backup.RotationForm;
        }

        private static void SwitchWideHeight(BlockModel blockToRotation)
        {
            int width = blockToRotation.Width;
            blockToRotation.Width = blockToRotation.Height;
            blockToRotation.Height = width;
        }        
    }
}

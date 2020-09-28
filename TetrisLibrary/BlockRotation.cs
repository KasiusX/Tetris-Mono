using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisLibrary
{
    public class BlockRotation
    {
        public event EventHandler<BlockModel> Rotated;
        public void RotateBlock(HitboxManager manager, List<BlockModel> droppedBlocks, BlockModel blockToRotation)
        {
            BlockModel backup = blockToRotation;
            int x = blockToRotation.X;
            int y = blockToRotation.Y;

            blockToRotation.ChangePositionOn(0, 0);
            manager.GetRotatedHitbox(blockToRotation);
            blockToRotation.ChangePositionOn(x, y);

            SwitchWideHeight(blockToRotation);
            Rotated.Invoke(this, blockToRotation);

            if (blockToRotation.DoesColideWithDroppedBlocks(droppedBlocks))
               LoadBackup(backup, blockToRotation);                        
        }

        private void LoadBackup(BlockModel backup,BlockModel blockToRotation)
        {
            blockToRotation.X = backup.X;
            blockToRotation.Y = backup.Y;
            blockToRotation.Hitbox = backup.Hitbox;
            blockToRotation.RotationForm = backup.RotationForm;
        }

        private void SwitchWideHeight(BlockModel blockToRotation)
        {
            int width = blockToRotation.Width;
            blockToRotation.Width = blockToRotation.Height;
            blockToRotation.Height = width;
        }        
    }
}

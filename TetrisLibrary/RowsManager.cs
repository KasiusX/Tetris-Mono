using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace TetrisLibrary
{
    public class RowsManager
    {
        public int[] RowCounts { get; set; } = new int[20];
        public event EventHandler<int> RowFilled;
        public event EventHandler GameEnded;

        public RowsManager()
        {
            EmptyRowCounts();
        }
        
        public void EmptyRowCounts()
        {
            for (int i = 0; i < RowCounts.Length; i++)
            {
                RowCounts[i] = 0;
            }
        }

        private  void CheckForCompleteRow()
        {
            int row = 0;
            foreach (int i in RowCounts)
            {
                row++;
                if (i == 10)
                {
                    RowCounts[row - 1] = 0;
                    RowFilled.Invoke(this, row);
                }
            }
        }
                
        public void AddBlock(BlockModel block, bool checkForCompleteRow = true)
        {
            foreach (Rectangle rec in block.Hitbox)
            {
                int i = (rec.Y-150) / 40;
                RowCounts[i]++;
            }
            if(checkForCompleteRow)
            CheckForCompleteRow();
        }

        public void ResetRows(List<BlockModel> blocks)
        {
            EmptyRowCounts();
            foreach (BlockModel block in blocks)
            {
                AddBlock(block, false);
            }
        }

        public void CheckForEnd()
        {
            if (RowCounts[0] > 0)
                GameEnded.Invoke(this, EventArgs.Empty);
        }

    }
}

using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace TetrisLibrary
{
    public class BlockMovement
    {
        public event EventHandler<string> Moved;
        public void CheckForMove(KeyboardState state, BlockModel block, bool moveLeft, bool moveRight, bool moveDown)
        {
            if ((state.IsKeyDown(Keys.A) && block.X > 25) && moveLeft)
            {
                block.MoveLeft();
                Moved.Invoke(this, "left");
            }
            if ((state.IsKeyDown(Keys.D) && block.X + block.Width < 425) && moveRight)
            {
                block.MoveRight();
                Moved.Invoke(this, "right");
            }
            if (state.IsKeyDown(Keys.S) && moveDown)
            {
                block.MoveDown();
                Moved.Invoke(this, "down");
            }
        }
    }
}

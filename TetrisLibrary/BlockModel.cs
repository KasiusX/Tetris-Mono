using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net.Configuration;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace TetrisLibrary
{
    public class BlockModel
    {

        private int _X;

        public int X
        {
            get { return _X; }
            internal set {
                int moveX = value - _X;
                ChangeX(moveX);
                _X = value;
            }
        }

        private int _Y;

        public int Y
        {
            get { return _Y; }
            internal set {
                int moveY = value - _Y;
                ChangeY(moveY);
                _Y = value;
            }
        }

        public int Width { get; set; }
        public int Height{ get; set; }
        public int SquareWidth { get; set; } = 40;
        public List<Rectangle> Hitbox { get; set; }
        public BlockType Type { get; set; }
        public Color Color { get; set; }
        public int RotationForm { get; set; } = 1;

        public event EventHandler Rotated;

        public BlockModel(BlockType type, Color color, int width, int height)
        {
            Type = type;
            Color = color;
            Width = width;
            Height = height;
            HitboxManager manager = new HitboxManager();
            Hitbox = manager.GetHitBoxes(this);
        } 
        
        private bool ColideWithSomething(List<BlockModel> droppedBlocks)
        {
            foreach (BlockModel block in droppedBlocks)
            {
                if (block.DoesColideWithBlockHitbox(Hitbox))
                    return true;
            }
            return false;
        }

        private bool DoesColideWithBlockHitbox(List<Rectangle> hitbox)
        {
            foreach (Rectangle thisRectangleHitbox in Hitbox)
            {
                foreach (Rectangle newRectangleHitbox in hitbox)
                {
                    if (thisRectangleHitbox.Intersects(newRectangleHitbox))
                        return true;
                }
            }
            return false;
        }
        public void ChangePositionOn(int x, int y)
        {
            X = x;
            Y = y;
        }

        //changes y for each rectangle in Hitbox based on y change
        private void ChangeY(int moveY)
        {
            for (int i = 0; i < Hitbox.Count; i++)
            {
                Rectangle rectangleToMove = Hitbox[i];
                rectangleToMove.Y += moveY;
                Hitbox[i] = rectangleToMove;
            }
        }

        //changes x for each rectangle in Hitbox based on x change
        private void ChangeX(int moveX)
        {
            for(int i = 0; i < Hitbox.Count; i++)
            {
                Rectangle rectangleToMove = Hitbox[i];
                rectangleToMove.X += moveX;
                Hitbox[i] = rectangleToMove;
            }
        }

        public bool DoesColideWithDroppedBlocks(List<BlockModel> droppedBlocks)
        {
            foreach (BlockModel droppedBlock in droppedBlocks)
            {
                if (droppedBlock.DoesColideWithBlockHitbox(Hitbox))
                    return true;
            }
            return false;
        }

        public void Rotate(HitboxManager manager, List<BlockModel> droppedBlocks)
        {
            BlockModel backup = this;
            int x = X;
            int y = Y;
            ChangePositionOn(0, 0);
            manager.RotateHitBox(this);
            Rotated.Invoke(this, EventArgs.Empty);

            ChangePositionOn(x, y);
            SwitchWideHeight();
            CheckIfIsOutside();

            if (ColideWithSomething(droppedBlocks))
                LoadBackup(backup);
        }

        private void LoadBackup(BlockModel backup)
        {
            X = backup.X;
            Y = backup.Y;
            Hitbox = backup.Hitbox;
            RotationForm = backup.RotationForm;
        }        

        private void SwitchWideHeight()
        {
            int width = Width;
            Width = Height;
            Height = width;
        }

        private bool CheckIfIsOutside()
        {
            for(int i = 0; i < Hitbox.Count; i++)
            {
                if (Hitbox[i].X >= 400)
                {
                    return true;                  
                }
            }
            return false;
        }
        //foreach rectengle in Hitbox checked if it is above the row that was deleted if yes it move it down
        public void DropDownRectangles(int rowY)
        {
            for (int i = 0; i < Hitbox.Count; i++)
            {
                Rectangle rectangleHitboxToDrop = Hitbox[i];
                if (rectangleHitboxToDrop.Y < rowY)
                {
                    rectangleHitboxToDrop.Y += SquareWidth;
                }
                Hitbox[i] = rectangleHitboxToDrop;
            }  

        }

        
    }
}

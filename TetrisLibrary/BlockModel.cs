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
            private set {
                int moveX = value - _X;
                ChangeX(moveX);
                if (_X < value)
                    Moved?.Invoke(this, "right");
                else if (_X > value)
                    Moved?.Invoke(this, "left");
                _X = value;
            }
        }

        private int _Y;

        public int Y
        {
            get { return _Y; }
            private set {
                int moveY = value - _Y;
                ChangeY(moveY);
                Moved?.Invoke(this, "down");
                _Y = value;
            }
        }

        public int Width { get; set; }
        public int Height{ get; set; }
        public int BlockWidth { get; set; } = 40;
        public List<Rectangle> Hitbox { get; set; }
        public BlockType Type { get; set; }
        public Color Color { get; set; }
        public int RotationForm { get; set; } = 1;

        public event EventHandler<string> Moved;
        public event EventHandler Stoped;
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

        public bool MoveDown(List<BlockModel> droppedBlocks)
        {            
            int previousY = Y;
            Y += BlockWidth;
            if (ColideWithSomething(droppedBlocks))
            {
                Y = previousY;
                Stoped.Invoke(this, EventArgs.Empty);
                return false;
            }
            else
                return true;
        }                   

        public void MoveRight(List<BlockModel> droppedBlocks)
        {
            int previousX = X;
            X += BlockWidth;
            if (ColideWithSomething(droppedBlocks))
            {
                X = previousX;
            }
        }

        public void MoveLeft(List<BlockModel> droppedBlocks)
        {
            int previousX = X;
            X -= BlockWidth;
            if(ColideWithSomething(droppedBlocks))
            {
                X = previousX;
            }
        }
        private bool ColideWithSomething(List<BlockModel> droppedBlocks)
        {
            foreach (BlockModel block in droppedBlocks)
            {
                if (block.ColideWith(Hitbox))
                    return true;
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

        public bool ColideWith(List<Rectangle> hitbox)
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
            CheckIfIsOutside(droppedBlocks);

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

        private void CheckIfIsOutside(List<BlockModel> droppedBlocks)
        {
            for(int i = 0; i < Hitbox.Count; i++)
            {
                if (Hitbox[i].X >= 400)
                {
                    MoveLeft(droppedBlocks);                  
                }
            }
        }
        //foreach rectengle in Hitbox checked if it is above the row that was deleted if yes it move it down
        public void DropDownRectangles(int rowY)
        {
            for (int i = 0; i < Hitbox.Count; i++)
            {
                Rectangle rectangleHitboxToDrop = Hitbox[i];
                if (rectangleHitboxToDrop.Y < rowY)
                {
                    rectangleHitboxToDrop.Y += BlockWidth;
                }
                Hitbox[i] = rectangleHitboxToDrop;
            }  

        }

        public void MoveAbsoluteDown(List<BlockModel> droppedBlocks)
        {
            while (true)
            {                
                if (!MoveDown(droppedBlocks) || Y + Height == 950)
                    break;
            }
        }
    }
}

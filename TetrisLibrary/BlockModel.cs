using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace TetrisLibrary
{
    public class BlockModel
    {

        public int X { get; private set; }
        public int Y { get; private set; }
        public int Width { get; set; }
        public int Height{ get; set; }
        public int Speed { get; set; } = 40;
        public List<Rectangle> HitBox { get; set; }
        public BlockType Type { get; set; }
        public Color Color { get; set; }
        public int RotatinForm { get; set; } = 1;

        public event EventHandler<string> Moved;
        public event EventHandler Stoped;
        public event EventHandler Rotated;
        public BlockModel()
        {

        }
        public BlockModel(int startingY,BlockType type, Color color, int width, int height)
        {
            Type = type;
            Color = color;
            Width = width;
            Height = height;
            HitboxManager manager = new HitboxManager();
            HitBox = manager.GetHitBoxes(this);
        }
        public void MoveDown(List<BlockModel> droppedBlocks)
        {
            if (CanMakeMove(droppedBlocks,0, Speed))
            {
                Y += Speed;
                ChangeY(Speed);
                Moved.Invoke(this, "down");
            }
            else
            {
                Stoped.Invoke(this, EventArgs.Empty);
            }
        }

        public void MoveAbsoluteDown(List<BlockModel> droppedBlocks, int maxY)
        {
            bool canMove = true;
            while(canMove)
            {
                if (Y + Height != maxY)
                {
                    Y += Speed;
                    ChangeY(Speed);
                }
                else
                    canMove = false;
            }
        }

        public void MoveRight(List<BlockModel> droppedBlocks)
        {
            if (CanMakeMove(droppedBlocks, Speed, 0))
            {
                X += Speed;
                ChangeX(Speed);
                Moved.Invoke(this, "right");
            }
        }

        public void MoveLeft(List<BlockModel> droppedBlocks)
        {
            if (CanMakeMove(droppedBlocks, -Speed, 0))
            {
                X -= Speed;
                ChangeX(-Speed);
                Moved.Invoke(this, "left");
            }
        }        

        public void ChangePositionOn(int x, int y)
        {
            int moveX = x - X;
            ChangeX(moveX);
            int moveY = y - Y;
            ChangeY(moveY);
            X = x;
            Y = y;
        }

        private void ChangeY(int move)
        {
            List<Rectangle> newHitBox = new List<Rectangle>();
            foreach (Rectangle r in HitBox)
            {
                Rectangle rec = r;
                rec.Y += move;
                newHitBox.Add(rec);
            }
            HitBox = newHitBox;
        }

        private void ChangeX(int move)
        {
            List<Rectangle> newHitBox = new List<Rectangle>();
            foreach (Rectangle r in HitBox)
            {
                Rectangle rec = r;
                rec.X += move;
                newHitBox.Add(rec);
            }
            HitBox = newHitBox;
        }

        public bool CheckIfIsDown(int windowHeight)
        {
           return Y + Height == windowHeight - 25;
        }

        private bool CanMakeMove( List<BlockModel> droppedBlocks, int changeX, int changeY)
        {
            List<Rectangle> testHitBox = MakeTestHitBox(changeX, changeY);
            
            foreach (BlockModel block in droppedBlocks)
            {
                if (block.ColideWith(testHitBox))
                    return false;
            }
            return true;
        }

        private List<Rectangle> MakeTestHitBox(int changeX, int changeY)
        {
            List<Rectangle> output = new List<Rectangle>();
            foreach (Rectangle r in HitBox)
            {
                Rectangle rec = r;
                rec.X += changeX;
                rec.Y += changeY;
                output.Add(rec);
            }
            return output;        
        }

        

        public bool ColideWith(List<Rectangle> hitbox)
        {
            foreach (Rectangle thisRec in HitBox)
            {
                foreach (Rectangle newRec in hitbox)
                {
                    if (thisRec.Intersects(newRec))
                        return true;
                }
            }
            return false;
        }

        public void ResetPosition()
        {
            X = 0;
            Y = 0;
        }

        public void RotatedInvoke()
        {
            Rotated.Invoke(this, EventArgs.Empty);
        }

        public void Rotate(HitboxManager manager, List<BlockModel> droppedBlocks)
        {
            if (CanRotate(manager,droppedBlocks))
            {
                manager.RotateHitBox(this);
                Rotated.Invoke(this, EventArgs.Empty);
                int x = X;
                int y = Y;
                ResetPosition();
                ChangePositionOn(x, y);
                SwitchWideHeight();
                CheckIfIsOutside(droppedBlocks);
            }
        }

        private bool CanRotate(HitboxManager manager,List<BlockModel> droppedBlocks)
        {
            BlockModel testBlock = new BlockModel { Type = Type, RotatinForm = RotatinForm == 4? 1: RotatinForm+1 };
            testBlock.HitBox = manager.GetHitBoxes(testBlock);
            testBlock.ChangePositionOn(X, Y);
            foreach (BlockModel block in droppedBlocks)
            {
                if (block.ColideWith(testBlock.HitBox))
                {
                    return false;
                }
            }
            return true;
        }

        private void SwitchWideHeight()
        {
            int width = Width;
            Width = Height;
            Height = width;
        }

        private void CheckIfIsOutside(List<BlockModel> droppedBlocks)
        {
            foreach (var rec in HitBox)
            {
                if (rec.X >= 400)
                    MoveLeft(droppedBlocks);
            }
        }

        private void DropDownRotationForm()
        {
            if (RotatinForm == 1)
                RotatinForm = 4;
            else
                RotatinForm -= 1;
        }

        public void MoveDownRectangle(int y)
        {
            List<Rectangle> recToMove = new List<Rectangle>();
            foreach (Rectangle rectangle in HitBox)
            {
                if (rectangle.Y < y)
                    recToMove.Add(rectangle);
            }
            foreach (Rectangle rectangle1 in recToMove)
            {
                HitBox.Remove(rectangle1);
                Rectangle rec = rectangle1;
                rec.Y += 40;
                HitBox.Add(rec);
            }
        }
        
    }
}

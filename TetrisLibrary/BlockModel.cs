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

        private int _X;

        public int X
        {
            get { return _X; }
            private set {
                int moveX = value - _X;
                ChangeX(moveX);
                if (_X < value)
                    Moved.Invoke(this, "right");
                else if (_X > value)
                    Moved.Invoke(this, "left");
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
                Moved.Invoke(this, "down");
                _Y = value;
            }
        }

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
        public BlockModel(BlockType type, Color color, int width, int height)
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
            }
            else
            {
                Stoped.Invoke(this, EventArgs.Empty);
            }
        }               

        public void MoveRight(List<BlockModel> droppedBlocks)
        {
            if (CanMakeMove(droppedBlocks, Speed, 0))
            {
                X += Speed;
            }
        }

        public void MoveLeft(List<BlockModel> droppedBlocks)
        {
            if (CanMakeMove(droppedBlocks, -Speed, 0))
            {
                X -= Speed;
            }
        }        

        public void ChangePositionOn(int x, int y)
        {
            X = x;
            Y = y;
        }

        private void ChangeY(int moveY)
        {
            List<Rectangle> newHitBox = new List<Rectangle>();
            foreach (Rectangle r in HitBox)
            {
                Rectangle rec = r;
                rec.Y += moveY;
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

        public void Rotate(HitboxManager manager, List<BlockModel> droppedBlocks)
        {
            if (CanRotate(manager,droppedBlocks))
            {
                int x = X;
                int y = Y;               
                ChangePositionOn(0, 0);
                manager.RotateHitBox(this);
                Rotated.Invoke(this, EventArgs.Empty);
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

        public void MoveAbsoluteDown(List<BlockModel> droppedBlocks)
        {
            while (true)
            {
                if (CanMakeMove(droppedBlocks, 0, Speed) && Y + Height != 950)
                {
                    MoveDown(droppedBlocks);
                }
                else
                {
                    Stoped.Invoke(this, EventArgs.Empty);
                    break;
                }
            }
        }


        
        
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
        public BlockModel(int startingY,BlockType type, Color color, int width, int height)
        {
            Type = type;
            Color = color;
            Width = width;
            Height = height;
            HitboxManager manager = new HitboxManager();
            HitBox = manager.GetHitBoxes(Type,this);
            ChangePositionOn(25 + 4 * 40, startingY);
            
        }
        public void MoveDown()
        {
            Y += Speed;
            ChangeY(Speed);
        }

        public void MoveRight()
        {
            X += Speed;
            ChangeX(Speed);
        }

        public void MoveLeft()
        {
            X -= Speed;
            ChangeX(-Speed);
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
    }
}

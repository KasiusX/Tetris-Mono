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

        public BlockModel(BlockType type, Color color, int width, int height)
        {
            Type = type;
            Color = color;
            Width = width;
            Height = height;
            HitboxManager manager = new HitboxManager();
            Hitbox = manager.GetHitBoxes(this);
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
    }
}

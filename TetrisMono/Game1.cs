using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using TetrisLibrary;

namespace TetrisMono
{
    public class Game1 : Game
    {
        const int fieldHeight = 800;
        const int fieldWidth = 400;
        const int space = 25;
        const int nextBlockSize = 100;
        const int startingY = 150;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D gameField;
        Texture2D gameBackground;
        Texture2D nextBlockFieldSprite;
        Texture2D nextBlockSprite;
        Texture2D blockSprite;
        SpriteFont gameFont;
        SpriteFont endGameFont;

        BlockModel activeBlock;
        BlockModel nextBlock;
        BlockManager blockManager;
        BlockMovement blockMovement;
        RowsManager rowsManager;

        int secondOfMoveDown;
        bool moveRight = true;
        bool moveLeft = true;
        bool moveDown = true;
        bool rotate = true;
        bool end = false;
        bool dropDown = true;
        bool restart = false;
        public int Score { get; set; }

        List<BlockModel> droppedBlocks = new List<BlockModel>();
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 975;
            graphics.PreferredBackBufferWidth = 450;
        }


        protected override void Initialize()
        {
            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameField = Content.Load<Texture2D>("gameField-sprite");
            gameBackground = Content.Load<Texture2D>("gameBackground-sprite");
            nextBlockFieldSprite = Content.Load<Texture2D>("nextBlock-sprite");
            blockSprite = Content.Load<Texture2D>("square-sprite");
            gameFont = Content.Load<SpriteFont>("game-font");
            endGameFont = Content.Load<SpriteFont>("EndGameFont");
            blockManager = new BlockManager(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, startingY);
            blockMovement = new BlockMovement();
            rowsManager = new RowsManager();
            rowsManager.RowFilled += RowsManager_RowFilled;
            rowsManager.GameEnded += RowsManager_GameEnded;
        }

        private void RowsManager_GameEnded(object sender, EventArgs e)
        {
            end = true;
        }

        private void RowsManager_RowFilled(object sender, int e)
        {
            Score += 1;
            RemoveRow(e);
        }

        private void BlockMovement_Moved(object sender, string e)
        {
            if (e == "left")
                moveLeft = false;
            else if (e == "right")
                moveRight = false;
            else if (e == "down")
            {
                moveDown = false;
                dropDown = false;
            }
        }

        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            if (!end)
            {
                if (activeBlock == null)
                {
                    NewBlock();

                }
                if (secondOfMoveDown != gameTime.TotalGameTime.Seconds)
                {
                    activeBlock.MoveDown(droppedBlocks);
                    secondOfMoveDown = gameTime.TotalGameTime.Seconds;
                }


                CheckForPlayerMovement();
                                
                
                CheckIfBlockIsDown();
            }
            else
            {
                CheckForRestart();
            }

            CheckForResetBools();

            base.Update(gameTime);
        }

        private void ActiveBlock_Stoped(object sender, System.EventArgs e)
        {
            NewBlock();
            rowsManager.CheckForEnd();
        }

        private void CheckForPlayerMovement()
        {
            blockMovement.CheckForMove(Keyboard.GetState(), activeBlock, droppedBlocks, moveLeft, moveRight, moveDown);
            blockMovement.CheckForRotation(Keyboard.GetState(), activeBlock, rotate, blockManager.manager, droppedBlocks);
            blockMovement.CheckForAbsoluteDown(Keyboard.GetState(), activeBlock, dropDown, droppedBlocks);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            DrawField();
            spriteBatch.DrawString(gameFont, $"Score: {Score}", new Vector2(25, 25), Color.White);

            if (activeBlock != null)
            {
                DrawBlock(activeBlock);
            }
            foreach (BlockModel block in droppedBlocks)
            {
                DrawBlock(block);
            }
            spriteBatch.Draw(nextBlockSprite, new Rectangle(graphics.PreferredBackBufferWidth - space - nextBlockSize + 5, space + 5, nextBlockSize - 10, nextBlockSize - 10), nextBlock.Color);

            if (end)
            {
                spriteBatch.DrawString(endGameFont, "GAME OVER", new Vector2(60, 360), Color.Black);
                spriteBatch.DrawString(endGameFont, $"Score: {Score}", new Vector2(60, 410), Color.Black);
                spriteBatch.DrawString(gameFont, "(press enter to restart)", new Vector2(60, 460), Color.Black);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawBlock(BlockModel block)
        {
            foreach (var r in block.Hitbox)
            {
                spriteBatch.Draw(blockSprite, r, block.Color);
            }
        }

        private void DrawField()
        {
            spriteBatch.Draw(gameBackground, new Rectangle { X = 0, Y = 0, Width = graphics.PreferredBackBufferWidth, Height = graphics.PreferredBackBufferHeight }, Color.White);
            spriteBatch.Draw(gameField, new Rectangle { X = space, Y = nextBlockSize + 2 * space, Width = fieldWidth, Height = fieldHeight }, Color.White);
            spriteBatch.Draw(nextBlockFieldSprite, new Rectangle { X = graphics.PreferredBackBufferWidth - nextBlockSize - space, Y = space, Width = nextBlockSize, Height = nextBlockSize }, Color.White);
        }

        private void CheckForResetBools()
        {
            if (!Keyboard.GetState().IsKeyDown(Keys.A))
                moveLeft = true;
            if (!Keyboard.GetState().IsKeyDown(Keys.D))
                moveRight = true;
            if (!Keyboard.GetState().IsKeyDown(Keys.S))
                moveDown = true;
            if (!Keyboard.GetState().IsKeyDown(Keys.W))
                rotate = true;
            if (!Keyboard.GetState().IsKeyDown(Keys.Enter))
                restart = true;
            if (!Keyboard.GetState().IsKeyDown(Keys.Space))
                dropDown = true;
        }

        private void CheckIfBlockIsDown()
        {
            if (activeBlock.Y + activeBlock.Height == graphics.PreferredBackBufferHeight -25)
            {
                NewBlock();
            }
        }

        private void NewBlock()
        {
            if (activeBlock != null)
            {
                droppedBlocks.Add(activeBlock);
                rowsManager.AddBlock(activeBlock);
            }
            else
            {
                nextBlock = blockManager.GenerateRandomBlock();
            }
            activeBlock = nextBlock;
            activeBlock.Moved += BlockMovement_Moved;
            activeBlock.Stoped += ActiveBlock_Stoped;
            activeBlock.Rotated += ActiveBlock_Rotated;
            activeBlock.ChangePositionOn(25 + 4 * 40, startingY);
            nextBlock = blockManager.GenerateRandomBlock();
            nextBlock.ChangePositionOn(graphics.PreferredBackBufferWidth - nextBlockSize - space, space);
            SetNextSprite(nextBlock.Type);


        }

        private void ActiveBlock_Rotated(object sender, EventArgs e)
        {
            rotate = false;
        }

        private void SetNextSprite(BlockType type)
        {
            switch (type)
            {
                case BlockType.leftLShape:
                    nextBlockSprite = Content.Load<Texture2D>("leftLShape-sprite");
                    break;
                case BlockType.rightLShape:
                    nextBlockSprite = Content.Load<Texture2D>("rightLShape-sprite");
                    break;
                case BlockType.leftZShape:
                    nextBlockSprite = Content.Load<Texture2D>("leftZShape-sprite");
                    break;
                case BlockType.rightZShape:
                    nextBlockSprite = Content.Load<Texture2D>("rightZShape-sprite");
                    break;
                case BlockType.longBlock:
                    nextBlockSprite = Content.Load<Texture2D>("longBlock-sprite");
                    break;
                case BlockType.squareBlock:
                    nextBlockSprite = Content.Load<Texture2D>("squareBlock-sprite");
                    break;
                case BlockType.TShapeBlock:
                    nextBlockSprite = Content.Load<Texture2D>("TShapeBlock-sprite");
                    break;
                default:
                    break;
            }
        }

        private void RemoveRow(int rowNumber)
        {
            int y = (rowNumber - 1) * 40 + 150;
            List<Rectangle> recToDelete = new List<Rectangle>();
            foreach (BlockModel block in droppedBlocks)
            {
                foreach (Rectangle rec in block.Hitbox)
                {
                    if (rec.Y == y)
                        recToDelete.Add(rec);
                }
                foreach (Rectangle rec in recToDelete)
                {
                    block.Hitbox.Remove(rec);
                    recToDelete = new List<Rectangle>();
                }
            }
            ClearDroppedBlocks();
            MoveBlocksDown(y);
        }

        private void MoveBlocksDown(int y)
        {            
            for (int i = 0; i < droppedBlocks.Count; i++)
            {
                droppedBlocks[i].DropDownRectangles(y);
            }
            rowsManager.ResetRows(droppedBlocks);
        }

        private void ClearDroppedBlocks()
        {
            List<BlockModel> blocksToRemove = new List<BlockModel>();
            foreach (BlockModel block in droppedBlocks)
            {
                if (block.Hitbox.Count == 0)
                    blocksToRemove.Add(block);
            }
            foreach (BlockModel block in blocksToRemove)
            {
                droppedBlocks.Remove(block);
            }
        }

        private void CheckForRestart()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                Restart();
        }

        private void Restart()
        {
            NewBlock();
            Score = 0;
            droppedBlocks = new List<BlockModel>();
            end = false;
            rowsManager.EmptyRowCounts();
        }

    }
}

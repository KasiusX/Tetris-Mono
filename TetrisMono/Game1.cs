using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Xml.Schema;
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
        Texture2D nextBlock;
        Texture2D blockSprite;
        SpriteFont gameFont;

        BlockModel activeBlock;
        BlockManager blockManager;
        BlockMovement blockMovement;

        int secondOfMoveDown;
        bool moveRight = true;
        bool moveLeft = true;
        bool moveDown = true;
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
            nextBlock = Content.Load<Texture2D>("nextBlock-sprite");
            blockSprite = Content.Load<Texture2D>("square-sprite");
            gameFont = Content.Load<SpriteFont>("game-font");
            blockManager = new BlockManager(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, startingY);
            blockMovement = new BlockMovement();
            blockMovement.Moved += BlockMovement_Moved;
        }

        private void BlockMovement_Moved(object sender, string e)
        {
            if (e == "left")
                moveLeft = false;
            else if (e == "right")
                moveRight = false;
            else if (e == "down")
                moveDown = false;
        }

        protected override void UnloadContent()
        {
            
        }

        
        protected override void Update(GameTime gameTime)
        {
            if (activeBlock == null)
                activeBlock = blockManager.GenerateRandomBlock();
            if (secondOfMoveDown != gameTime.TotalGameTime.Seconds)
            {
                activeBlock.MoveDown();
                secondOfMoveDown = gameTime.TotalGameTime.Seconds;
            }

                           
            blockMovement.CheckForMove(Keyboard.GetState(), activeBlock,moveLeft,moveRight,moveDown);

            CheckForBoolResetSides();

            CheckIfBlockIsDown();

            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            DrawField();
            spriteBatch.DrawString(gameFont, $"Score: {Score}", new Vector2(25,25),Color.White);
            if (activeBlock != null)
            {
                foreach (var r in activeBlock.HitBox)
                {
                    spriteBatch.Draw(blockSprite, r, activeBlock.Color);
                }
            }
            foreach (BlockModel block in droppedBlocks)
            {
                foreach (var r in block.HitBox)
                {
                    spriteBatch.Draw(blockSprite, r, block.Color);
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawField()
        {
            spriteBatch.Draw(gameBackground, new Rectangle { X = 0, Y = 0, Width = graphics.PreferredBackBufferWidth, Height = graphics.PreferredBackBufferHeight }, Color.White);   
            spriteBatch.Draw(gameField, new Rectangle { X = space, Y = nextBlockSize + 2*space, Width = fieldWidth, Height = fieldHeight }, Color.White);
            spriteBatch.Draw(nextBlock, new Rectangle { X = graphics.PreferredBackBufferWidth- nextBlockSize-space, Y = space, Width = nextBlockSize, Height = nextBlockSize }, Color.White);
        }

        private void CheckForBoolResetSides()
        {
            if (!Keyboard.GetState().IsKeyDown(Keys.A))
                moveLeft = true;
            if (!Keyboard.GetState().IsKeyDown(Keys.D))
                moveRight = true;
            if (!Keyboard.GetState().IsKeyDown(Keys.S))
                moveDown = true;
        }

        private void CheckIfBlockIsDown()
        {
            if (activeBlock.CheckIfIsDown(graphics.PreferredBackBufferHeight))
            {
                BlockModel droppedBlock = activeBlock;
                droppedBlocks.Add(activeBlock);
                activeBlock = null;
            }
        }
    }
}

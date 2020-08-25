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
            Texture2D squareBlock = Content.Load<Texture2D>("squareBlock-sprite");
            Texture2D longBlock = Content.Load<Texture2D>("longBlock-sprite");
            Texture2D tShape = Content.Load<Texture2D>("TShape-sprite");
            Texture2D leftLShape = Content.Load<Texture2D>("leftLShape-sprite");
            Texture2D rightLShape = Content.Load<Texture2D>("rightLShape-sprite");
            Texture2D leftZShape = Content.Load<Texture2D>("leftZShape-sprite");
            Texture2D rightZShape = Content.Load<Texture2D>("rightZShape-sprite");
            gameFont = Content.Load<SpriteFont>("game-font");
            blockManager = new BlockManager(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, startingY, squareBlock, longBlock, tShape, leftLShape, rightLShape, leftZShape, rightZShape);
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
            if(activeBlock != null)
            spriteBatch.Draw(activeBlock.Sprite, new Vector2(activeBlock.X, activeBlock.Y), Color.White);
            foreach (BlockModel block in droppedBlocks)
            {
                spriteBatch.Draw(block.Sprite, new Vector2(block.X, block.Y), Color.White);
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
            if (activeBlock.Y + activeBlock.Height == graphics.PreferredBackBufferHeight - 25)
            {
                BlockModel droppedBlock = activeBlock;
                droppedBlocks.Add(activeBlock);
                activeBlock = null;
            }
        }
    }
}

using Gameproject.Model;
using Gameproject.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;


namespace Gameproject.Controller
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Applicationcontroller : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Camera camera = new Camera();
        private Menucontroller menucontroller;
        private Gamecontroller gamecontroller;
        private GameTime gametime;
        private bool hasclickedplay;

        private BallSimulation ballsim;
        private Playersimulation playersim;
        private Startview startview;
        private Drawmap drawmap;
        private LevelOne lvlone;
        private int[,] map;
        private List<Rectangle> Ballcollisions = new List<Rectangle>();
        private List<Vector4> convertedballcollison;
        private List<Rectangle> Playercollision = new List<Rectangle>();
        private List<Rectangle> newTiles = new List<Rectangle>();
        private List<Vector4> convertednewTiles;
        private List<Vector2> playercreatedtiles = new List<Vector2>();

        enum Gamestate
        {
            MainMenu,
            Playing,
        }

        Gamestate CurrentGameState = Gamestate.MainMenu;

        private List<Texture2D> textures = new List<Texture2D>();

        public Applicationcontroller()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight =800;
            graphics.PreferredBackBufferWidth = 800;
            graphics.IsFullScreen = false;
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here            
            camera.scalegame(graphics);
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            camera.SetFieldSize(graphics.GraphicsDevice.Viewport);

            spriteBatch = new SpriteBatch(GraphicsDevice);

            menucontroller = new Menucontroller(graphics);
            gamecontroller = new Gamecontroller(graphics);

            menucontroller.LoadContent(spriteBatch,Content,camera);

            gamecontroller.LoadContent(spriteBatch, Content, camera);
            
            //ballsim = new BallSimulation();
            //playersim = new Playersimulation();
            
            //drawmap = new Drawmap();
            //lvlone = new LevelOne();
       
            //startview = new Startview(Content, camera, spriteBatch, ballsim, playersim,drawmap, graphics);

            ////Loads the map once when the application starts. Will use update function to call a function in drawmap that allows me to place new tiles..
            //map = lvlone.getmap();
            //textures = startview.ReturnedTextures();
            //drawmap.Drawlevel(map, textures, spriteBatch, camera);
            
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            switch(CurrentGameState)
            {
                case Gamestate.MainMenu:
                    hasclickedplay = menucontroller.Update();

                    if(hasclickedplay == true)
                    {
                        CurrentGameState = Gamestate.Playing;
                    }
                    break;

                case Gamestate.Playing:
                    gamecontroller.Update(gameTime);
                    if(Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        CurrentGameState = Gamestate.MainMenu;
                    }
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            switch(CurrentGameState)
            {
                case Gamestate.MainMenu:
                    menucontroller.Draw();
                    break;
                case Gamestate.Playing:
                    gamecontroller.Draw();
                    break;
            }
            base.Draw(gameTime);
        }
    }
}
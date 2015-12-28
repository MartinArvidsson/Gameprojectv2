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
        private BallSimulation ballsim;
        private Playersimulation playersim;
        private Startview startview;
        private Drawmap drawmap;
        private LevelOne lvlone;
        private int[,] map;
        private List<Rectangle> Ballcollisions = new List<Rectangle>();
        private List<Vector4> convertedballcollison;
        private List<Rectangle> Playercollision = new List<Rectangle>();
        private List<Vector4> convertedplayercollison;
        private List<Rectangle> newTiles = new List<Rectangle>();
        private List<Vector4> convertednewTiles;
        private List<Vector2> playercreatedtiles = new List<Vector2>();
        private List<Vector2> convertedplayercreatedtiles;


        private List<Texture2D> textures = new List<Texture2D>();

        public Applicationcontroller()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight =900;
            graphics.PreferredBackBufferWidth = 900;
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
            ballsim = new BallSimulation();
            playersim = new Playersimulation();
            drawmap = new Drawmap();
            lvlone = new LevelOne();

            startview = new Startview(Content, camera, spriteBatch, ballsim, playersim,drawmap, graphics);

            //Loads the map once when the application starts. Will use update function to call a function in drawmap that allows me to place new tiles..
            map = lvlone.getmap();
            textures = startview.ReturnedTextures();
            drawmap.Drawlevel(map, textures, spriteBatch, camera);
            
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
            var buttonclicked = Keyboard.GetState();

            if (buttonclicked.IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            
            //Ballupdating
            Ballcollisions = drawmap.Returnballcollisions();
            newTiles = drawmap.Returnplayercreatedtiles();
            convertedballcollison = new List<Vector4>();
            convertednewTiles = new List<Vector4>();

            foreach(Rectangle rect in Ballcollisions)
            {
                Vector2 convertedcoords = new Vector2(rect.X, rect.Y);
                Vector2 convertedsize = new Vector2(rect.Width, rect.Height);
                convertedcoords = camera.convertologicalcoords(convertedcoords);
                convertedsize = camera.convertologicalcoords(convertedsize);

                convertedballcollison.Add(new Vector4(convertedcoords.X, convertedcoords.Y, convertedsize.X, convertedsize.Y));
            }

            foreach (Rectangle rect in newTiles)
            {
                Vector2 convertedcoords = new Vector2(rect.X, rect.Y);
                Vector2 convertedsize = new Vector2(rect.Width, rect.Height);
                convertedcoords = camera.convertologicalcoords(convertedcoords);
                convertedsize = camera.convertologicalcoords(convertedsize);

                convertednewTiles.Add(new Vector4(convertedcoords.X, convertedcoords.Y, convertedsize.X, convertedsize.Y));
            }

            ballsim.UpdateBall((float)gameTime.ElapsedGameTime.TotalSeconds, convertedballcollison, convertednewTiles);

            //Playerupdating
            Playercollision = drawmap.Returnplayercollisions();
            convertedplayercollison = new List<Vector4>();
            playersim.SetBool(drawmap.Returnfinishedcreating());
            foreach (Rectangle rect in Playercollision)
            {
                Vector2 _convertedcoords = new Vector2(rect.X, rect.Y);
                Vector2 _convertedsize = new Vector2(rect.Width, rect.Height);
                _convertedcoords = camera.convertologicalcoords(_convertedcoords);
                _convertedsize = camera.convertologicalcoords(_convertedsize);

                convertedplayercollison.Add(new Vector4(_convertedcoords.X, _convertedcoords.Y, _convertedsize.X, _convertedsize.Y));
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.Down) ||
                Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                //Playermovements
                playersim.UpdatePlayer(buttonclicked, convertedplayercollison, convertedballcollison);
            }

            //Mapupdating
            playercreatedtiles = playersim.getplayercreatedtiles();
            convertedplayercreatedtiles = new List<Vector2>();
            foreach (Vector2 vector in playercreatedtiles)
            {
                convertedplayercreatedtiles.Add(camera.Converttovisualcoords(vector));
            }
            drawmap.updatedtilestoadd(convertedplayercreatedtiles);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            startview.Draw();
            base.Draw(gameTime);
        }
    }
}
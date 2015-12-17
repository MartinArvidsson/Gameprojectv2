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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Camera camera = new Camera();
        BallSimulation ballsim;
        private Startview startview;
        private Drawmap drawmap = new Drawmap();
        LevelOne lvlone = new LevelOne();
        private int[,] map;
        private List<Rectangle> Ballcollisions = new List<Rectangle>();
        private List<Vector4> convertedcollison = new List<Vector4>();
        private List<Texture2D> textures = new List<Texture2D>();

        public Applicationcontroller()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight =600;
            graphics.PreferredBackBufferWidth = 600;
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
            startview = new Startview(Content, camera, spriteBatch, ballsim, graphics);

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
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            if(Keyboard.GetState().IsKeyDown(Keys.R))
            {
                startview = new Startview(Content, camera, spriteBatch, ballsim, graphics);
            }
            
            
            Ballcollisions = drawmap.ReturnCollisonlist();
            convertedcollison = new List<Vector4>();
            foreach(Rectangle rect in Ballcollisions)
            {
                Vector2 convertedcoords = new Vector2(rect.X, rect.Y);
                Vector2 convertedsize = new Vector2(rect.Width, rect.Height);
                convertedcoords = camera.convertologicalcoords(convertedcoords);
                convertedsize = camera.convertologicalcoords(convertedsize);

                convertedcollison.Add(new Vector4(convertedcoords.X, convertedcoords.Y, convertedsize.X, convertedsize.Y));
            }
            ballsim.UpdateBall((float)gameTime.ElapsedGameTime.TotalSeconds, convertedcollison);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            startview.Draw();

            base.Draw(gameTime);
        }
    }
}
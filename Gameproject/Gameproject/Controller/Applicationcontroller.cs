using Gameproject.Model;
using Gameproject.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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
        private Song song;
        private bool hasclickedplay,hasclickedtryagain,hasclickednextlevel,doesuserwanttoexit,exittomenu;
        private int currentcase = 1;
        private int currentlevel = 1;
        enum Gamestate
        {
            MainMenu,
            Newgame,
            Playing,
            PlayerLost,
            PlayerWon,
        }

        Gamestate CurrentGameState = Gamestate.MainMenu;

        //private List<Texture2D> textures = new List<Texture2D>();

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

            gamecontroller.LoadContent(spriteBatch, Content, camera, currentlevel);

            song = Content.Load<Song>("KillingTime");

            MediaPlayer.Play(song);
            MediaPlayer.Volume = 0.3f;
            MediaPlayer.IsRepeating = true;
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
                    currentcase = 1;
                    hasclickedplay = menucontroller.Update(currentcase);
                    doesuserwanttoexit = menucontroller.exitgame();

                    if (doesuserwanttoexit == true)
                    {
                        Exit();
                    }

                    if(hasclickedplay == true)
                    {
                        CurrentGameState = Gamestate.Playing;
                    }
                    break;

                case Gamestate.Playing:
                    gamecontroller.Update(gameTime);
                    //Pause
                    if(Keyboard.GetState().IsKeyDown(Keys.P))
                    {
                        CurrentGameState = Gamestate.MainMenu;
                    }
                    if(gamecontroller.playerwon() == true)
                    {
                        currentlevel = gamecontroller.lvlchooser();
                        System.Console.WriteLine(currentlevel);
                        CurrentGameState = Gamestate.PlayerWon;
                    }
                    if(gamecontroller.playerdied() == true)
                    {
                        CurrentGameState = Gamestate.PlayerLost;
                    }
                    break;
                case Gamestate.PlayerWon:
                    currentcase = 2;
                    hasclickednextlevel = menucontroller.Update(currentcase);
                    exittomenu = menucontroller.exittomenu();

                    if(exittomenu == true)
                    {
                        CurrentGameState = Gamestate.MainMenu;
                    }

                    if (hasclickednextlevel == true)
                    {
                        CurrentGameState = Gamestate.Newgame;
                        //Ny bana NYI;
                    }
                    break;

                case Gamestate.PlayerLost:
                    currentcase = 3;
                    hasclickedtryagain = menucontroller.Update(currentcase);
                    if (hasclickedtryagain == true)
                    {
                        CurrentGameState = Gamestate.Newgame;
                        //Ny bana NYI;
                    }
                    break;
                case Gamestate.Newgame:
                    gamecontroller = new Gamecontroller(graphics);
                    gamecontroller.LoadContent(spriteBatch, Content, camera,currentlevel);
                    CurrentGameState = Gamestate.Playing;
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
                    menucontroller.Draw(currentcase);
                    break;
                case Gamestate.Playing:
                    gamecontroller.Draw();
                    break;
                case Gamestate.PlayerWon:
                    menucontroller.Draw(currentcase);
                    break;
                case Gamestate.PlayerLost:
                    menucontroller.Draw(currentcase);
                    break;
            }
            base.Draw(gameTime);
        }
    }
}
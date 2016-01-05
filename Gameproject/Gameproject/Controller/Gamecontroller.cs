using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gameproject.View;
using Gameproject.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
namespace Gameproject.Controller
{
    class Gamecontroller
    {
        private GraphicsDeviceManager graphics;
        private ContentManager Content;
        private SpriteBatch spriteBatch;
        private Camera camera = new Camera();
        private BallSimulation ballsim;
        private Playersimulation playersim;
        private Startview startview;
        private Drawmap drawmap;
        private LevelOne lvlone;
        private LevelTwo lvltwo;
        private LevelThree lvlthree;
        private int[,] map;
        private List<Rectangle> Ballcollisions = new List<Rectangle>();
        private List<Vector4> convertedballcollison;
        private List<Rectangle> Playercollision = new List<Rectangle>();
        private List<Rectangle> newTiles = new List<Rectangle>();
        private List<Rectangle> deathtiles = new List<Rectangle>();
        private List<Vector4> convertednewTiles;
        private List<Vector4> converteddeathtiles;
        private List<Vector2> playercreatedtiles = new List<Vector2>();
        private List<Texture2D> textures = new List<Texture2D>();

        private bool playerhasdied = false;
        private bool playerhaswon = false;
        private int newlevel,ballstoadd,timer;
        public Gamecontroller(GraphicsDeviceManager _graphics)
        {
            graphics = _graphics;
        }

        public void LoadContent(SpriteBatch _spritebatch,ContentManager _content,Camera _camera,int _newlevel)
        {
            spriteBatch = _spritebatch;
            Content = _content;
            camera = _camera;
            newlevel = _newlevel;
            playersim = new Playersimulation();
            drawmap = new Drawmap();

            //Loads the map once when the application starts. Will use update function to call a function in drawmap that allows me to place new tiles..
            if(newlevel == 1)
            {
                lvlone = new LevelOne();
                map = lvlone.getmap();
                ballstoadd = 2;
            }
            if(newlevel == 2)
            {
                lvltwo = new LevelTwo();
                map = lvltwo.getmap();
                timer = 60;
                ballstoadd = 4;
            }
            if(newlevel == 3)
            {
                lvlthree = new LevelThree();
                map = lvlthree.getmap();
                timer = 120;
                //Add special tile on map 3 that instakills the player if he touches it, it doesn't count as a playertile so it's like the map is missing one block.
                //Harder to navigate
                ballstoadd = 5;
            }

            ballsim = new BallSimulation(ballstoadd);

            startview = new Startview(Content, camera, spriteBatch, ballsim, playersim, drawmap, graphics,map,timer);
            textures = startview.ReturnedTextures();
            drawmap.Drawlevel(map, textures, spriteBatch, camera);
        }

        public void Update(GameTime gameTime)
        {
            var buttonclicked = Keyboard.GetState();

            if (playersim.isgameover == true || startview.Returntime() == true)
            {
                playerhasdied = true;
            }

            if (drawmap.playerdidwin == true)
            {
                playerhaswon = true;
                newlevel += 1;
            }

            //Ballupdating
            Ballcollisions = drawmap.Returnballcollisions();
            newTiles = drawmap.Returnplayercreatedtiles();
            deathtiles = drawmap.ReturnDeathtiles();
            convertedballcollison = new List<Vector4>();
            convertednewTiles = new List<Vector4>();
            converteddeathtiles = new List<Vector4>();

            foreach (Rectangle rect in deathtiles)
            {
                Vector2 convertedcoords = new Vector2(rect.X, rect.Y);
                Vector2 convertedsize = new Vector2(rect.Width, rect.Height);
                convertedcoords = camera.convertologicalcoords(convertedcoords);
                convertedsize = camera.convertologicalcoords(convertedsize);

                converteddeathtiles.Add(new Vector4(convertedcoords.X, convertedcoords.Y, convertedsize.X, convertedsize.Y));
            }

            foreach (Rectangle rect in Ballcollisions)
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

            ballsim.UpdateBall((float)gameTime.ElapsedGameTime.TotalSeconds, convertedballcollison, convertednewTiles,converteddeathtiles,startview);

            //Playerupdating
            Playercollision = drawmap.Returnplayercollisions();
            playersim.SetBool(drawmap.Returnfinishedcreating());
            playersim.setInt(ballsim.playerbeenhit);
            playersim.setcollisions(Playercollision, Ballcollisions,deathtiles);
            playersim.hitwall(camera);

            if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.Down) ||
                Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                //Playermovements
                playersim.UpdatePlayer(buttonclicked);
            }

            //Mapupdating
            drawmap.updatedtilestoadd(playersim.getplayercreatedtiles());


        }

        public void Draw(GameTime gameTime)
        {
            startview.Draw((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public bool playerdied()
        {
            return playerhasdied;
        }

        public bool playerwon()
        {
            return playerhaswon;
        }

        public int lvlchooser()
        {
            return newlevel;
        }
    }
}

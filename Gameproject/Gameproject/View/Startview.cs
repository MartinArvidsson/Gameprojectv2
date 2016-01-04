using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Gameproject.Model;
using Gameproject.Controller;

namespace Gameproject.View
{
    class Startview : CollisionObserver
    {

        private ContentManager content;
        private Camera camera;
        private SpriteBatch spritebatch;
        private BallSimulation ballsim;
        private Playersimulation playersim;
        private GraphicsDeviceManager graphics;

        private Drawballs drawballs = new Drawballs();
        private Drawmap drawmap;
        private DrawPlayer drawplayer = new DrawPlayer();
        private Player player;
        private SplitterSystem splittersystem;

        private LevelOne lvlone = new LevelOne();
        private LevelTwo lvltwo = new LevelTwo();
        private LevelThree lvlthree = new LevelThree();

        private Texture2D balltexture, 
            ballbackgroundtexture,
            playerbackgroundtexture, 
            playercreatestexture, 
            playersprite,
            playertookdamage;

        private Vector2 playercenter, 
            ballcenter;
        private int[,] map;

        private List<Texture2D> maptextures = new List<Texture2D>();
        private List<Ball> totalballs = new List<Ball>();

        private SoundEffect ballhitwall, ballhitplayerwall,playerfinishedtiles;

        public Startview(ContentManager _content, Camera _camera, SpriteBatch _spritebatch, BallSimulation _ballsim,Playersimulation _playersim,Drawmap _drawmap, GraphicsDeviceManager _graphics,int[,] _map)
        {
            content = _content; //Camera etc..
            camera = _camera;
            spritebatch = _spritebatch;
            ballsim = _ballsim;
            playersim = _playersim;
            drawmap = _drawmap;
            graphics = _graphics;
            map = _map;

            balltexture = content.Load<Texture2D>("BALL"); //Balltexture
            ballcenter = new Vector2(balltexture.Width / 2, balltexture.Height / 2); //Ballcenter

            playersprite = content.Load<Texture2D>("Player");
            playercenter = new Vector2(playersprite.Width / 2, playersprite.Height / 2);

            ballbackgroundtexture = content.Load<Texture2D>("Ballground"); //Sprites
            playerbackgroundtexture = content.Load<Texture2D>("Playergroundtile");
            playercreatestexture = content.Load<Texture2D>("Playercreatingground");
            ballhitwall = content.Load<SoundEffect>("Ballwallsound");
            ballhitplayerwall = content.Load<SoundEffect>("Ballplayerwallsound");
            playerfinishedtiles = content.Load<SoundEffect>("Playerplacedtiles");
            playertookdamage = content.Load<Texture2D>("Playerdamaged");

            maptextures.Add(ballbackgroundtexture); //Sprites added to list
            maptextures.Add(playerbackgroundtexture);
            maptextures.Add(playercreatestexture);
            totalballs = ballsim.getballs(); //Gets the amount of balls
            player = playersim.getplayer();
        }

        public void Draw(float elapsedtime)
        {
            if(playersim.returnplayerhits() == 1)
            {
                playersprite = content.Load<Texture2D>("Playerdmg1");
            }
            if (playersim.returnplayerhits() == 2)
            {
                playersprite = content.Load<Texture2D>("Playerdmg2");
            }
            
            drawmap.Drawlevel(map, maptextures, spritebatch, camera); //Draws the map
            drawmap.Updatelevel(drawmap.Returnplayertilestoadd());
            if(drawmap.Returnfinishedcreating())
            {
                //playerfinishedtiles.Play(0.1f,1,0);
            }
            drawballs.drawallballs(totalballs, camera, balltexture, spritebatch, ballcenter); //Draws the balls
            drawplayer.drawplayer(player, camera, playersprite, spritebatch, playercenter); //Draws the player
            if(splittersystem != null)
            {
                splittersystem.Draw();
                splittersystem.Update(elapsedtime);
            }
        }

        public List<Texture2D> ReturnedTextures()
        {
            return maptextures;
        }

        public void hitregularwall()
        {
            Random _rand = new Random();
            float pitch = _rand.Next(0, 2);
            float pan = _rand.Next(0, 2);
            //ballhitwall.Play(0.1f, pitch, pan);
        }

        public void hitplayerwall()
        {
            Random _rand = new Random();
            float pitch = _rand.Next(0, 2);
            float pan = _rand.Next(0, 2);
            //ballhitplayerwall.Play(0.1f, pitch, pan);
        }

        public void playerdamaged()
        {
            splittersystem = new SplitterSystem(playertookdamage, spritebatch, camera, 0.5f, player.getplayerpos);
        }
    }
}

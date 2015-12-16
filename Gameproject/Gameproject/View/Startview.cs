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
    class Startview
    {

        private ContentManager content;
        private Camera camera;
        private SpriteBatch spritebatch;
        private BallSimulation ballsim;
        private GraphicsDeviceManager graphics;

        private Drawballs drawballs = new Drawballs();
        private Drawmap drawmap = new Drawmap();
        private LevelOne lvlone = new LevelOne();

        private Texture2D balltexture;
        private Texture2D ballbackgroundtexture;
        private Texture2D playerbackgroundtexture;
        private Texture2D playercreatestexture;
        private Vector2 ballcenter;

        private int[,] map;

        private List<Texture2D> maptextures = new List<Texture2D>();
        private List<Ball> totalballs = new List<Ball>();

        public Startview(ContentManager _content, Camera _camera, SpriteBatch _spritebatch, BallSimulation _ballsim, GraphicsDeviceManager _graphics)
        {
            content = _content;
            camera = _camera;
            spritebatch = _spritebatch;
            ballsim = _ballsim;
            graphics = _graphics;

            balltexture = content.Load<Texture2D>("BALL");
            ballcenter = new Vector2(balltexture.Width / 2, balltexture.Height / 2);

            ballbackgroundtexture = content.Load<Texture2D>("Ballground");
            playerbackgroundtexture = content.Load<Texture2D>("Playergroundtile");
            playercreatestexture = content.Load<Texture2D>("Playercreatingground");

            maptextures.Add(ballbackgroundtexture);
            maptextures.Add(playerbackgroundtexture);
            maptextures.Add(playercreatestexture);

            map = lvlone.getmap();
            totalballs = ballsim.getballs();

        }

        public void Draw(float elapsedtime)
        {
            spritebatch.Begin(SpriteSortMode.FrontToBack);
            drawmap.Drawlevel(map,maptextures,spritebatch, camera);
            drawballs.drawallballs(totalballs, camera, balltexture, spritebatch, ballcenter);
            spritebatch.End();
        }
    }
}

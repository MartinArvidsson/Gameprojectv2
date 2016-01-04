using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gameproject.View
{
    class SplitterSystem
    {
        private float lifetime = 2f;
        private float timelived = 0;
        private List<SplitterParticle> particles = new List<SplitterParticle>();
        private int maxparticles = 15;
        private Random rand = new Random();
        private SpriteBatch spritebatch;
        private Camera camera;
        private float scale;
        private Vector2 startpos = new Vector2();

        public SplitterSystem(Texture2D spark,SpriteBatch _spritebatch,Camera _camera,float _scale,Vector2 _startlocation)
        {
            spritebatch = _spritebatch;
            camera = _camera;
            scale = _scale;
            startpos = _startlocation;
            while(particles.Count < maxparticles)
            {
                particles.Add(new SplitterParticle(spark, rand, spritebatch, camera, scale, startpos, lifetime));
            }
        }

        public void Draw()
        {
            spritebatch.Begin();
            foreach(SplitterParticle particle in particles)
            {
                particle.Draw();
            }
            spritebatch.End();
        }

        public void Update(float timeElapsed)
        {
            timelived += timeElapsed;
            if(timelived > lifetime)
            {
                particles.Clear();
            }
            foreach (SplitterParticle particle in particles)
            {
                //Hitwall(particle);
                particle.UpdatePos(timeElapsed);
            }
        }

        public void Hitwall(SplitterParticle particle)
        {
            if (particle.startpos.X + particle.particleradius > 1.0 || particle.startpos.X + particle.particleradius < 0.0) //If ball bounces <---->
            {
                particle.setparticleVelocityX();
            }
            if (particle.startpos.Y + particle.particleradius > 1.0 || particle.startpos.Y + particle.particleradius < 0.0) //If ball bounces ^ v
            {
                particle.setparticleVelocityY();
            }
        }
    }
}

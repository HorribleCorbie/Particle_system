using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static Particle_system.Particle;

namespace Particle_system
{
    public class TopEmitter : Emitter
    {
        public int Width;
        public int Height;
        public override Particle CreateParticle()
        {
            var particle = new EvilParticle();
            return particle;
        }
        public override void UpdateState()
        {
            var target = impactPoints.FirstOrDefault() as Player;

            int particlesToCreate = ParticlesPerTick;

            foreach (var particle in particles.ToList())
            {
                particle.X += particle.SpeedX;
                particle.Y += particle.SpeedY;

                if (particle.Y > Height)
                {
                    if (particles.Count > ParticlesCount)
                    {
                        particles.Remove(particle);
                    }
                    ResetParticle(particle);
                    continue;
                }

                foreach (var point in impactPoints.ToList())
                {
                    point.ImpactParticle(particle);
                }

                if (particle.Life <= 0)
                {
                    if (particles.Count > ParticlesCount)
                    {
                        particles.Remove(particle);
                    }
                    ResetParticle(particle);
                    continue;
                }

               

                float dx = target.X - particle.X;
                float dy = target.Y - particle.Y;

                float length = MathF.Sqrt(dx * dx + dy * dy);

                if (length > 0)
                {
                    dx /= length;
                    dy /= length;
                }

                float speed = 4f; 

                particle.SpeedX = dx * speed;
                particle.SpeedY = dy * speed;
            }

            while (particles.Count < ParticlesCount && particlesToCreate-- > 0)
            {
                var particle = CreateParticle();
                ResetParticle(particle);
                particles.Add(particle);
            }
           

        }
        public override void ResetParticle(Particle particle)
        {
            var target = impactPoints.FirstOrDefault() as Player;

            particle.X = Particle.rand.Next(Width);
            particle.Y = -10;

            float dx = target.X - particle.X;
            float dy = target.Y - particle.Y;

            float length = MathF.Sqrt(dx * dx + dy * dy);

            if (length > 0)
            {
                dx /= length;
                dy /= length;
            }

            float speed = 4f; 

            particle.SpeedX = dx * speed;
            particle.SpeedY = dy * speed;

            particle.Life = 1;
        }
    }
}

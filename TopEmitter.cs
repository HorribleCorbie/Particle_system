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

                MovementParticle(particle);
            }

            while (particles.Count < ParticlesCount && particlesToCreate-- > 0)
            {
                var particle = CreateParticle();
                ResetParticle(particle);
                particles.Add(particle);
            }
           

        }
        
        public void MovementParticle(Particle particle)
        {
            var target = impactPoints.FirstOrDefault() as Player;

            float dx = target.X - particle.X;
            float dy = target.Y - particle.Y;

            float length = MathF.Sqrt(dx * dx + dy * dy);

            if (length > 0)
            {
                dx /= length;
                dy /= length;
            }
            var monster = particle as EvilParticle;
            float speed = monster.speed;

            particle.SpeedX = dx * speed;
            particle.SpeedY = dy * speed;
        }

        public override void ResetParticle(Particle particle)
        {

            particle.X = Particle.rand.Next(Width);
            particle.Y = -10;

            MovementParticle(particle);
            
            particle.Life = (particle as EvilParticle).DefaultHP;
        }
    }
}

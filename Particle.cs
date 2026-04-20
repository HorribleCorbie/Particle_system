using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Particle_system
{
    public class Particle
    {
        public float Life;

        public int Radius;
        public float X;
        public float Y;

        public float SpeedX;
        public float SpeedY;

        public static Random rand = new Random();

        public Particle()
        {
            var direction = (double)rand.Next(360);
            var speed = 1 + rand.Next(10);

            SpeedX = (float)(Math.Cos(direction / 180 * Math.PI) * speed);
            SpeedY = -(float)(Math.Sin(direction / 180 * Math.PI) * speed);

            Radius = 2 + rand.Next(10);
            Life = 100 + rand.Next(100);
        }


        public virtual void Draw(Graphics g)
        {
            float k = Math.Min(1f, Life / 100f);
            int alpha = (int)(k * 255);

            var color = Color.FromArgb(alpha, Color.Black);
            var b = new SolidBrush(color);

            g.FillEllipse(b, X - Radius, Y - Radius, Radius * 2, Radius * 2);

            b.Dispose();
        }

        public class ParticleColorful : Particle
        {
            public Color FromColor;
            public Color ToColor;

            public static Color MixColor(Color color1, Color color2, float k)
            {
                k = Math.Clamp(k, 0f, 1f);
                return Color.FromArgb(
                    (int)(color2.A * k + color1.A * (1 - k)),
                    (int)(color2.R * k + color1.R * (1 - k)),
                    (int)(color2.G * k + color1.G * (1 - k)),
                    (int)(color2.B * k + color1.B * (1 - k))
                );
            }

            public override void Draw(Graphics g)
            {
                float k = Math.Min(1f, Life / 100);

                var color = MixColor(FromColor, ToColor, k);
                var b = new SolidBrush(color);

                g.FillEllipse(b, X - Radius, Y - Radius, Radius * 2, Radius * 2);

                b.Dispose();
            }
        }
        public Matrix GetTransform()
        {
            var matrix = new Matrix();
            matrix.Translate(X, Y);
            return matrix;
        }

        public virtual bool Overlaps(Particle other)
        {
            float dx = X - other.X;
            float dy = Y - other.Y;

            float dist2 = dx * dx + dy * dy;

            float r = Radius + other.Radius;

            return dist2 <= r * r;
        }
    }
}

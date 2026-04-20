using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Particle_system
{
    internal class EvilParticle : Particle
    {
        public Image Img = Image.FromFile("sceleton.png");
        public EvilParticle()
        {
            var direction = (double)rand.Next(360);
            var speed = 1;

            SpeedX = (float)(Math.Cos(direction / 180 * Math.PI) * speed);
            SpeedY = -(float)(Math.Sin(direction / 180 * Math.PI) * speed);

            Radius = 10 + rand.Next(10);
            Life = 1;
        }
        public override void Draw(Graphics g)
        {

            g.DrawImage(
                Img,
                X - Radius,
                Y - Radius,
                Radius * 2,
                Radius * 2
            );

        }

    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Particle_system.Particle;

namespace Particle_system
{
    internal class Tower 
    {
        public float X;
        public float Y;
        public int Radius = 80;
        public Image Img = Image.FromFile("tower.png");

        public void Render(Graphics g)
        {
            g.DrawImage(
                   Img,
                   X - Radius / 2,
                   Y - Radius / 2,
                   Radius,
                   Radius
               );

        }
    }
}

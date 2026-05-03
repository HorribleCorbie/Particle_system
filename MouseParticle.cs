using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Particle_system
{
    internal class MouseParticle 
    {
        public int Radius;
        public Action<MouseParticle> timerIsOver;
        public Color color = Color.Red;
        public float X;
        public float Y;
        public float timer = 50;
        public bool isReady=false;

        public MouseParticle()
        {
            Radius = 4;
        }

        public void ResetParticle()
        {
            color = Color.Red;
            timer = 50;
            isReady = false;
        }

        public void Draw(Graphics g)
        {

            g.FillEllipse(
                new SolidBrush(color),
                X - Radius,
                Y - Radius,
                Radius * 2,
                Radius * 2
            );

        }
    }
}

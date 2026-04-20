using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Particle_system
{
    internal class Player : IImpactPoint
    {
        public Action<Particle> isOverlaps;
        public int Health = 1000;
        public float X;
        public float Y;
        public float radius =25;
        public Image Img = Image.FromFile("maintower.png");

        public virtual void Render(Graphics g)
        {
            g.DrawImage(
                Img,
                X - radius,
                Y - radius,
                radius*2,
                radius * 2
            );

            var stringFormat = new StringFormat(); 
            stringFormat.Alignment = StringAlignment.Center; 
            stringFormat.LineAlignment = StringAlignment.Center; 

            var text = $"Здоровье: {Health}";
            var font = new Font("Verdana", 10);

            var size = g.MeasureString(text, font);

            g.FillRectangle(
                new SolidBrush(Color.Red),
                X - size.Width / 2,
                Y + radius * 1.5f - size.Height / 2,
                size.Width,
                size.Height
            );


            g.DrawString(
                text,
                new Font("Verdana", 10),
                new SolidBrush(Color.White),
                X,
                Y+radius * 1.5f,
                stringFormat 
            );
        }
        public override void ImpactParticle(Particle particle)
        {
            float dx = X - particle.X;
            float dy = Y - particle.Y;
            float dist2 = dx * dx + dy * dy;

            if (dist2 <= radius * radius)
            {
                Health -= 1;
                particle.Life = 0;
            }
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Particle_system
{
    public class GravityPoint : IImpactPoint
    {
        public int Power = 100;

        public override void ImpactParticle(Particle particle)
        {
            float gX = X - particle.X;
            float gY = Y - particle.Y;

            double r = Math.Sqrt(gX * gX + gY * gY);
            if (r + particle.Radius < Power / 2)
            {
                float r2 = (float)Math.Max(100, gX * gX + gY * gY);
                particle.SpeedX += gX * Power / r2;
                particle.SpeedY += gY * Power / r2;
            }
        }

        public override void Render(Graphics g)
        {
            g.DrawEllipse(
                   new Pen(Color.Red),
                   X - Power / 2,
                   Y - Power / 2,
                   Power,
                   Power
               );

            //var stringFormat = new StringFormat(); // создаем экземпляр класса
            //stringFormat.Alignment = StringAlignment.Center; // выравнивание по горизонтали
            //stringFormat.LineAlignment = StringAlignment.Center; // выравнивание по вертикали

            //var text = $"Я гравитон\nc силой {Power}";
            //var font = new Font("Verdana", 10);

            //// вызываем MeasureString, чтобы померить размеры текста
            //var size = g.MeasureString(text, font);

            //// рисуем подложнку под текст
            //g.FillRectangle(
            //    new SolidBrush(Color.Red),
            //    X - size.Width / 2, // так как я выравнивал текст по центру то подложка должна быть центрирована относительно X,Y
            //    Y - size.Height / 2,
            //    size.Width,
            //    size.Height
            //);


            //g.DrawString(
            //    $"Я гравитон\nc силой {Power}",
            //    new Font("Verdana", 10),
            //    new SolidBrush(Color.White),
            //    X,
            //    Y,
            //    stringFormat // передаем инфу о выравнивании
            //);

        }
    }
}

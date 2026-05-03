using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Particle_system
{
    internal class TowerDefense : Bullet
    {
        public float Angle;
        public float AngularSpeed;
        public float CenterX;
        public float CenterY;
        public float MainRadius;

        public override void Draw(Graphics g)
        {
            g.FillEllipse(
                new SolidBrush(Color.Blue),
                X - Radius,
                Y - Radius,
                Radius * 2,
                Radius * 2
                );

        }

        public override void Update()
        {
            Angle += AngularSpeed;

            X = CenterX + MainRadius * MathF.Cos(Angle);
            Y = CenterY + MainRadius * MathF.Sin(Angle);
        }
    }
}

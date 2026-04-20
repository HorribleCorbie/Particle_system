using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Particle_system
{
    internal class EvilParticle : Particle
    {
        enum EnemyType { Monster, Skeleton, Zombie}
        public Image Img;
        public EvilParticle()
        {
            var direction = (double)rand.Next(360);
            var speed = 1;

            SpeedX = (float)(Math.Cos(direction / 180 * Math.PI) * speed);
            SpeedY = -(float)(Math.Sin(direction / 180 * Math.PI) * speed);

            Radius = 20 + rand.Next(10);
            Life = 1;
            EnemyType type = (EnemyType)rand.Next(3);
            string str = type.ToString() switch
            {
                "Monster" => "monster.png",
                "Skeleton" => "skeleton.png",
                "Zombie" => "zombie.png",
                _ => "monster.png"
            };
            Img = Image.FromFile(str);
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

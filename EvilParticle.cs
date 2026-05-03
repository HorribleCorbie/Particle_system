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
        public float speed = 3.5f;
        public int DefaultHP;
        public int type = (int)rand.Next(3);

        public EvilParticle()
        {
            var direction = (double)rand.Next(360);
            string str;

            Radius = 20 + rand.Next(10);
            Life = 1;
            switch (type)
            {
                case 0:
                    str = "monster.png";
                    speed = 3f;
                    Life = 2;
                    break;
                case 1:
                    str = "skeleton.png";
                    break;
                case 2:
                    str = "zombie.png";
                    speed = 5f;
                    break;
                default:
                    str = "skeleton.png";
                    break;
            }
            DefaultHP = Life;
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

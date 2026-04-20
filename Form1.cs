using System.Numerics;
using static Particle_system.Particle;

namespace Particle_system
{
    public partial class Form1 : Form
    {
        List<Emitter> emitters = new List<Emitter>();
        Player player;
        Emitter emitter;
        GravityPoint point1;
        TopEmitter top;
        Bullet particle;
        int Points = 0;
        List<Bullet> bullets = new List<Bullet>();

        public Form1()
        {
            InitializeComponent();
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);
            picDisplay.BackgroundImage = Image.FromFile("background.jpg");
            picDisplay.BackgroundImageLayout = ImageLayout.Stretch;

            //this.emitter = new Emitter
            //{
            //    Direction = 0,
            //    Spreading = 10,
            //    SpeedMin = 10,
            //    SpeedMax = 10,
            //    ColorFrom = Color.Gold,
            //    ColorTo = Color.FromArgb(0, Color.Red),
            //    ParticlesPerTick = 10,
            //    X = picDisplay.Width / 2,
            //    Y = picDisplay.Height / 2,
            //};

            player = new Player
            {
                X = picDisplay.Width / 2,
                Y = picDisplay.Height - 100,
            };
            //emitters.Add(this.emitter);
            //point1 = new GravityPoint
            //{
            //    X = picDisplay.Width / 2 + 100,
            //    Y = picDisplay.Height / 2,
            //};
            //emitter.impactPoints.Add(point1);

            top = new TopEmitter
            {
                Width = picDisplay.Width,
                Height = picDisplay.Height,
                GravitationY = 0.25f
            };
            emitters.Add(this.top);
            top.impactPoints.Add(player);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (var emitter in emitters)
            {
                emitter.UpdateState();
            }

            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.Transparent);

                foreach (var emitter in emitters)
                {
                    player.Render(g);
                    emitter.Render(g);
                }
                foreach (var bullet in bullets.ToList())
                {
                    bullet.Draw(g);
                    bullet.X += bullet.SpeedX;
                    bullet.Y += bullet.SpeedY;
                    foreach (var p in top.particles.ToList())
                    {
                        if (p.Overlaps(bullet))
                        {
                            bullet.isOverlaps(p, bullet);
                        }
                    }
                }
            }

            picDisplay.Invalidate();
        }
        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            foreach (var emitter in emitters)
            {
                emitter.MousePositionX = e.X;
                emitter.MousePositionY = e.Y;
            }

            //point1.X = e.X;
            //point1.Y = e.Y;
        }


        private void tbDirection_Scroll(object sender, EventArgs e)
        {
            top.ParticlesCount = tbDirection.Value;
            lblDirection.Text = $"{tbDirection.Value}";
        }


        private void tbGravition_Scroll(object sender, EventArgs e)
        {
            //point1.Power = tbGraviton.Value;
        }

        private void tbGraviton2_Scroll(object sender, EventArgs e)
        {
            //point2.Power = tbGraviton2.Value;
        }

        private void picDisplay_MouseClick(object sender, MouseEventArgs e)
        {
            particle = new Bullet();
            var X = e.X;
            var Y = e.Y;
            particle.X = player.X;
            particle.Y = player.Y;
            float dx = X - particle.X;
            float dy = Y - particle.Y;

            float length = MathF.Sqrt(dx * dx + dy * dy);

            if (length > 0)
            {
                dx /= length;
                dy /= length;
            }

            float speed = 7f;

            particle.SpeedX = dx * speed;
            particle.SpeedY = dy * speed;
            particle.isOverlaps += (p, b) =>
            {
                ++Points;
                points.Text = $"—чет: {Points}";
                p.Life = 0;
                b.Life = 0;
                bullets.Remove(b);
            };
            bullets.Add(particle);
        }

        private void btnTower_Click(object sender, EventArgs e)
        {
            if (Points == 50)
            {

            }
        }
    }
}

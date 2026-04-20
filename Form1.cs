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
        int TowerX;
        List<Bullet> bullets = new List<Bullet>();
        List<Tower> towers = new List<Tower>();

        public float SpeedBullets = 5f;

        public Form1()
        {
            InitializeComponent();
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);
            picDisplay.BackgroundImage = Image.FromFile("background.jpg");
            picDisplay.BackgroundImageLayout = ImageLayout.Stretch;
            TowerX = picDisplay.Width - 100;
            player = new Player
            {
                X = picDisplay.Width / 2,
                Y = picDisplay.Height - 100,
            };
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
                foreach (var tower in towers)
                {
                    tower.Render(g);
                }
                foreach (var bullet in bullets.ToList())
                {
                    bullet.Draw(g);
                    bullet.X += bullet.SpeedX;
                    bullet.Y += bullet.SpeedY;
                    if (bullet.Y <= -picDisplay.Height)
                    {
                        bullet.isLose(bullet);
                    }
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
            CreateBullete(e.X, e.Y, player.X, player.Y, 7f, true);
        }



        private void CreateBullete(float X, float Y, float Start_X, float Start_Y, float speed, bool isMainTower)
        {
            particle = new Bullet();
            particle.X = Start_X;
            particle.Y = Start_Y;
            float dx = X - particle.X;
            float dy = Y - particle.Y;

            float length = MathF.Sqrt(dx * dx + dy * dy);

            if (length > 0)
            {
                dx /= length;
                dy /= length;
            }


            particle.SpeedX = dx * speed;
            particle.SpeedY = dy * speed;

            particle.isOverlaps += (p, b) =>
            {
                p.Life = 0;
                b.Life = 0;
                bullets.Remove(b);
                if (isMainTower)
                {
                    ++Points;
                    points.Text = $"Счет: {Points}";
                }
                else
                    CreateBullete(X, Y, Start_X, Start_Y, SpeedBullets, false);
            };
            particle.isLose += (b) =>
            {
                b.Life = 0;
                bullets.Remove(b);
                if (!isMainTower)
                    CreateBullete(X, Y, Start_X, Start_Y, SpeedBullets, false);
            };

            bullets.Add(particle);
        }

        private void btnTower_Click(object sender, EventArgs e)
        {
            if (Points >= 2 && towers.Count < 4)
            {
                Tower tower = new Tower
                {
                    X = TowerX,
                    Y = picDisplay.Height - 50
                };
                TowerX -= picDisplay.Width / 4;
                towers.Add(tower);
                Points -= 2;
                points.Text = $"Счет: {Points}";

                CreateBullete(tower.X, -picDisplay.Height, tower.X, tower.Y, SpeedBullets, false);
                if (towers.Count == 4)
                {
                    btnTower.Text = "Заполнено";
                    btnTower.Enabled = false;
                    btnSpeed.Enabled= true;
                }
            }
        }

        private void btnHP_Click(object sender, EventArgs e)
        {
            if (Points >= 25 && player.Health < 1000)
            {
                int HP = 1000 - player.Health;
                if (HP < 50)
                    player.Health += HP;
                else
                    player.Health += 50;
                Points -= 25;
                points.Text = $"Счет: {Points}";
            }
        }

        private void btnSpeed_Click(object sender, EventArgs e)
        {
            if (Points >= 2) {
                Points -= 2;
                points.Text = $"Счет: {Points}";
                foreach (var tower in towers)
                {
                    SpeedBullets +=1f;
                }
            }
        }
    }
}

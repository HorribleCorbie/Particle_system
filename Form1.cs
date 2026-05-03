using System.Drawing;
using System.Numerics;
using static Particle_system.Particle;
using static System.Windows.Forms.AxHost;

namespace Particle_system
{
    public partial class Form1 : Form
    {
        List<Emitter> emitters = new List<Emitter>();
        Player player;
        TopEmitter top;
        int Points = 0;
        bool isGameOver = false;
        int TowerX;
        List<Bullet> bullets = new List<Bullet>();
        List<Tower> towers = new List<Tower>();
        MouseParticle mouseArea = new MouseParticle();

        int PriceDefense = 15;
        int TowerDefense = 0;
        private float nextAngle = 0f;
        private float angleStep = 0.48f;

        public float SpeedBullets = 5f;


        public Form1()
        {
            InitializeComponent();
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);
            points.Text = $"Счет: {Points}";
            btnDefense.Text = $"Защита = {PriceDefense}";
            picDisplay.BackgroundImage = Image.FromFile("background.jpg");
            picDisplay.BackgroundImageLayout = ImageLayout.Stretch;
            TowerX = picDisplay.Width - picDisplay.Width / 8;

            player = new Player
            {
                X = picDisplay.Width / 2,
                Y = picDisplay.Height - 100,
            };

            player.GameOver += (p) =>
            {
                isGameOver = true;

            };

            top = new TopEmitter
            {
                Width = picDisplay.Width,
                Height = picDisplay.Height,
                GravitationY = 0.25f
            };

            mouseArea.timerIsOver += (m) =>
            {
                mouseArea.color = Color.Green;
                mouseArea.isReady = true;
            };

            emitters.Add(this.top);
            top.impactPoints.Add(player);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isGameOver)
            {
                timer1.Stop();
                using (var g = Graphics.FromImage(picDisplay.Image))
                {
                    g.Clear(Color.Black);

                    var stringFormat = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };

                    var font = new Font("Verdana", 60);

                    g.DrawString(
                        "GAME OVER",
                        font,
                        Brushes.White,
                        picDisplay.Width / 2,
                        picDisplay.Height / 2,
                        stringFormat
                    );
                }
                btnTower.Enabled = false;
                btnSpeed.Enabled = false;
                btnHP.Enabled = false;
                btnDefense.Enabled = false;
                picDisplay.Invalidate();
                picDisplay.Refresh();
                return;
            }

            mouseArea.timer--;
            if (mouseArea.timer <= 0)
            {
                mouseArea.timerIsOver(mouseArea);
            }

            foreach (var emitter in emitters)
            {
                emitter.UpdateState();
            }

            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.Transparent);
                mouseArea.Draw(g);

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
                    bullet.Update();
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
            mouseArea.X = e.X;
            mouseArea.Y = e.Y;
        }

        private void tbDirection_Scroll(object sender, EventArgs e)
        {
            top.ParticlesCount = tbDirection.Value;
            lblDirection.Text = $"{tbDirection.Value}";
        }

        private void picDisplay_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                CreateBullete(e.X, e.Y, player.X, player.Y, 7f, true);
            }
            else if (e.Button == MouseButtons.Right && mouseArea.isReady)
            {
                int bulletCount = 10;
                float radius = 100f;

                for (int i = 0; i < bulletCount; i++)
                {
                    double angle = i * (Math.PI * 2 / bulletCount);

                    float targetX = e.X + (float)(Math.Cos(angle) * radius);
                    float targetY = e.Y + (float)(Math.Sin(angle) * radius);

                    CreateBullete(targetX, targetY, e.X, e.Y, 7f, true, true);
                }
                mouseArea.ResetParticle();
            }
        }

        private void CreateBullete(float endX, float endY, float startX, float startY, float speed, bool isMainTower, bool isRightClickMouse = false)
        {
            Bullet bullet = new Bullet();
            bullet.X = startX;
            bullet.Y = startY;
            float dx = endX - bullet.X;
            float dy = endY - bullet.Y;

            float length = MathF.Sqrt(dx * dx + dy * dy);

            if (length > 0)
            {
                dx /= length;
                dy /= length;
            }

            bullet.SpeedX = dx * speed;
            bullet.SpeedY = dy * speed;

            bullet.isOverlaps += (p, b) =>
            {
                p.Life--;
                b.Life--;

                bullets.Remove(b);
                ++Points;
                points.Text = $"Счет: {Points}";

                if (!isMainTower && !isRightClickMouse)
                {
                    CreateBullete(endX, endY, startX, startY, SpeedBullets, false);
                }

            };
            bullet.isLose += (b) =>
            {
                b.Life--;
                bullets.Remove(b);
                if (!isMainTower && !isRightClickMouse)
                    CreateBullete(endX, endY, startX, startY, SpeedBullets, false);
            };

            bullets.Add(bullet);
        }

        private void btnTower_Click(object sender, EventArgs e)
        {
            if (Points >= 1 && towers.Count < 4)
            {
                Tower tower = new Tower
                {
                    X = TowerX,
                    Y = picDisplay.Height - 50
                };
                TowerX -= picDisplay.Width / 4;
                towers.Add(tower);
                Points -= 1;
                points.Text = $"Счет: {Points}";

                CreateBullete(tower.X, -picDisplay.Height, tower.X, tower.Y, SpeedBullets, false);
                if (towers.Count == 4)
                {
                    btnTower.Text = "Заполнено";
                    btnTower.Enabled = false;
                    btnSpeed.Enabled = true;
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
            if (Points >= 25)
            {
                Points -= 25;
                points.Text = $"Счет: {Points}";
                foreach (var tower in towers)
                {
                    SpeedBullets += 1f;
                }
            }
            if (SpeedBullets >= 15f)
            {
                btnSpeed.Text = "Заполнено";
                btnSpeed.Enabled = false;
            }
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            btnTower.Enabled = true;
            btnSpeed.Enabled = false;
            btnHP.Enabled = true;
            btnDefense.Enabled = true;

            PriceDefense = 25;
            TowerDefense = 0;
            nextAngle = 0f;
            angleStep = 0.48f;

            Points = 0;
            isGameOver = false;
            SpeedBullets = 5f;
            towers.Clear();
            bullets.Clear();
            emitters.Clear();

            btnDefense.Text = $"Защита = {PriceDefense}";
            points.Text = $"Счет: {Points}";
            btnTower.Text = "Башня = 25";
            btnSpeed.Text = "Ускорение = 25";
            mouseArea.ResetParticle();

            picDisplay.BackgroundImage = Image.FromFile("background.jpg");
            picDisplay.BackgroundImageLayout = ImageLayout.Stretch;
            TowerX = picDisplay.Width - picDisplay.Width / 8;

            player = new Player
            {
                X = picDisplay.Width / 2,
                Y = picDisplay.Height - 100,
            };

            player.GameOver += (p) =>
            {
                isGameOver = true;

            };

            top = new TopEmitter
            {
                Width = picDisplay.Width,
                Height = picDisplay.Height,
                GravitationY = 0.25f
            };

            emitters.Add(this.top);
            top.impactPoints.Add(player);
            timer1.Start();
        }

        private void btnDefense_Click(object sender, EventArgs e)
        {
            if (Points >= PriceDefense && TowerDefense < 13)
            {
                var LastDefense = bullets.FindLast(b => b is TowerDefense);
                if (LastDefense is TowerDefense d)
                {
                    nextAngle = d.Angle + angleStep;

                }

                Points -= PriceDefense;
                points.Text = $"Счет: {Points}";
                TowerDefense bullet = new TowerDefense();

                bullet.CenterX = player.X;
                bullet.CenterY = player.Y;
                bullet.MainRadius = 100;

                bullet.Angle = nextAngle;
                bullet.AngularSpeed = 0.07f;

                bullet.isOverlaps += (p, b) =>
                {
                    p.Life = 0;
                    ++Points;
                    points.Text = $"Счет: {Points}";
                };

                PriceDefense += 10;
                btnDefense.Text = $"Защита = {PriceDefense}";

                TowerDefense++;
                bullets.Add(bullet);
                if (TowerDefense == 13)
                {
                    btnDefense.Text = "Заполнено";
                    btnDefense.Enabled = false;
                }
            }
        }

    }
}

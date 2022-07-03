using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Media;

namespace _12IA_Game_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public class Bullets
    {
        public bool shooting = false;
        public Ellipse visual = new Ellipse();
        public Rect hitbox = new Rect();

        public Bullets()
        {
            ImageBrush bulletImage = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/bullet.png"))
            };
            this.visual.Fill = bulletImage;
            this.visual.Height = 6;
            this.visual.Width = 6;
            this.visual.Stretch = Stretch.Fill;
        }
        public void SetHitBox()
        {
            this.hitbox.X = Canvas.GetLeft(this.visual);
            this.hitbox.Y = Canvas.GetTop(this.visual);
            this.hitbox.Width = this.visual.Width;
            this.hitbox.Height = this.visual.Height;
        }

        public void ResetBullet()
        {
            Canvas.SetLeft(this.visual, Canvas.GetLeft(MainWindow.player.middle) - 3);
            Canvas.SetTop(this.visual, Canvas.GetTop(MainWindow.player.middle) - 3);
            Canvas.SetRight(this.visual, Canvas.GetRight(MainWindow.player.middle) + 3);
            Canvas.SetBottom(this.visual, Canvas.GetBottom(MainWindow.player.middle) + 3);
        }
    }

    public class Walls
    {
        public Rectangle visual = new Rectangle();
        public Rect hitbox = new Rect();

        public Walls(double x, double y, double w, double h)
        {
            SolidColorBrush colour = new SolidColorBrush(Colors.Pink);
            this.visual.Fill = colour;
            this.visual.Stroke = colour;
            this.visual.Width = w;
            this.visual.Height = h;
            Canvas.SetLeft(this.visual, x);
            Canvas.SetTop(this.visual, y);
        }
        public void SetHitBox()
        {
            this.hitbox.X = Canvas.GetLeft(this.visual);
            this.hitbox.Y = Canvas.GetTop(this.visual);
            this.hitbox.Width = this.visual.Width;
            this.hitbox.Height = this.visual.Height;
        }
    }

    public class Player
    {
        public Ellipse visual = new Ellipse();
        public Rectangle gun = new Rectangle();
        public Rect hitbox = new Rect();
        public Rectangle middle = new Rectangle();
        public double x = 0;
        public double xpos = 0;
        public double ypos = 0;
        public bool shooting = false;

        public Player()
        {
            ImageBrush playerImage = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/ship aseprite.png"))
            };
            this.visual.Fill = playerImage;
            this.visual.Width = 50;
            this.visual.Height = 50;
            this.visual.Stretch = Stretch.Fill;
            Canvas.SetLeft(this.visual, SystemParameters.PrimaryScreenWidth / 2);
            Canvas.SetTop(this.visual, SystemParameters.PrimaryScreenHeight / 2);

            this.gun.Fill = new SolidColorBrush(Colors.Transparent);
            this.gun.Width = 25;
            this.gun.Height = 6;
            Canvas.SetLeft(this.gun, Canvas.GetLeft(this.visual) + this.visual.Width / 2);
            Canvas.SetTop(this.gun, Canvas.GetTop(this.visual) + this.visual.Height / 2 - (this.gun.Height / 2));

            this.middle.Width = 1;
            this.middle.Height = 1;
            Canvas.SetLeft(this.middle, Canvas.GetLeft(this.visual) + (this.visual.Width / 2));
            Canvas.SetTop(this.middle, Canvas.GetTop(this.visual) + (this.visual.Width / 2));
        }

        public void SetHitBox()
        {
            this.hitbox.X = Canvas.GetLeft(this.visual);
            this.hitbox.Y = Canvas.GetTop(this.visual);
            this.hitbox.Width = this.visual.Width;
            this.hitbox.Height = this.visual.Height;
        }

        public void Shoot(Point direction)
        {
            this.x = 1;
            this.shooting = true;
            if (direction.X < 0)
            {
                this.x *= -1;
            }
            if (direction.X == 0)
            {
                this.x = 0;
            }

            MainWindow.pew.shooting = true;
            Canvas.SetLeft(MainWindow.pew.visual, Canvas.GetLeft(this.middle) - 3);
            Canvas.SetTop(MainWindow.pew.visual, Canvas.GetTop(this.middle) - 3);
            this.xpos = Canvas.GetLeft(MainWindow.pew.visual);
            this.ypos = Canvas.GetTop(MainWindow.pew.visual);

            DispatcherTimer shootTimer = new DispatcherTimer();
            shootTimer.Tick += shootTimer_Tick;
            shootTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            shootTimer.Start();

            double Gradient()
            {
                var gradient = direction.Y / direction.X;
                return gradient * this.x + this.ypos;
            }

            void shootTimer_Tick(object sender, EventArgs e)
            {
                if (x < 0)
                {
                    x -= 5;
                }
                else if (x > 0)
                {
                    x += 5;
                }
                else if (x == 0)
                {
                    x = 0;
                }

                Canvas.SetLeft(MainWindow.pew.visual, (this.x + this.xpos));
                Canvas.SetTop(MainWindow.pew.visual, Gradient());
                if (this.shooting == false)
                {
                    shootTimer.Stop();
                    this.shooting = false;
                    MainWindow.pew.shooting = false;
                }
            }
        }
    }

    public class Enemy
    {
        public Ellipse visual = new Ellipse();
        public Rect hitbox = new Rect();
        public Rectangle middle = new Rectangle();
        public bool alive = true;

        public Enemy(int x, int y)
        {
            ImageBrush playerImage = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/enemy.png"))
            };
            this.visual.Fill = playerImage;
            this.visual.Width = 60;
            this.visual.Height = 50;
            this.visual.Stretch = Stretch.Fill;
            Canvas.SetLeft(this.visual, x);
            Canvas.SetTop(this.visual, y);

            this.middle.Width = 1;
            this.middle.Height = 1;
            Canvas.SetLeft(this.middle, Canvas.GetLeft(this.visual) + (this.visual.Width / 2));
            Canvas.SetTop(this.middle, Canvas.GetTop(this.visual) + (this.visual.Width / 2));
        }

        public void SetHitBox()
        {
            this.hitbox.X = Canvas.GetLeft(this.visual);
            this.hitbox.Y = Canvas.GetTop(this.visual);
            this.hitbox.Width = this.visual.Width;
            this.hitbox.Height = this.visual.Height;
        }
    }

    public partial class MainWindow : Window
    {
        public Point pos, enemyPos;
        public double angle, enemyAngle;
        public static Bullets pew = new Bullets();
        public static List<Walls> walls = new List<Walls>();
        public static List<Enemy> enemies = new List<Enemy>();
        public static Player player = new Player();
        public bool moveUp, moveDown, moveLeft, moveRight;
        public int score, interval, period;
        public int enemySpeed = 3;
        public int health = 3;
        public Key UpControl, DownControl, LeftControl, RightControl;
        public string ShootControl, RecallControl;

        DispatcherTimer tmrEngine = new DispatcherTimer();
        DispatcherTimer tmrSpawn = new DispatcherTimer();
        DispatcherTimer tmrIncrement = new DispatcherTimer();
        public TimeSpan decrease = new TimeSpan(0, 0, 0, 0, 100);
        public TimeSpan spawn = new TimeSpan(0, 0, 0, 3, 500);
        public TimeSpan minimum = new TimeSpan(0, 0, 0, 0, 200);

        readonly SoundPlayer playSoundtrack = new SoundPlayer(Properties.Resources.Cubic_Planets);

        public MainWindow(Key Up, Key Down, Key Left, Key Right, string Shoot, string Recall)
        {
            UpControl = Up; DownControl = Down; LeftControl = Left; RightControl = Right; ShootControl = Shoot; RecallControl = Recall;

            InitializeComponent();
            InitializeAnimation();
            Game_Canvas.Height = SystemParameters.PrimaryScreenHeight;
            Game_Canvas.Width = SystemParameters.PrimaryScreenWidth;

            tmrEngine.Tick += GameEngine;
            tmrEngine.Interval = new TimeSpan(0, 0, 0, 0, 5);   //dispatcher timer for basic game function
            tmrEngine.Start();

            tmrSpawn.Tick += Spawn;
            tmrSpawn.Interval = spawn;                          //dispatcher timer for spawning enemies
            tmrSpawn.Start();

            tmrIncrement.Tick += Increment;
            tmrIncrement.Interval = new TimeSpan(0, 0, 1);      //dispatcher timer for decreasing enemy spawn time
            tmrIncrement.Start();

            Game_Canvas.Focus();

            playSoundtrack.PlayLooping();

            MakeWalls();    

            Game_Canvas.Children.Add(pew.visual);
            Game_Canvas.Children.Add(player.visual);        //adding player elements to the canvas 
            Game_Canvas.Children.Add(player.gun);
            Game_Canvas.Children.Add(player.middle);

            if (ShootControl == null)
            {
                UpControl = Key.W;
                DownControl = Key.S;
                LeftControl = Key.A;
                RightControl = Key.D; ;
                ShootControl = "LeftMouse";
                RecallControl = "RightMouse"; 
            }
        }

        private void InitializeAnimation()      //background scroll
        {
            var menuScroll = new DoubleAnimation
            {
                From = 0,
                To = -1920,
                Duration = TimeSpan.FromSeconds(80/3),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true,
            };
            imgBackground.Width = SystemParameters.PrimaryScreenWidth * 2;
            imgBackground.BeginAnimation(Canvas.LeftProperty, menuScroll);
        }

        private void Exit(object sender, RoutedEventArgs e)     //exit button
        {
            this.Close();       
        }

        private void TextHighlight(object sender, MouseEventArgs e)
        {
            Highlight(e.Source as TextBlock);
        }

        private void TextDehighlight(object sender, MouseEventArgs e)
        {
            Dehighlight(e.Source as TextBlock);
        }

        private void Highlight(TextBlock text)
        {
            text.Background = new SolidColorBrush(Colors.White);
        }

        private void Dehighlight(TextBlock text)
        {
            text.Background = new SolidColorBrush(Colors.Transparent);
        }

        public void MakeWalls()
        {
            walls.Add(new Walls(-100, 0, 100, SystemParameters.PrimaryScreenHeight));    //vertical walls
            walls.Add(new Walls(SystemParameters.PrimaryScreenWidth, 0, 100, SystemParameters.PrimaryScreenHeight));

            walls.Add(new Walls(0, -100, SystemParameters.PrimaryScreenWidth, 100));     //horizontal walls
            walls.Add(new Walls(0, SystemParameters.PrimaryScreenHeight, SystemParameters.PrimaryScreenWidth, 100));   

            foreach (Walls item in walls)
            {
                Game_Canvas.Children.Add(item.visual);      //add all walls to the main canvas
            }
        }

        private void GameEngine(object sender, EventArgs e)
        {
            foreach (Walls item in walls)
            {
                item.SetHitBox();
            }

            foreach (Walls x in walls)      //wall collision detection 
            {
                if (CollisionDect(pew.hitbox, x.hitbox))
                {
                    player.shooting = false;
                    pew.shooting = false;
                    pew.ResetBullet();
                }
            }

            if (moveLeft && Canvas.GetLeft(player.visual) > 0)      //player movement 
            {
                Canvas.SetLeft(player.visual, Canvas.GetLeft(player.visual) - 10);
                Canvas.SetLeft(player.gun, Canvas.GetLeft(player.visual) + player.visual.Width / 2);
                Canvas.SetTop(player.gun, Canvas.GetTop(player.visual) + player.visual.Height / 2 - (player.gun.Height / 2));
                Canvas.SetLeft(player.middle, Canvas.GetLeft(player.visual) + (player.visual.Width / 2));
                Canvas.SetTop(player.middle, Canvas.GetTop(player.visual) + (player.visual.Width / 2));
                player.SetHitBox();
                pew.SetHitBox();
                if (player.shooting == false && pew.shooting == false)
                {
                    pew.ResetBullet();
                }
            }
            if (moveRight && (Canvas.GetLeft(player.visual) + player.visual.Width) < 1920)
            {
                Canvas.SetLeft(player.visual, Canvas.GetLeft(player.visual) + 10);
                Canvas.SetLeft(player.gun, Canvas.GetLeft(player.visual) + player.visual.Width / 2);
                Canvas.SetTop(player.gun, Canvas.GetTop(player.visual) + player.visual.Height / 2 - (player.gun.Height / 2));
                Canvas.SetLeft(player.middle, Canvas.GetLeft(player.visual) + (player.visual.Width / 2));
                Canvas.SetTop(player.middle, Canvas.GetTop(player.visual) + (player.visual.Width / 2));
                player.SetHitBox();
                pew.SetHitBox();
                if (player.shooting == false && pew.shooting == false)
                {
                    pew.ResetBullet();
                }
            }
            if (moveUp && Canvas.GetTop(player.visual) > 0)
            {
                Canvas.SetTop(player.visual, Canvas.GetTop(player.visual) - 10);
                Canvas.SetLeft(player.gun, Canvas.GetLeft(player.visual) + player.visual.Width / 2);
                Canvas.SetTop(player.gun, Canvas.GetTop(player.visual) + player.visual.Height / 2 - (player.gun.Height / 2));
                Canvas.SetLeft(player.middle, Canvas.GetLeft(player.visual) + (player.visual.Width / 2));
                Canvas.SetTop(player.middle, Canvas.GetTop(player.visual) + (player.visual.Width / 2));
                player.SetHitBox();
                pew.SetHitBox();
                if (player.shooting == false && pew.shooting == false)
                {
                    pew.ResetBullet();
                }
            }
            if (moveDown && (Canvas.GetTop(player.visual) + player.visual.Height) < 1080)
            {
                Canvas.SetTop(player.visual, Canvas.GetTop(player.visual) + 10);
                Canvas.SetLeft(player.gun, Canvas.GetLeft(player.visual) + player.visual.Width / 2);
                Canvas.SetTop(player.gun, Canvas.GetTop(player.visual) + player.visual.Height / 2 - (player.gun.Height / 2));
                Canvas.SetLeft(player.middle, Canvas.GetLeft(player.visual) + (player.visual.Width / 2));
                Canvas.SetTop(player.middle, Canvas.GetTop(player.visual) + (player.visual.Width / 2));
                player.SetHitBox();
                pew.SetHitBox();
                if (player.shooting == false && pew.shooting == false)
                {
                    pew.ResetBullet();
                }
            }
          
            foreach (Enemy item in enemies)     //enemy movement 
            {
                if (item.alive == true)
                {
                    if (Canvas.GetTop(item.visual) < Canvas.GetTop(player.visual))
                    {
                        Canvas.SetTop(item.visual, Canvas.GetTop(item.visual) + enemySpeed);
                        Canvas.SetLeft(item.middle, Canvas.GetLeft(item.visual) + (item.visual.Width / 2));
                        Canvas.SetTop(item.middle, Canvas.GetTop(item.visual) + (item.visual.Width / 2));
                    }
                    if (Canvas.GetTop(item.visual) > Canvas.GetTop(player.visual))
                    {
                        Canvas.SetTop(item.visual, Canvas.GetTop(item.visual) - enemySpeed);
                        Canvas.SetLeft(item.middle, Canvas.GetLeft(item.visual) + (item.visual.Width / 2));
                        Canvas.SetTop(item.middle, Canvas.GetTop(item.visual) + (item.visual.Width / 2));
                    }
                    if (Canvas.GetLeft(item.visual) < Canvas.GetLeft(player.visual))
                    {
                        Canvas.SetLeft(item.visual, Canvas.GetLeft(item.visual) + enemySpeed);
                        Canvas.SetLeft(item.middle, Canvas.GetLeft(item.visual) + (item.visual.Width / 2));
                        Canvas.SetTop(item.middle, Canvas.GetTop(item.visual) + (item.visual.Width / 2));
                    }
                    if ((Canvas.GetLeft(item.visual) + item.visual.Width) > (Canvas.GetLeft(player.visual) + player.visual.Width))
                    {
                        Canvas.SetLeft(item.visual, Canvas.GetLeft(item.visual) - enemySpeed);
                        Canvas.SetLeft(item.middle, Canvas.GetLeft(item.visual) + (item.visual.Width / 2));
                        Canvas.SetTop(item.middle, Canvas.GetTop(item.visual) + (item.visual.Width / 2));
                    }
                }
            }

            pew.SetHitBox();
            player.SetHitBox();

            if (ShootControl == "LeftMouse")
            {
                if (player.shooting == false)       //shoot bullet
                {
                    if (Mouse.LeftButton == MouseButtonState.Pressed)
                    {
                        player.shooting = true;
                        player.Shoot(pos);
                        pew.shooting = true;
                    }
                }

                if (player.shooting == true)        //recall bullet
                {
                    if (Mouse.RightButton == MouseButtonState.Pressed)
                    {
                        pew.ResetBullet();
                        pew.shooting = false;
                        player.shooting = false;
                    }
                }
            }
            if (ShootControl == "RightMouse")
            {
                if (player.shooting == false)       //shoot bullet
                {
                    if (Mouse.RightButton == MouseButtonState.Pressed)
                    {
                        player.shooting = true;
                        player.Shoot(pos);
                        pew.shooting = true;
                    }
                }

                if (player.shooting == true)        //recall bullet
                {
                    if (Mouse.LeftButton == MouseButtonState.Pressed)
                    {
                        pew.ResetBullet();
                        pew.shooting = false;
                        player.shooting = false;
                    }
                }
            }

            pos = Mouse.GetPosition(player.middle);
            angle = GetAngle(pos);
                                                                                                        //rotating player
            RotateTransform gunrotateTransform = new RotateTransform(angle, 0, player.gun.Height / 2);      
            player.gun.RenderTransform = gunrotateTransform;
            RotateTransform playerrotateTransform = new RotateTransform(angle, player.visual.Width / 2, player.visual.Height / 2);
            player.visual.RenderTransform = playerrotateTransform;

            foreach (Enemy item in enemies)
            {
                item.SetHitBox();
            }

            foreach (Enemy x in enemies)
            {
                if (x.alive == true)
                {
                    if (CollisionDect(x.hitbox, pew.hitbox))
                    {
                        player.shooting = false;
                        pew.shooting = false;                           //enemy/bullet collision detection
                        pew.ResetBullet();
                        Game_Canvas.Children.Remove(x.visual);
                        x.alive = false;
                        score += 1;
                    }
                }
            }

            foreach (Enemy x in enemies)
            {
                if (x.alive == true)
                {
                    if (CollisionDect(x.hitbox, player.hitbox))       //enemy/player collision detection
                    {
                        Game_Canvas.Children.Remove(x.visual);
                        x.alive = false;
                        health -= 1;
                    }
                }
            }

            txtHealth.Text = $"Health = {health}";

            foreach (Enemy item in enemies)     //enemy rotation
            {
                enemyPos = new Point(Canvas.GetLeft(player.middle) - Canvas.GetLeft(item.middle), Canvas.GetTop(player.middle) - Canvas.GetTop(item.middle));
                enemyAngle = GetAngle(enemyPos);
                RotateTransform enemyRotateTransform = new RotateTransform(enemyAngle, item.visual.Width / 2, item.visual.Height / 2);
                item.visual.RenderTransform = enemyRotateTransform;
            }

            if (health < 1) //stop game on player death
            {
                GameOver gameOver = new GameOver(score);
                gameOver.Show();
                Game_Canvas.Children.Clear();
                walls.Clear();
                enemies.Clear();
                this.Close();
                tmrEngine.Stop();
                tmrSpawn.Stop();
            }
        }

        public void Spawn(object sender, EventArgs e)
        {
            Random rand = new Random();
            int spawnLocation = rand.Next(1, 4);
            int locationX, locationY;

            if (spawnLocation == 1)                         //spawn top of screen
            {
                locationX = rand.Next(0, 1920);

                enemies.Add(new Enemy(locationX, 0));

                Game_Canvas.Children.Add(enemies[enemies.Count - 1].visual);
            }
            else if (spawnLocation == 2)                   //spawn right of screen
            {
                locationY = rand.Next(0, 1080);
                enemies.Add(new Enemy(1920, locationY));

                Game_Canvas.Children.Add(enemies[enemies.Count - 1].visual);
            }
            else if (spawnLocation == 3)                  //spawn bottom of screen
            {
                locationX = rand.Next(0, 1080);
                enemies.Add(new Enemy(locationX, 1080));

                Game_Canvas.Children.Add(enemies[enemies.Count - 1].visual);
            }
            else if (spawnLocation == 4)                 //spawn left of screen
            {
                locationY = rand.Next(0, 1080);
                enemies.Add(new Enemy(0, locationY));

                Game_Canvas.Children.Add(enemies[enemies.Count - 1].visual);
            }
        }

        public void Increment(object sender, EventArgs e)       //gradual difficult curve, lowers enemy spawn time
        {
            interval += 1;
            if (interval == 10)
            {
                interval = 0;
                tmrSpawn.Interval -= decrease;
            }
            if (tmrSpawn.Interval < minimum)
            {
                tmrIncrement.Stop();
            }
        }

        static double GetAngle(Point mouse)
        {
            if (mouse.X > 0 && mouse.Y > 0) //bottom right
            {
                return (Math.Atan(mouse.Y / mouse.X)) * (180 / Math.PI);
            }
            else if (mouse.X < 0 && mouse.Y > 0) //bottom left
            {
                mouse.X *= -1;
                var ang = Math.Atan(mouse.Y / mouse.X) * (180 / Math.PI);
                return 90 + (90 - ang);
            }
            else if (mouse.X > 0 && mouse.Y < 0) //top right
            {
                mouse.Y *= -1;
                var ang = Math.Atan(mouse.Y / mouse.X) * (180 / Math.PI);
                return 360 - ang;
            }
            else if (mouse.X < 0 && mouse.Y < 0) //top left
            {
                mouse.X *= -1;
                mouse.Y *= -1;
                var ang = Math.Atan(mouse.Y / mouse.X) * (180 / Math.PI);
                return 180 + ang;
            }
            return -1;
        }

        public bool CollisionDect(Rect a, Rect b)
        {
            if (a.IntersectsWith(b))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Game_Canvas_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == LeftControl)      //if user has released the left key, set moveLeft to false, etc.
            {
                moveLeft = false;
            }
            if (e.Key == RightControl)
            {
                moveRight = false;
            }
            if (e.Key == UpControl)
            {
                moveUp = false;
            }
            if (e.Key == DownControl)
            {
                moveDown = false;
            }
        }

        private void Game_Canvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == LeftControl)      //if user is pressing the left key, set moveLeft to true, etc.
            {
                moveLeft = true;
            }
            if (e.Key == RightControl)
            {
                moveRight = true;
            }
            if (e.Key == UpControl)
            {
                moveUp = true;
            }
            if (e.Key == DownControl)
            {
                moveDown = true;
            }
        }
    }
}

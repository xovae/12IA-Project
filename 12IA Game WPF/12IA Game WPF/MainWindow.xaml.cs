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
            visual.Fill = bulletImage;
            visual.Height = 6;
            visual.Width = 6;
            visual.Stretch = Stretch.Fill;
        }
        public void SetHitBox()
        {
            hitbox.X = Canvas.GetLeft(visual);
            hitbox.Y = Canvas.GetTop(visual);
            hitbox.Width = visual.Width;
            hitbox.Height = visual.Height;
        }

        public void ResetBullet()
        {
            Canvas.SetLeft(visual, Canvas.GetLeft(MainWindow.player.middle) - 3);
            Canvas.SetTop(visual, Canvas.GetTop(MainWindow.player.middle) - 3);
            Canvas.SetRight(visual, Canvas.GetRight(MainWindow.player.middle) + 3);
            Canvas.SetBottom(visual, Canvas.GetBottom(MainWindow.player.middle) + 3);
        }
    }

    public class Walls
    {
        public Rectangle visual = new Rectangle();
        public Rect hitbox = new Rect();

        public Walls(double x, double y, double w, double h)
        {
            SolidColorBrush colour = new SolidColorBrush(Colors.Pink);
            visual.Fill = colour;
            visual.Stroke = colour;
            visual.Width = w;
            visual.Height = h;
            Canvas.SetLeft(visual, x);
            Canvas.SetTop(visual, y);
        }
        public void SetHitBox()
        {
            hitbox.X = Canvas.GetLeft(visual);
            hitbox.Y = Canvas.GetTop(visual);
            hitbox.Width = visual.Width;
            hitbox.Height = visual.Height;
        }
    }

    public class Player
    {
        public Ellipse visual = new Ellipse();
        public Rectangle gun = new Rectangle();
        public Rect hitbox = new Rect();
        public Rectangle middle = new Rectangle();
        public double x = 0;
        public double y = 0;
        public double xpos = 0;
        public double ypos = 0;
        public bool shooting = false;
        public double yinc; 
        public double xinc;
        public int bulletSpeed = 10;

        public Player()
        {
            ImageBrush playerImage = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/ship aseprite.png"))
            };
            visual.Fill = playerImage;
            visual.Width = 50;
            visual.Height = 50;
            visual.Stretch = Stretch.Fill;
            Canvas.SetLeft(visual, SystemParameters.PrimaryScreenWidth / 2);
            Canvas.SetTop(visual, SystemParameters.PrimaryScreenHeight / 2);

            gun.Fill = new SolidColorBrush(Colors.Transparent);
            gun.Width = 25;
            gun.Height = 6;
            Canvas.SetLeft(gun, Canvas.GetLeft(visual) + visual.Width / 2);
            Canvas.SetTop(gun, Canvas.GetTop(visual) + visual.Height / 2 - (gun.Height / 2));

            middle.Width = 1;
            middle.Height = 1;
            Canvas.SetLeft(middle, Canvas.GetLeft(visual) + (visual.Width / 2));
            Canvas.SetTop(middle, Canvas.GetTop(visual) + (visual.Width / 2));
        }

        public void SetHitBox()
        {
            hitbox.X = Canvas.GetLeft(visual);
            hitbox.Y = Canvas.GetTop(visual);
            hitbox.Width = visual.Width;
            hitbox.Height = visual.Height;
        }

        public void Shoot(Point direction)
        {
            var angle = Math.Atan(Math.Abs(direction.Y) / Math.Abs(direction.X));
            yinc = Math.Sin(angle) * bulletSpeed;
            xinc = Math.Cos(angle) * bulletSpeed;
            x = 1; //x counter for movement
            y = 1; //y counter for movement
            shooting = true;
          
            if (direction.X < 0)
            {
                xinc *= -1;
            }
            if (direction.Y < 0)
            {
               yinc *= -1;
            }

            MainWindow.pew.shooting = true;
            Canvas.SetLeft(MainWindow.pew.visual, Canvas.GetLeft(middle) - 3);
            Canvas.SetTop(MainWindow.pew.visual, Canvas.GetTop(middle) - 3);
            xpos = Canvas.GetLeft(MainWindow.pew.visual);
            ypos = Canvas.GetTop(MainWindow.pew.visual);

            DispatcherTimer shootTimer = new DispatcherTimer();
            shootTimer.Tick += shootTimer_Tick;
            shootTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            shootTimer.Start();

            void shootTimer_Tick(object sender, EventArgs e)
            {
                x += xinc;
                y += yinc;
                if (x > SystemParameters.PrimaryScreenHeight || x < -SystemParameters.PrimaryScreenHeight)
                {
                    shooting = false;
                    MainWindow.pew.shooting = false;
                    shootTimer.Stop();
                    MainWindow.pew.ResetBullet();
                }

                Canvas.SetLeft(MainWindow.pew.visual, x + xpos);
                Canvas.SetTop(MainWindow.pew.visual, y + ypos);

                if (shooting == false)
                {
                    shootTimer.Stop();
                    shooting = false;
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
            visual.Fill = playerImage;
            visual.Width = 60;
            visual.Height = 50;
            visual.Stretch = Stretch.Fill;
            Canvas.SetLeft(visual, x);
            Canvas.SetTop(visual, y);

            middle.Width = 1;
            middle.Height = 1;
            Canvas.SetLeft(middle, Canvas.GetLeft(visual) + (visual.Width / 2));
            Canvas.SetTop(middle, Canvas.GetTop(visual) + (visual.Width / 2));
        }

        public void SetHitBox()
        {
            hitbox.X = Canvas.GetLeft(visual);
            hitbox.Y = Canvas.GetTop(visual);
            hitbox.Width = visual.Width;
            hitbox.Height = visual.Height;
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
        public int score, interval, period, enemySpeed, health, threshold;
        public int playerSpeed = 10;
        public Key UpControl, DownControl, LeftControl, RightControl;
        public string ShootControl, RecallControl, Difficulty;

        readonly DispatcherTimer tmrEngine = new DispatcherTimer();
        readonly DispatcherTimer tmrSpawn = new DispatcherTimer();
        readonly DispatcherTimer tmrIncrement = new DispatcherTimer();

        public TimeSpan decrease = new TimeSpan(0, 0, 0, 0, 100);
        public TimeSpan spawn = new TimeSpan(0, 0, 0, 3, 500);
        public TimeSpan minimum = new TimeSpan(0, 0, 0, 0, 200);

        readonly SoundPlayer playSoundtrack = new SoundPlayer(Properties.Resources.Cubic_Planets);

        public MainWindow(Key Up, Key Down, Key Left, Key Right, string Shoot, string Recall, string diff)
        {
            UpControl = Up; DownControl = Down; LeftControl = Left; RightControl = Right; ShootControl = Shoot; RecallControl = Recall; Difficulty = diff;

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

            if (Difficulty == "Easy")
            {
                enemySpeed = 3;
                health = 5;
                threshold = 15;
            }
            else if (Difficulty == "Medium")
            {
                enemySpeed = 5;
                health = 3;
                threshold = 10;
            }
            else if (Difficulty == "Hard")
            {
                enemySpeed = 7;
                health = 2;
                threshold = 5;
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
            Close();       
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

            foreach (Walls x in walls)      
                {
                    if (CollisionDect(pew.hitbox, x.hitbox))
                    {
                        player.shooting = false;
                        pew.shooting = false;
                        pew.ResetBullet();
                    }
            }       //wall collision detection 

            //player movement 

            if (moveLeft && Canvas.GetLeft(player.visual) > 0)     
            {
                Canvas.SetLeft(player.visual, Canvas.GetLeft(player.visual) - playerSpeed);
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
            }                               //move left
            if (moveRight && (Canvas.GetLeft(player.visual) + player.visual.Width) < 1920)
            {
                Canvas.SetLeft(player.visual, Canvas.GetLeft(player.visual) + playerSpeed);
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
            }   //move right
            if (moveUp && Canvas.GetTop(player.visual) > 0)
            {
                Canvas.SetTop(player.visual, Canvas.GetTop(player.visual) - playerSpeed);
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
            }                                  //move up
            if (moveDown && (Canvas.GetTop(player.visual) + player.visual.Height) < 1080)
            {
                Canvas.SetTop(player.visual, Canvas.GetTop(player.visual) + playerSpeed);
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
            }    //move down
          
            foreach (Enemy item in enemies)     
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
            }                                                  //enemy movement 

            pew.SetHitBox();
            player.SetHitBox();

            pos = Mouse.GetPosition(player.middle);
            angle = GetAngle(pos);

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
                                                                                                       
            RotateTransform gunrotateTransform = new RotateTransform(angle, 0, player.gun.Height / 2);                   //rotating player
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
                        pew.shooting = false;                         
                        pew.ResetBullet();
                        Game_Canvas.Children.Remove(x.visual);
                        x.alive = false;
                        score += 1;
                    }
                }
            }     //enemy/bullet collision detection

            foreach (Enemy x in enemies)
            {
                if (x.alive == true)
                {
                    if (CollisionDect(x.hitbox, player.hitbox))      
                    {
                        Game_Canvas.Children.Remove(x.visual);
                        x.alive = false;
                        health -= 1;
                    }
                }
            }     //enemy/player collision detection

            txtHealth.Text = $"Health = {health}";
            txtScore.Text = $"Score = {score}";

            foreach (Enemy item in enemies)
            {
                enemyPos = new Point(Canvas.GetLeft(player.middle) - Canvas.GetLeft(item.middle), Canvas.GetTop(player.middle) - Canvas.GetTop(item.middle));
                enemyAngle = GetAngle(enemyPos);
                RotateTransform enemyRotateTransform = new RotateTransform(enemyAngle, item.visual.Width / 2, item.visual.Height / 2);
                item.visual.RenderTransform = enemyRotateTransform;
            }    //enemy rotation

            if (health < 1) 
            {
                GameOver gameOver = new GameOver(score, UpControl, DownControl, LeftControl, RightControl, ShootControl, RecallControl, Difficulty);
                gameOver.Show();
                Game_Canvas.Children.Clear();
                walls.Clear();
                enemies.Clear();
                Close();
                tmrEngine.Stop();
                tmrSpawn.Stop();
            }   //stop game on player death
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
        }         //spawning enemies

        public void Increment(object sender, EventArgs e)       //gradual difficult curve, lowers enemy spawn time
        {
            interval += 1;
            if (interval == threshold)
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

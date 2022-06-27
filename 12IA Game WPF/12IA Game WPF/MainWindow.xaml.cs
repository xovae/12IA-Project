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
            SolidColorBrush orange = new SolidColorBrush(Colors.Orange);
            this.visual.Fill = orange;
            this.visual.Stroke = orange;
            this.visual.Height = 6;
            this.visual.Width = 6;
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
            Canvas.SetRight(this.visual, 60);
            Canvas.SetBottom(this.visual, 60);
        }
    }

    public class HorzWalls
    {
        public Rectangle visual = new Rectangle();
        public Rect hitbox = new Rect();

        public HorzWalls(int x, int y)
        {
            SolidColorBrush colour = new SolidColorBrush(Colors.Pink);
            this.visual.Fill = colour;
            this.visual.Stroke = colour;
            this.visual.Width = 1920;
            this.visual.Height = 100;
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

    public class VertWalls
    {
        public Rectangle visual = new Rectangle();
        public Rect hitbox = new Rect();
        public VertWalls(int x, int y)
        {
            SolidColorBrush colour = new SolidColorBrush(Colors.Pink);
            this.visual.Fill = colour;
            this.visual.Stroke = colour;
            this.visual.Width = 50;
            this.visual.Height = 1080;
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
                ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/black arrow.png"))
            };
            this.visual.Fill = playerImage;
            this.visual.Width = 50;
            this.visual.Height = 50;
            this.visual.Stretch = Stretch.Fill;
            Canvas.SetLeft(this.visual, 1920 / 2);
            Canvas.SetTop(this.visual, 1080 / 2);

            this.gun.Fill = new SolidColorBrush(Colors.Purple);
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
        
        public Enemy()
        {
            SolidColorBrush colour = new SolidColorBrush(Colors.Red);
            this.visual.Fill = colour;
            this.visual.Stroke = colour;
            this.visual.Width = 50;
            this.visual.Height = 50;
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
        public Point pos;
        public double angle;
        public static Bullets pew = new Bullets();
        public static List<VertWalls> vertBorders = new List<VertWalls>();
        public static List<HorzWalls> horzBorders = new List<HorzWalls>();
        public static Player player = new Player();


        SoundPlayer playSoundtrack = new SoundPlayer(Properties.Resources.Cubic_Planets);

        public MainWindow()
        {
            InitializeComponent();
            InitializeAnimation();
            Game_Canvas.Height = SystemParameters.PrimaryScreenHeight;
            Game_Canvas.Width = SystemParameters.PrimaryScreenWidth;

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += GameEngine;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 5);
            dispatcherTimer.Start();
            Game_Canvas.Focus();

            playSoundtrack.PlayLooping();

            MakeVertWalls();
            MakeHorzWalls();

            Game_Canvas.Children.Add(pew.visual);
            Game_Canvas.Children.Add(player.visual);
            Game_Canvas.Children.Add(player.gun);
            Game_Canvas.Children.Add(player.middle);
           
        }

        private void InitializeAnimation()
        {
            var menuScroll = new DoubleAnimation
            {
                From = -0,
                To = -1080,
                Duration = TimeSpan.FromSeconds(15),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true,
            };
            imgBackground.BeginAnimation(Canvas.LeftProperty, menuScroll);
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void MakeVertWalls()
        {
            vertBorders.Add(new VertWalls(-50, 0)); 
            vertBorders.Add(new VertWalls(1920, 0)); 

            foreach (VertWalls item in vertBorders)
            {
                Game_Canvas.Children.Add(item.visual);
            }
        }

        public void MakeHorzWalls()
        {
            horzBorders.Add(new HorzWalls(0, -100));
            horzBorders.Add(new HorzWalls(0, 1080));

            foreach (HorzWalls item in horzBorders)
            {
                Game_Canvas.Children.Add(item.visual);
            }
        }

        private void GameEngine(object sender, EventArgs e)
        {
            foreach (VertWalls item in vertBorders)
            {
                item.SetHitBox();
            }
           
            foreach (VertWalls x in vertBorders)
            {
                if (CollisionDect(pew.hitbox, x.hitbox))
                {
                    player.shooting = false;
                    pew.shooting = false;
                    pew.ResetBullet();
                }
            }

            foreach (HorzWalls item in horzBorders)
            {
                item.SetHitBox();
            }

            foreach (HorzWalls x in horzBorders)
            {
                if (CollisionDect(pew.hitbox, x.hitbox))
                {
                    player.shooting = false;
                    pew.shooting = false;
                    pew.ResetBullet();
                }
            }

            pew.SetHitBox();

            pos = Mouse.GetPosition(player.middle);
            angle = GetAngle(pos);

            if (player.shooting == false) 
            {
                if (Mouse.LeftButton == MouseButtonState.Pressed)
                {
                    player.shooting = true;
                    player.Shoot(pos);
                    pew.shooting = true; 
                }
            }

            RotateTransform gunrotateTransform = new RotateTransform(angle, 0, player.gun.Height / 2);
            player.gun.RenderTransform = gunrotateTransform;
            RotateTransform playerrotateTransform = new RotateTransform(angle, player.visual.Width / 2, player.visual.Height / 2);
            player.visual.RenderTransform = playerrotateTransform;

            Random spawn = new Random();
            int spawnLocation = spawn.Next(1, 4);
            if (spawnLocation == 1)
            {
                spawn.Next(0, 1080);

            }
            else if(spawnLocation == 2)
            {

            }
            else if (spawnLocation == 3)
            {

            }
            else if (spawnLocation == 4)
            {

            }

            //var pos = GetMousePos(frmGame, wWidth, wHeight);
            //var angle = GetAngle(pos);
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

        //static Point GetMousePos(Window back, double w, double h)
        //{
        //    Point mousePos = Mouse.GetPosition(back);
        //    var ofbackX = w / 2 * -1;
        //    var ofbackY = h / 2 * -1;

        //    mousePos.Offset(ofbackX, ofbackY);
        //    return mousePos;

        //}

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
    }
}

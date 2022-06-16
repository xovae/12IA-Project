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

    public class Walls
    {
        public Rectangle visual = new Rectangle();
        public Rect hitbox = new Rect();
        public Walls( int x, int y)
        {
            SolidColorBrush color = new SolidColorBrush(Colors.Pink);
            this.visual.Fill = color;
            this.visual.Stroke = color;
            this.visual.Width = 10;
            this.visual.Height = 1920;
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
            //ImageBrush playerImage = new ImageBrush();
            //playerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/black arrow.png"));
            //this.visual.Fill = playerImage;
            //this.visual.Width = 1138;
            //this.visual.Height = 494;


            this.visual.Fill = new SolidColorBrush(Colors.Red);
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

            MainWindow.pew.shooting = true;
            Canvas.SetLeft(MainWindow.pew.visual, Canvas.GetLeft(this.middle) - 3);
            Canvas.SetTop(MainWindow.pew.visual, Canvas.GetTop(this.middle) - 3);
            this.xpos = Canvas.GetLeft(MainWindow.pew.visual);
            this.ypos = Canvas.GetTop(MainWindow.pew.visual);

            DispatcherTimer shootTimer = new DispatcherTimer();
            shootTimer.Tick += shootTimer_Tick;
            shootTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            shootTimer.Start();

            double Equation()
            {
                var equation = direction.Y / direction.X;
                return equation * this.x + this.ypos;
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

                Canvas.SetLeft(MainWindow.pew.visual, (this.x + this.xpos));
                Canvas.SetTop(MainWindow.pew.visual, Equation());
                if (this.shooting == false)
                {
                    shootTimer.Stop();
                    this.shooting = false;
                    MainWindow.pew.shooting = false;
                }
            }
        }
    }

    public partial class MainWindow : Window
    {
        public Point pos;
        public double angle;
        //public static Label xx = new Label();
        public static Bullets pew = new Bullets();
        public static List<Walls> borders = new List<Walls>();
        public static Player player = new Player();
        bool moveLeft, moveRight, moveUp, moveDown;

        public double wHeight, wWidth; //doubles storing window width and height
       

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

            MakeWalls();

            Game_Canvas.Children.Add(player.visual);
            Game_Canvas.Children.Add(player.gun);
            Game_Canvas.Children.Add(player.middle);
            Game_Canvas.Children.Add(pew.visual);

            //ImageBrush playerImage = new ImageBrush();   // make a player image, image brush
            //// load the player image into it
            //playerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/black arrow.png"));
            //// assign the player to the player rectangle fill
            //player.Fill = playerImage;

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

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Game_Canvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                moveLeft = true;
            }
            if (e.Key == Key.Right)
            {
                moveRight = true;
            }
        }

        private void Game_Canvas_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                moveLeft = false;
            }
            if (e.Key == Key.Right)
            {
                moveRight = false;
            }
        }

        public void MakeWalls()
        {
            borders.Add(new Walls(-10, Convert.ToInt32(Game_Canvas.ActualHeight))); 
            borders.Add(new Walls(1920, Convert.ToInt32(Game_Canvas.ActualHeight))); 

            foreach (Walls item in borders)
            {
                Game_Canvas.Children.Add(item.visual);
            }
        }



        private void GameEngine(object sender, EventArgs e)
        {
            foreach (Walls item in borders)
            {
                item.SetHitBox();
            }
            pew.SetHitBox();

            foreach (Walls x in borders)
            {
                if (CollisionDect(pew.hitbox, x.hitbox))
                {
                    player.shooting = false;
                    pew.shooting = false;
                    pew.ResetBullet();
                }
            }

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

            RotateTransform rotateTransform = new RotateTransform(angle, 0, player.gun.Height / 2);
            player.gun.RenderTransform = rotateTransform;
            //double left = (Game_Canvas.ActualWidth - player.ActualWidth) / 2;
            //double top = (Game_Canvas.ActualHeight - player.ActualHeight) / 2;
            //Canvas.SetTop(player, top);
            //Canvas.SetLeft(player, left);

            //this.Left = System.Windows.SystemParameters.PrimaryScreenWidth;
            //this.Top = System.Windows.SystemParameters.PrimaryScreenHeight;
            //wHeight = frmGame.Height;
            //wWidth = frmGame.Width;
            //var pos = GetMousePos(frmGame, wWidth, wHeight);
            //var angle = GetAngle(pos);
            //RotateTransform rotateTransform = new RotateTransform(angle, player.Width / 2, player.Height / 2);
            //player.RenderTransform = rotateTransform;

            //if (moveLeft && Canvas.GetLeft(player.visual) > 0)
            //{
            //    Canvas.SetLeft(player.visual, Canvas.GetLeft(player.visual) - 10);
            //    Canvas.SetLeft(player.hitbox, Canvas.GetLeft(player.hitbox) - 10);
            //}
            //if (moveRight && Canvas.GetLeft(player.visual) + player.visual.Width < Application.Current.MainWindow.Width)
            //{
            //    Canvas.SetLeft(player.visual, Canvas.GetLeft(player.visual) + 10);
            //}
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

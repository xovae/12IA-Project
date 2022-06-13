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
    public partial class MainWindow : Window
    {
        //list for removing items (i.e killed enemies, bullets)
        List<Rectangle> itemstoreremove = new List<Rectangle>();
        //random number generation
        Random rand = new Random();


        double[] boundaries = new double[2] {Convert.ToDouble(SystemParameters.PrimaryScreenWidth), Convert.ToDouble(SystemParameters.PrimaryScreenHeight)};

        //int enemySpriteCounter; // int to help change enemy images
        //int enemyCounter = 100; // enemy spawn time
        //int limit = 50; // limit of enemy spawns                        //REPLACE WITH OWN cODe
        //int score = 0; // default score
        //int damage = 0; // default damage

        double wHeight, wWidth;
        Rect playerHitBox; //hitbox to check for collision

        SoundPlayer playSoundtrack = new SoundPlayer(Properties.Resources.Cubic_Planets1);


        public MainWindow()
        {

            InitializeComponent();
            InitializeAnimation();
            boundaries[0] = Game_Canvas.Height;
            boundaries[1] = Game_Canvas.Width;

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += GameEngine;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 5);
            dispatcherTimer.Start();
            Game_Canvas.Focus();

            ImageBrush playerImage = new ImageBrush();   // make a player image, image brush
            // load the player image into it
            playerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/black arrow.png"));
            // assign the player to the player rectangle fill
            player.Fill = playerImage;

            playSoundtrack.PlayLooping();
        }

        private void InitializeAnimation()
        {
            var menuScroll = new DoubleAnimation
            {
                From = -0,
                To = -1080,
                Duration = TimeSpan.FromSeconds(15),
                RepeatBehavior = RepeatBehavior.Forever
            };
            menuScroll.AutoReverse = true;
            imgBackground.BeginAnimation(Canvas.LeftProperty, menuScroll);
        }

        private void Key_Down(object sender, KeyEventArgs e)
        {

        }

        private void Key_Up(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                var pos = GetMousePos(frmGame, wWidth, wHeight);
                var angle = GetAngle(pos);
                Rectangle newBullet = new Rectangle
                {
                    Tag = "bullet",
                    Height = 20,
                    Width = 5,
                    Fill = Brushes.AliceBlue,
                    Stroke = Brushes.Red
                };

            }

            //if (e.Key == Key.Space)
            //{
            //    Rectangle newBullet = new Rectangle
            //    {
            //        Tag = "bullet",
            //        Height = 20,
            //        Width = 5,
            //        Fill = Brushes.White,
            //        Stroke = Brushes.Red                                        //placeholder code for bullet creation, must change for rotation logic
            //    };

            //    // place the bullet on top of the player location
            //    Canvas.SetTop(newBullet, Canvas.GetTop(player) - newBullet.Height);
            //    // place the bullet middle of the player image
            //    Canvas.SetLeft(newBullet, Canvas.GetLeft(player) + player.Width / 2);
            //    // add the bullet to the screen
            //    Game_Canvas.Children.Add(newBullet);
            //}
        }

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GameEngine(object sender, EventArgs e)
        {
            double left = (Game_Canvas.ActualWidth - player.ActualWidth) / 2;
            double top = (Game_Canvas.ActualHeight - player.ActualHeight) / 2;
            Canvas.SetTop(player, top);
            Canvas.SetLeft(player, left);

            this.Left = System.Windows.SystemParameters.PrimaryScreenWidth;
            this.Top = System.Windows.SystemParameters.PrimaryScreenHeight;
            wHeight = frmGame.Height;
            wWidth = frmGame.Width;
            var pos = GetMousePos(frmGame, wWidth, wHeight);
            var angle = GetAngle(pos);
            RotateTransform rotateTransform = new RotateTransform(angle, player.Width/2, player.Height/2);
            player.RenderTransform = rotateTransform;


            //playerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);
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

        static Point GetMousePos(Window back, double w, double h)
        {
            Point mousePos = Mouse.GetPosition(back);
            var ofbackX = w / 2 * -1;
            var ofbackY = h / 2 * -1;

            mousePos.Offset(ofbackX, ofbackY);
            return mousePos;

        }
    }
}

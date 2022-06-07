﻿using System;
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
        //declaring booleans for moving left and right
        //bool moveLeft, moveRight, moveUp, moveDown;
        //list for removing items (i.e killed enemies, bullets)
        List<Rectangle> itemstoreremove = new List<Rectangle>();
        //random number generation
        Random rand = new Random();



        double[] boundaries = new double[4] { 0, 0, 1920, 1080 };


        //int enemySpriteCounter; // int to help change enemy images
        //int enemyCounter = 100; // enemy spawn time
        int playerSpeed = 10; // player movement speed
        //int limit = 50; // limit of enemy spawns                        //REPLACE WITH OWN cODe
        //int score = 0; // default score
        //int damage = 0; // default damage

        double wHeight, wWidth;
        Rect playerHitBox; //hitbox to check for collision


        public MainWindow()
        {


            InitializeComponent();
            boundaries[3] = Game_Canvas.Height;
            boundaries[2] = Game_Canvas.Width;
            //Canvas.SetLeft(player, Canvas.GetLeft(player));


            //if (moveLeft && Canvas.GetLeft(player) > 0)
            //{
            //    Canvas.SetLeft(player, Canvas.GetLeft(player) - playerSpeed);
            //}

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += GameEngine;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 5);
            dispatcherTimer.Start();
            Game_Canvas.Focus();

            ImageBrush background = new ImageBrush();   //make an image brush called background
            //background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/le pac.gif"));   //source of image background, get working with gif
            background.TileMode = TileMode.Tile;        //set the background image to tile (REPLACE WHEN GIF IS MADE)
            background.Viewport = new Rect(0, 0, 0.15, 0.15);   //set height and width of background image brush 
            background.ViewportUnits = BrushMappingMode.RelativeToBoundingBox;      //set background to viewport unit 
            Game_Canvas.Background = background;    //setting the background of the game canvas to the imagebrush background that has been created

            ImageBrush playerImage = new ImageBrush();   // make a player image, image brush
            // load the player image into it
            playerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/black arrow.png"));
            // assign the player to the player rectangle fill
            player.Fill = playerImage;
            //Canvas.SetLeft(player);
            SoundPlayer playSoundtrack = new SoundPlayer(Properties.Resources.Cubic_Planets1);
            Canvas.SetLeft(player/*.Width / 2*/, Convert.ToInt32(Game_Canvas.ActualWidth) / 2);
            Canvas.SetTop(player, Convert.ToInt32(Game_Canvas.ActualHeight / 2));
            //playSoundtrack.Play();
            playSoundtrack.PlayLooping();
        }

        //if (moveLeft && Canvas.GetLeft(player) > 0)
        //{
        //    Canvas.SetLeft(player, Canvas.GetLeft(player) - playerSpeed);
        //}

        private void Soundtrack(object sender, RoutedEventArgs e)
        {
           
        }


        private void Key_Down(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Left)      //if user is pressing the left key, set moveLeft to true
            //{
            //    moveLeft = true;
            //}
            //if (e.Key == Key.Right)     //if user is pressing the right key, set moveLeft to true
            //{
            //    moveRight = true;
            //}
            //if (e.Key == Key.Up)
            //{
            //    moveUp = true;
            //}
            //if (e.Key == Key.Down)
            //{
            //    moveDown = true;
            //}
        }

        private void Key_Up(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Left)      //if user has released the left key, set moveLeft to false
            //{
            //    moveLeft = false;
            //}
            //if (e.Key == Key.Right)     //if user has released the left key, set moveRight to false
            //{
            //    moveRight = false;
            //}
            //if (e.Key == Key.Up)
            //{
            //    moveUp = false;
            //}
            //if (e.Key == Key.Down)
            //{
            //    moveDown = false;
            //}
            if (e.Key == Key.Space)
            {
                Rectangle newBullet = new Rectangle
                {
                    Tag = "bullet",
                    Height = 20,
                    Width = 5,
                    Fill = Brushes.White,
                    Stroke = Brushes.Red                                        //placeholder code for bullet creation, must change for rotation logic
                };

                // place the bullet on top of the player location
                Canvas.SetTop(newBullet, Canvas.GetTop(player) - newBullet.Height);
                // place the bullet middle of the player image
                Canvas.SetLeft(newBullet, Canvas.GetLeft(player) + player.Width / 2);
                // add the bullet to the screen
                Game_Canvas.Children.Add(newBullet);
            }
        }

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMusic_Click(object sender, RoutedEventArgs e)
        {
            //MediaPlayer playSoundtrack = new MediaPlayer();
            //var uri = new Uri("pack://siteoforigin:,,,/sound/Cubic_Planets.wav");
            //playSoundtrack.Open(uri);
            //playSoundtrack.Play();
            SoundPlayer playSoundtrack = new SoundPlayer(Properties.Resources.Cubic_Planets1);
            playSoundtrack.Play();

            playSoundtrack.LoadAsync();
            //playSoundtrack.PlayLooping();
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

            //if (moveLeft && Canvas.GetLeft(player) > 0)
            //{
            //    Canvas.SetLeft(player, Canvas.GetLeft(player) - playerSpeed);
            //}
            //if (moveRight && Canvas.GetLeft(player) + player.Width < Application.Current.MainWindow.Width)
            //{
            //    Canvas.SetLeft(player, Canvas.GetLeft(player) + playerSpeed);
            //}
            //if (moveUp /*&& Canvas.GetTop(player)*/)
            //{
            //    Canvas.SetTop(player, Canvas.GetTop(player) - playerSpeed);
            //}
            //if (moveDown == true)
            //{
            //    Canvas.SetTop(player, Canvas.GetTop(player) + playerSpeed);
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

        static Point GetMousePos(Window back, double w, double h)
        {
            Point mousePos = Mouse.GetPosition(back);
            var ofbackX = w / 2 * -1;
            var ofbackY = h / 2 * -1;

            mousePos.Offset(ofbackX, ofbackY);
            return mousePos;

        }


        //private void MakeEnemies()
        //{
        //    GC.Collect();

        //}



    }
}

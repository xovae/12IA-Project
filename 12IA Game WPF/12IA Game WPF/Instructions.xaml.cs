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
using System.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace _12IA_Game_WPF
{
    /// <summary>
    /// Interaction logic for Instructions.xaml
    /// </summary>
    public partial class Instructions : Window
    {
        public Key UpControl = Key.W, DownControl = Key.S, LeftControl = Key.A, RightControl = Key.D;
        public string ShootControl, RecallControl, difficulty;
        public bool UpBinding, DownBinding, LeftBinding, RightBinding;
        public SolidColorBrush white = new SolidColorBrush(Colors.White);
        public SolidColorBrush transparent = new SolidColorBrush(Colors.Transparent);

        readonly SoundPlayer playSoundtrack = new SoundPlayer(Properties.Resources.Red_Champagne);
        readonly DispatcherTimer tmrHighlight = new DispatcherTimer();


        public Instructions()
        {
            InitializeComponent();
            InitializeAnimation();

            playSoundtrack.PlayLooping();

            if (ShootControl == null)
            {
                ShootControl = "LeftMouse";
                RecallControl = "RightMouse";
            }

            tmrHighlight.Tick += Enable;
            tmrHighlight.Interval = new TimeSpan(0, 0, 0, 0, 5);
            tmrHighlight.Start();
        }

        private void InitializeAnimation()
        {
            var menuScroll = new DoubleAnimation
            {
                From = -0,
                To = -1080,
                Duration = TimeSpan.FromSeconds(15),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true
            };
            
            imgBackground.Width = SystemParameters.PrimaryScreenWidth * 2;
            imgBackground.BeginAnimation(Canvas.LeftProperty, menuScroll);
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
            text.Background = white;
        }

        private void Dehighlight(TextBlock text)
        {
            text.Background = transparent;
        }

        private void Enable(object sender, EventArgs e)
        {
            while (UpBinding == true)
            {
                txtUpSet.Background = white;
            }
            while (DownBinding == true)
            {
                txtDownSet.Background = white;
            }
            while (LeftBinding == true)
            {
                txtLeftSet.Background = white;
            }
            while (RightBinding == true)
            {
                txtRightSet.Background = white;
            }
        }

        private void Reset(object sender, MouseButtonEventArgs e)
        {
            UpControl = Key.W; txtUp.Text = $"Up = {UpControl}";
            DownControl = Key.S; txtDown.Text = $"Down = {DownControl}";
            LeftControl = Key.A; txtLeft.Text = $"Left = {LeftControl}";
            RightControl = Key.D; txtRight.Text = $"Right = {RightControl}";
            ShootControl = "LeftMouse"; txtShoot.Text = $"Shoot = Left Click";
            RecallControl = "RightMouse"; txtRecall.Text = $"Recall Bullet = Right Click";
        }

        private void Easy(object sender, MouseButtonEventArgs e)
        {
            difficulty = "Easy";
        }

        private void Medium(object sender, MouseButtonEventArgs e)
        {
            difficulty = "Medium";
        }

        private void Hard(object sender, MouseButtonEventArgs e)
        {
            difficulty = "Hard";
        }

        private void Swap(object sender, MouseButtonEventArgs e)
        {
            if (ShootControl == "LeftMouse")
            {
                ShootControl = "RightMouse"; txtShoot.Text = $"Shoot = Right Click";
                RecallControl = "LeftMouse"; txtRecall.Text = $"Recall Bullet = Left Click";
            }
            else if (ShootControl == "RightMouse")
            {
                ShootControl = "LeftMouse"; txtShoot.Text = $"Shoot = Right Click";
                RecallControl = "RightMouse"; txtRecall.Text = $"Recall Bullet = Left Click";
            }
        }

        private void Play(object sender, MouseButtonEventArgs e)
        {
            loading_screen loading = new loading_screen(UpControl, DownControl, LeftControl, RightControl, ShootControl, RecallControl, difficulty);
            loading.Show();
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (UpBinding == true)
            {
                UpControl = e.Key;
                txtUp.Text = $"Up = {UpControl}";
                UpBinding = false;
            }
            else if (DownBinding == true)
            {
                DownControl = e.Key;
                txtDown.Text = $"Down = {DownControl}";
                DownBinding = false;
            }
            else if (LeftBinding == true)
            {
                LeftControl = e.Key;
                txtLeft.Text = $"Left = {LeftControl}";
                LeftBinding = false;
            }
            else if (RightBinding == true)
            {
                RightControl = e.Key;
                txtRight.Text = $"Right = {RightControl}";
                RightBinding = false;
            }
        }

        private void SetUp(object sender, MouseButtonEventArgs e)
        {
            UpBinding = true;
        }

        private void SetDown(object sender, MouseButtonEventArgs e)
        {
            DownBinding = true;
        }

        private void SetLeft(object sender, MouseButtonEventArgs e)
        {
            LeftBinding = true;
        }

        private void SetRight(object sender, MouseButtonEventArgs e)
        {
            RightBinding = true;
        }
    }
}

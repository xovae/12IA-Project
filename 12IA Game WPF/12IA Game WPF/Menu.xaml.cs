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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Media;

namespace _12IA_Game_WPF
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        readonly SoundPlayer playSoundtrack = new SoundPlayer(Properties.Resources.Tremendous_Feline__1_);

        public Menu()
        {
            InitializeComponent();
            InitializeAnimation();

            cnvMenu.Height = SystemParameters.PrimaryScreenHeight;
            cnvMenu.Width = SystemParameters.PrimaryScreenWidth;

            playSoundtrack.PlayLooping();
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

            var playScroll = new DoubleAnimation()
            {
                From = 540,
                To = 560,
                Duration = TimeSpan.FromSeconds(3),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true
            };

            var creditsScroll = new DoubleAnimation()
            {
                From = 650,
                To = 670,
                Duration = TimeSpan.FromSeconds(2),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true
            };

            txtPlay.BeginAnimation(Canvas.TopProperty, playScroll);
            txtCredits.BeginAnimation(Canvas.TopProperty, creditsScroll);
            imgBackground.BeginAnimation(Canvas.LeftProperty, menuScroll);
        }

        private void Play(object sender, RoutedEventArgs e)
        {
            loading_screen loading = new loading_screen();
            loading.Show();
            this.Close();
        }

        private void Credits(object sender, RoutedEventArgs e)
        {
            Credits credits = new Credits();
            credits.Show();
            this.Close();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

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

namespace _12IA_Game_WPF
{
    /// <summary>
    /// Interaction logic for Credits.xaml
    /// </summary>
    public partial class Credits : Window
    {

        SoundPlayer playSoundtrack = new SoundPlayer(Properties.Resources.Red_Champagne);

        public Credits()
        {
            InitializeComponent();
            InitializeAnimation();

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
         
            var HeadingScroll = new DoubleAnimation()
            {
                From = 226,
                To = 230,
                Duration = TimeSpan.FromSeconds(3),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true
            };

            var CreditsScroll = new DoubleAnimation
            {
                From = 448,
                To = 452,
                Duration = TimeSpan.FromSeconds(3),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true
            };
            txtCredits.BeginAnimation(Canvas.TopProperty, CreditsScroll);
            txtHeading.BeginAnimation(Canvas.TopProperty, HeadingScroll);
            imgBackground.Width = SystemParameters.PrimaryScreenWidth * 2;
            imgBackground.BeginAnimation(Canvas.LeftProperty, menuScroll);
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            Menu MainMenu = new Menu();
            MainMenu.Show();
            this.Close();
        }
    }
}

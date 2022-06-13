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
        double[] boundaries = new double[2] { Convert.ToDouble(SystemParameters.PrimaryScreenWidth), Convert.ToDouble(SystemParameters.PrimaryScreenHeight) };
        SoundPlayer playSoundtrack = new SoundPlayer(Properties.Resources.Illuminating_Bulbs);

        public Menu()
        {
            InitializeComponent();
            InitializeAnimation();

            boundaries[0] = cnvMenu.Height;
            boundaries[1] = cnvMenu.Width;
            
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

            imgBackground.BeginAnimation(Canvas.LeftProperty, menuScroll);
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            loading_screen loading = new loading_screen();
            loading.Show();
            this.Close();
        }

        private void btnCredits_Click(object sender, RoutedEventArgs e)
        {
            Credits credits = new Credits();
            credits.Show();
            this.Close();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

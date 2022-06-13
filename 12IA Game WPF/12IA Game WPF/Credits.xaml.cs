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
                RepeatBehavior = RepeatBehavior.Forever
            };
            menuScroll.AutoReverse = true;
            imgBackground.BeginAnimation(Canvas.LeftProperty, menuScroll);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Menu MainMenu = new Menu();
            MainMenu.Show();
            this.Close();
        }
    }
}

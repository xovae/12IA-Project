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
    /// Interaction logic for Instructions.xaml
    /// </summary>
    public partial class Instructions : Window
    {

        SoundPlayer playSoundtrack = new SoundPlayer(Properties.Resources.Red_Champagne);

        public Instructions()
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

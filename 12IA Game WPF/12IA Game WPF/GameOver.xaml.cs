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
    /// Interaction logic for GameOver.xaml
    /// </summary>
    public partial class GameOver : Window
    {

        SoundPlayer playSoundtrack = new SoundPlayer(Properties.Resources.Red_Champagne);

        public GameOver(int score)
        {
            InitializeComponent();
            InitializeAnimation();

            playSoundtrack.PlayLooping();

            txtSummary.Text = $"Good game! In your efforts, you managed to \n destroy {score} enemy ships!";
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
           
            var resetScroll = new DoubleAnimation
            {
                From = 714,
                To = 724,
                Duration = TimeSpan.FromSeconds(3),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true
            };

            var ExitScroll = new DoubleAnimation
            {
                From = 792,
                To = 800,
                Duration = TimeSpan.FromSeconds(2),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true
            };

            imgBackground.Width = SystemParameters.PrimaryScreenWidth * 2;
            imgBackground.BeginAnimation(Canvas.LeftProperty, menuScroll);

        }
        private void Exit(object sender, MouseButtonEventArgs e)     //exit button
        {
            this.Close();
        }
        private void Reset(object sender, MouseButtonEventArgs e)
        {
            Menu mainMenu = new Menu();
            mainMenu.Show();
            this.Close();
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
            text.Background = new SolidColorBrush(Colors.White);
        }

        private void Dehighlight(TextBlock text)
        {
            text.Background = new SolidColorBrush(Colors.Transparent);
        }
    }
}

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
        readonly SoundPlayer playSoundtrack = new SoundPlayer(Properties.Resources.Red_Champagne);

        public Key UpControl, DownControl, LeftControl, RightControl;
        public string ShootControl, RecallControl, Difficulty;
        public bool noMove;

        public GameOver(int score, Key Up, Key Down, Key Left, Key Right, string Shoot, string Recall, string diff, bool immobile)
        {
            InitializeComponent();
            InitializeAnimation();

            playSoundtrack.PlayLooping();

            txtSummary.Text = $"Good game! In your efforts, you managed to \n destroy {score} enemy ships!";

            UpControl = Up; DownControl = Down; LeftControl = Left; RightControl = Right; ShootControl = Shoot; RecallControl = Recall; Difficulty = diff; noMove = immobile;
        }

        private void InitializeAnimation()
        {
            var BackgroundScroll = new DoubleAnimation
            {
                From = -0,
                To = -1080,
                Duration = TimeSpan.FromSeconds(15),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true
            };
           
            var ResetScroll = new DoubleAnimation
            {
                From = 714,
                To = 724,
                Duration = TimeSpan.FromSeconds(3),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true
            };

            var MenuScroll = new DoubleAnimation
            {
                From = 780,
                To = 790,
                Duration = TimeSpan.FromSeconds(3),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true
            };

            var ExitScroll = new DoubleAnimation
            {
                From = 849,
                To = 857,
                Duration = TimeSpan.FromSeconds(2),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true
            };

            imgBackground.Width = SystemParameters.PrimaryScreenWidth * 2;
            imgBackground.BeginAnimation(Canvas.LeftProperty, BackgroundScroll);
            txtReset.BeginAnimation(Canvas.TopProperty, ResetScroll);
            txtMenu.BeginAnimation(Canvas.TopProperty, MenuScroll);
            txtExit.BeginAnimation(Canvas.TopProperty, ExitScroll);

        }
        private void Exit(object sender, MouseButtonEventArgs e)     //exit button
        {
            this.Close();
        }

        private void Reset(object sender, MouseButtonEventArgs e)
        {
            MainWindow game = new MainWindow(UpControl, DownControl, LeftControl, RightControl, ShootControl, RecallControl, Difficulty, noMove);
            game.Show();
            this.Close();
        }

        private void Menu(object sender, MouseButtonEventArgs e)
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

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
using System.Windows.Threading;

namespace _12IA_Game_WPF
{
    /// <summary>
    /// Interaction logic for loading_screen.xaml
    /// </summary>
    /// 

    public partial class loading_screen : Window
    {
        DispatcherTimer tmrLoading;
        Random rand = new Random();
        int factNumber;
        string[] facts = new string[] {"one", "two", "three" };


        public loading_screen()
        {
            InitializeComponent();
            InitializeAnimation();

            cnvLoading.Height = SystemParameters.PrimaryScreenHeight;
            cnvLoading.Width = SystemParameters.PrimaryScreenWidth;

            tmrLoading = new DispatcherTimer();
            tmrLoading.Tick += Loading;
            tmrLoading.Interval = new TimeSpan(0, 0, 0, 0, 5);
            tmrLoading.Start();
            DispatcherTimer tmrText = new DispatcherTimer();
            tmrText.Tick += Text;
            tmrText.Interval = new TimeSpan(0, 0, 0, 0, 500);
            tmrText.Start();
            DispatcherTimer tmrFacts = new DispatcherTimer();
            tmrFacts.Tick += FactGenerate;
            tmrFacts.Interval = new TimeSpan(0, 0, 0, 1);
            tmrFacts.Start();
            txtFacts.Text  = "Space Game";
            rectLoading.Width = 0;
            lblLoading.Content = "Loading";

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

        public void Text(object sender, EventArgs e)
        {
            lblLoading.Content += ".";
            if (Convert.ToString(lblLoading.Content) == "Loading....")
            {
                lblLoading.Content = "Loading";
            }
        }

        public void Loading(object sender, EventArgs e)
        {
            rectLoading.Width += 3;
            if (rectLoading.Width > 1095)
            {
                MainWindow game = new MainWindow();
                game.Show();
                this.Close();
                tmrLoading.Stop();
            }
        }

        public void FactGenerate(object sender, EventArgs e)
        {
            factNumber = rand.Next(0, facts.Length);

            txtFacts.Text = Convert.ToString(facts[factNumber]);


        }
    }
}

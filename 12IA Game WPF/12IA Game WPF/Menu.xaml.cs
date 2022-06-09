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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _12IA_Game_WPF
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        double[] boundaries = new double[2] { Convert.ToDouble(SystemParameters.PrimaryScreenWidth), Convert.ToDouble(SystemParameters.PrimaryScreenHeight) };
        public Menu()
        {
            InitializeComponent();

            boundaries[0] = cnvMenu.Height;
            boundaries[1] = cnvMenu.Width;

            Canvas.SetLeft(lblTitle, cnvMenu.ActualWidth - lblTitle.ActualWidth / 2);
            Canvas.SetLeft(btnPlay, cnvMenu.ActualWidth - btnPlay.ActualWidth / 2);
            Canvas.SetLeft(btnCredits, cnvMenu.ActualWidth - btnCredits.ActualWidth / 2);


            //double left = (Game_Canvas.ActualWidth - player.ActualWidth) / 2;
            //double top = (Game_Canvas.ActualHeight - player.ActualHeight) / 2;
            //Canvas.SetTop(player, top);
            //Canvas.SetLeft(player, left);

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

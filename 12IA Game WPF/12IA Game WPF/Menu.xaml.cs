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
        public Menu()
        {
            InitializeComponent();
            
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

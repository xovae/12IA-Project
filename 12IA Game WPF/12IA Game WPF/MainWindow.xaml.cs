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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _12IA_Game_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        bool left, right, up, down;

       

        public MainWindow()
        {
            InitializeComponent();
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            //character/*.*/
        }

        private void character_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left) { left = false; }
            if (e.Key == Key.Right) { right = false; }
            if (e.Key == Key.Up) { up = false; }
            if (e.Key == Key.Down) { down = false; }
        }

        private void character_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left) { left = true; }
            if (e.Key == Key.Right) { right= true; }
            if (e.Key == Key.Up) { up = true; }
            if (e.Key == Key.Down) { down = true; }
            if (e.Key == Key.Escape) { this.Close(); }
   
        }
    }
}

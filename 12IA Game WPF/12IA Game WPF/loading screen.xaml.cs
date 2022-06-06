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
using System.Windows.Threading;

namespace _12IA_Game_WPF
{
    /// <summary>
    /// Interaction logic for loading_screen.xaml
    /// </summary>
    /// 


    public partial class loading_screen : Window
    {
        public loading_screen()
        {
            InitializeComponent();

            DispatcherTimer tmrLoading = new DispatcherTimer();
            tmrLoading.Tick += Loading;
            tmrLoading.Interval = new TimeSpan(0, 0, 0, 0, 5);
            tmrLoading.Start();
            DispatcherTimer tmrText = new DispatcherTimer();
            tmrText.Tick += Text;
            tmrText.Interval = new TimeSpan(0, 0, 0, 0, 500);
            tmrText.Start();
            rectLoading.Width = 0;
            lblLoading.Content = "Loading";
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
            ////while (rectLoading.Width > 1095)
            ////{
            //    rectLoading.Width += 5;
            ////}
            //if (rectLoading.Width > 1095)
            //{
            //    MainWindow game = new MainWindow();
            //    game.Show();
            //    this.Close();
                
            //}
           
        }

        //private void Start()
        //{

        //}
    }
}

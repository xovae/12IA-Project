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
using System.Windows.Threading;

namespace _12IA_Game_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //declaring booleans for moving left and right
        bool moveLeft, moveRight;
        //list for removing items (i.e killed enemies, bullets)
        List<Rectangle> itemstoreremove = new List<Rectangle>();
        //random number generation
        Random rand = new Random();


        int enemySpriteCounter; // int to help change enemy images
        int enemyCounter = 100; // enemy spawn time
        int playerSpeed = 10; // player movement speed
        int limit = 50; // limit of enemy spawns                        //REPLACE WITH OWN cODe
        int score = 0; // default score
        int damage = 0; // default damage

        Rect playerHitBox; //hitbox to check for collision


        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            dispatcherTimer.Start();
            Game_Canvas.Focus();

            ImageBrush background = new ImageBrush();
            background.ImageSource = new BitmapImage(new Uri("\\hbhs.local/users/Home/Students/9jboulto/downloads/le pac.gif"));
            background.TileMode = TileMode.Tile;
            background.Viewport = new Rect(0, 0, 0.15, 0.15);
            background.ViewportUnits = BrushMappingMode.RelativeToBoundingBox;
            Game_Canvas.Background = background;
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            //character/*.*/
        }

        private void Key_Down(object sender, KeyEventArgs e)
        {

        }

        private void Key_Up(object sender, KeyEventArgs e)
        {

        }
    }
}

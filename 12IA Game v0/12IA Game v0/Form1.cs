using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _12IA_Game_v0
{
    public partial class frmGame : Form
    {

        bool right; //boolean values used for movement (is user pressing direction keys yes/no)
        bool left;
        bool up;
        bool down;
       

        public frmGame()
        {
            InitializeComponent();
        }

        private void pnlScreen_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tmrMovement_Tick(object sender, EventArgs e)
        {
            //side collision

            //top collision

            //side of screen collision

            //movement

            if (right == true) { player.Left += 5; }
            if (left == true) { player.Left -= 5; }
            if (up == true) { player.Top -= 5; }
            if (down == true) { player.Top += 5; }

        }
        private void frmGame_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right) { right = true; }
            if (e.KeyCode == Keys.Left) { left = true; }
            if (e.KeyCode == Keys.Up) { up = true; }
            if (e.KeyCode == Keys.Down) { down = true; }
            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }

        private void frmGame_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right) { right = false; }
            if (e.KeyCode == Keys.Left) { left = false; }
            if (e.KeyCode == Keys.Up) { up = false; }
            if (e.KeyCode == Keys.Down) { down = false; }
        }

    }
}

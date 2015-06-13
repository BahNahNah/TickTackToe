using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TickTactToe
{
    public partial class NewGameWindow : Form
    {
        Random r = new Random();
        private int sqSquares = 3;
        private int startingPlayer = 0;
        public int SquareRootSqares
        {
            get { return sqSquares; }
        }
        public int Starting
        {
            get { return startingPlayer; }
        }
        
        public NewGameWindow()
        {
            InitializeComponent();
            startingPlayer = r.Next(0, 3);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = trackBar1.Value.ToString();
            sqSquares = (int)trackBar1.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                startingPlayer = r.Next(1, 3);
            else if (radioButton3.Checked)
                startingPlayer = 2;
            else
                startingPlayer = 1;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void NewGameWindow_Load(object sender, EventArgs e)
        {

        }
    }
}

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
    public partial class Form1 : Form
    {
        int _turn = 0;
        SquareData[,] Board;
        int X_Win_SUM, O_Win_SUM;
        int X_Player_num, O_Player_num;
        bool GameOver = false;
        int Turn
        {
            get { return _turn; }
            set
            {
                _turn = value;
                if (_turn == X_Player_num)
                    statusLabel.Text = "X's Turn";
                else if (_turn == O_Player_num)
                    statusLabel.Text = "O's Turn";
                else
                    Turn = X_Player_num;
            }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GenerateBoard(3);
            Turn = X_Player_num;
        }

        void TakeTurn(SquareData sd)
        {
            if (GameOver)
                return;
            if (Board[sd.Column, sd.Row].Value != 0)
                return;
            Board[sd.Column, sd.Row].Value = Turn;
            sd.SetText(pString());
            sd.Enable(false);
            if (gameWon())
            {
                GameOver = true;
                statusLabel.Text = pString() + " Won!";
                return;
            }
            if(isDraw())
            {
                GameOver = true;
                statusLabel.Text = " Draw!";
                return;
            }
            if (Turn == X_Player_num)
                Turn = O_Player_num;
            else
                Turn = X_Player_num;
        }

        bool isDraw()
        {
            int sqSquares = (int)Math.Sqrt(Board.Length);
            for(int col = 0; col < sqSquares; col++)
            {
                for (int row = 0; row < sqSquares; row++)
                {
                    if (Board[col, row].Value == 0)
                        return false;
                }
            }
            return true;
        }


        string pString()
        {
            if (Turn == X_Player_num)
                return "X";
            else if (Turn == O_Player_num)
                return "O";
            return "?";
        }

        bool DrawSquares(SquareData[] sd)
        {
            foreach (SquareData s in sd)
                s.SetCol(Color.Red);
            return true;
        }

        bool gameWon()
        {
            int sqSquares = (int)Math.Sqrt(Board.Length);
            int rowtotal = 0;
            SquareData[] sd = new SquareData[sqSquares];
            //Row
            for (int col = 0; col < sqSquares; col++)
            {
                for (int row = 0; row < sqSquares; row++)
                {
                    sd[row] = Board[col, row];
                    rowtotal += Board[col, row].Value;
                }
                if (rowtotal == X_Win_SUM || rowtotal == O_Win_SUM)
                    return DrawSquares(sd);
                rowtotal = 0;
            }
            int coltotal = 0;
            for (int row = 0; row < sqSquares; row++)
            {
                for (int col = 0; col < sqSquares; col++)
                {
                    sd[col] = Board[col, row];
                    coltotal += Board[col, row].Value;
                }
                if (coltotal == X_Win_SUM || coltotal == O_Win_SUM)
                    return DrawSquares(sd);
                coltotal = 0;
            }
            int diagtotal = 0;
            for (int s = 0; s < sqSquares; s++)
            {
                sd[s] = Board[s, s];
                diagtotal += Board[s, s].Value;
            } 
            if (diagtotal == X_Win_SUM || diagtotal == O_Win_SUM)
                return DrawSquares(sd);
            diagtotal = 0;
            for (int s = 0; s < sqSquares; s++)
            {
                sd[s] = Board[s, (sqSquares - 1) - s];
                diagtotal += Board[s, (sqSquares - 1) - s].Value;
            }
            if (diagtotal == X_Win_SUM || diagtotal == O_Win_SUM)
                return DrawSquares(sd);

            return false;
        }

        void GenerateBoard(int sqNumbers)
        {
            GameOver = false;
            int mathSide = panel1.Height;
            if (panel1.Width < mathSide)
                mathSide = panel1.Width;
            panel1.Height = mathSide;
            panel1.Width = mathSide;
            int squareSize = mathSide / sqNumbers;
            panel1.Controls.Clear();
            Board = new SquareData[sqNumbers, sqNumbers];
            X_Player_num = 1;
            O_Player_num = sqNumbers + 1;
            X_Win_SUM = X_Player_num * sqNumbers;
            O_Win_SUM = O_Player_num * sqNumbers;
            for (int col = 0; col < sqNumbers; col++)
            {
                for (int row = 0; row < sqNumbers; row++)
                {
                   
                    Button b = new Button();
                    SquareData sd= new SquareData(col, row, b);
                    Board[col, row] = sd;
                    b.Tag = sd;
                    b.MouseClick += b_MouseClick;
                    Point p = new Point(squareSize * col, squareSize * row);
                    b.Size = new System.Drawing.Size(squareSize, squareSize);
                    b.Location = p;
                    panel1.Controls.Add(b);
                }
            }
        }

        void b_MouseClick(object sender, MouseEventArgs e)
        {
            SquareData sd = (SquareData)((Button)sender).Tag;
            TakeTurn(sd);
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (NewGameWindow ngw = new NewGameWindow())
            {
                if (ngw.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    GenerateBoard(ngw.SquareRootSqares);
                    if (ngw.Starting == 2)
                        Turn = O_Player_num;
                    else
                        Turn = X_Player_num;
                }
            }
        }
    }
    public class SquareData
    {
        private int _row, _column, vDat;
        private Button _b;
        public int Row
        {
            get { return _row; }
        }
        public int Column
        {
            get { return _column; }
        }
        public int Value
        {
            get { return vDat; }
            set { vDat = value; }
        }
        public SquareData(int col, int row, Button b)
        {
            _row = row;
            _column = col;
            _b = b;
            vDat = 0;
        }
        public void SetText(string t)
        {
            _b.Text = t;
        }
        public void SetCol(Color c)
        {
            _b.BackColor = c;
        }
        public void Enable(bool e)
        {
            _b.Enabled = e;
        }
    }
}

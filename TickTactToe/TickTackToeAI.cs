using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TickTactToe
{
    public delegate void SelectSquareDelegate(SquareData sd);
    class TickTackToeAI
    {
        private int AI_Value, Opponent_Value;
        public event SelectSquareDelegate MakeMoveCall;
        public int OpponentValue
        {
            get{return Opponent_Value;}
            private set {Opponent_Value = value;}
        }
        public int SquareValue
        {
            get{return AI_Value;}
            private set{AI_Value = value;}
        }
        public TickTackToeAI(int aiv, int opv)
        {
            AI_Value = aiv;
            Opponent_Value = opv;
        }
        public void CalculateTurn(SquareData[,] CurrentBoard)
        {
            BestNode(CurrentBoard);
        }

        private int BestNode(SquareData[,] CurrentBoard)
        {
            int BestScore = 0;
            SquareData NodeSquare = null;
            foreach(SquareData sd in CurrentBoard)
            {
                if(sd.Value == 0)
                {
                    if (NodeSquare == null)
                        NodeSquare = sd;
                    int nodeScore = 0;
                    sd.Value = SquareValue;
                    int winStat = BoardWinStatus(CurrentBoard);
                    if (!isDraw(CurrentBoard))
                    {
                        if (winStat == SquareValue)
                            nodeScore += 10;
                        else if (winStat == OpponentValue)
                            nodeScore -= 10;
                        else
                            nodeScore += BestNode(CurrentBoard);
                    }
                    if(nodeScore > BestScore)
                    {
                        BestScore = nodeScore;
                        NodeSquare = sd;
                    }
                    sd.Value = 0;
                }
            }
            if (MakeMoveCall != null)
                MakeMoveCall(NodeSquare);
            return 0;
        }

        bool isDraw(SquareData[,] Board)
        {
            int sqSquares = (int)Math.Sqrt(Board.Length);
            for (int col = 0; col < sqSquares; col++)
            {
                for (int row = 0; row < sqSquares; row++)
                {
                    if (Board[col, row].Value == 0)
                        return false;
                }
            }
            return true;
        }

        int BoardWinStatus(SquareData[,] Board)
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
                if (rowtotal == SquareValue)
                    return SquareValue;
                if (rowtotal == OpponentValue)
                    return OpponentValue;
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
                if (coltotal == SquareValue)
                    return SquareValue;
                if (coltotal == OpponentValue)
                    return OpponentValue;
                coltotal = 0;
            }
            int diagtotal = 0;
            for (int s = 0; s < sqSquares; s++)
            {
                sd[s] = Board[s, s];
                diagtotal += Board[s, s].Value;
            }
            if (diagtotal == SquareValue)
                return SquareValue;
            if (diagtotal == OpponentValue)
                return OpponentValue;
            diagtotal = 0;
            for (int s = 0; s < sqSquares; s++)
            {
                sd[s] = Board[s, (sqSquares - 1) - s];
                diagtotal += Board[s, (sqSquares - 1) - s].Value;
            }
            if (diagtotal == SquareValue)
                return SquareValue;
            if (diagtotal == OpponentValue)
                return OpponentValue;
            return 0;
        }
    }
}

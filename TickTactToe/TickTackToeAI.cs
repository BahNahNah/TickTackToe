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
            SquareData node = InitialNode(CurrentBoard);
            if (MakeMoveCall != null)
                MakeMoveCall(node);
        }

        private SquareData InitialNode(SquareData[,] CurrentBoard)
        {
            int BestScore = 0;
            SquareData NodeSquare = null;
            foreach(SquareData sd in CurrentBoard)
            {
                int nodeScore = 0;
                if(sd.Value == 0)
                {
                    if (NodeSquare == null)
                        NodeSquare = sd;
                    sd.Value = SquareValue;
                    int winStat = BoardWinStatus(CurrentBoard);
                    if (!isDraw(CurrentBoard))
                    {
                        if (winStat == SquareValue)
                        {
                            nodeScore += 10;
                        }
                        else
                        {
                            nodeScore += BestNode(CurrentBoard, false);
                        }

                    }
                    sd.Value = 0;
                }
                if (nodeScore > BestScore)
                {
                    BestScore = nodeScore;
                    NodeSquare = sd;
                }
            }
            return NodeSquare;
        }

        private int BestNode(SquareData[,] CurrentBoard, bool AiTurn)
        {
            int nodeScore = 0;
            int depth = 0;
            foreach (SquareData sd in CurrentBoard)
            {
                depth++;
                if (sd.Value == 0)
                {
                    if (AiTurn)
                    {
                        sd.Value = SquareValue;
                        int winStat = BoardWinStatus(CurrentBoard);
                        if (!isDraw(CurrentBoard))
                        {
                            if (winStat == SquareValue)
                                nodeScore += 10 - depth;
                            else if (winStat == OpponentValue)
                                nodeScore -= 10 - depth;
                            else
                                nodeScore += BestNode(CurrentBoard, false);
                        }
                    }
                    else
                    {
                        sd.Value = OpponentValue;
                        int winStat = BoardWinStatus(CurrentBoard);
                        if (!isDraw(CurrentBoard))
                        {
                            if (winStat == SquareValue)
                                nodeScore += 10 - depth;
                            else if (winStat == OpponentValue)
                                nodeScore -= 10 - depth;
                            else
                                nodeScore += BestNode(CurrentBoard, true);
                        }
                    }
                    sd.Value = 0;
                }
            }
            return nodeScore;
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
            int AiWinVal = sqSquares * sqSquares;
            int oppWin = OpponentValue * sqSquares;
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
                if (rowtotal == AiWinVal)
                    return SquareValue;
                if (rowtotal == oppWin)
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
                if (coltotal == AiWinVal)
                    return SquareValue;
                if (coltotal == oppWin)
                    return OpponentValue;
                coltotal = 0;
            }
            int diagtotal = 0;
            for (int s = 0; s < sqSquares; s++)
            {
                sd[s] = Board[s, s];
                diagtotal += Board[s, s].Value;
            }
            if (diagtotal == AiWinVal)
                return SquareValue;
            if (diagtotal == oppWin)
                return OpponentValue;
            diagtotal = 0;
            for (int s = 0; s < sqSquares; s++)
            {
                sd[s] = Board[s, (sqSquares - 1) - s];
                diagtotal += Board[s, (sqSquares - 1) - s].Value;
            }
            if (diagtotal == AiWinVal)
                return SquareValue;
            if (diagtotal == oppWin)
                return OpponentValue;
            return 0;
        }
    }
}

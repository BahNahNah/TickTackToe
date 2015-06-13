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
        public void CalculateTurn(SquareData[,,] CurrentBoard)
        {

        }
    }
}

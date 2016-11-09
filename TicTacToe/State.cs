using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class State : IComparable<State>
    {
        public int UtilityValue { get; set; }
        public int[] StateConfiguration { get; set; }

        public int CompareTo(State other)
        {
            return this.UtilityValue.CompareTo(other.UtilityValue);
        }
    }
}

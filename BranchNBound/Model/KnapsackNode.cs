using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BranchNBound.Model
{
    internal class KnapsackNode
    {
        internal double Cost { get; set; }
        internal int UpperCost { get; set; }
        internal int Mask { get; set; }
        internal int TotalWeight { get; set; }
        internal int Index { get; set; }
    }
}

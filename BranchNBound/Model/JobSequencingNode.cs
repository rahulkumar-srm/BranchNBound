using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BranchNBound.Model
{
    internal class JobSequencingNode
    {
        internal int Cost { get; set; }
        internal int UpperBound { get; set; }
        internal int Path { get; set; }
        internal int TotalTime { get; set; }
        internal int Index { get; set; }
    }
}

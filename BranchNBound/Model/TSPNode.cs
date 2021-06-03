using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BranchNBound.Model
{
    internal class TSPNode
    {
        internal int[,] ReducedMatrix { get; set; }
        internal int Cost { get; set; }
        internal int Vertex { get; set; }
        internal int Level { get; set; }
        internal List<int> Path { get; set; }
    }
}

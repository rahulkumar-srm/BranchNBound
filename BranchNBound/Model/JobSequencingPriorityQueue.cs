using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BranchNBound.Model
{
    internal class JobSequencingPriorityQueue
    {
        JobSequencingNode[] items;
        int top = 0;

        public JobSequencingPriorityQueue(int size)
        {
            items = new JobSequencingNode[size + 1];
        }

        internal void Push(JobSequencingNode node)
        {
            int i = ++top;

            while (i > 1 && items[i / 2].Cost > node.Cost)
            {
                items[i] = items[i / 2];
                i = i / 2;
            }

            items[i] = node;
        }

        internal JobSequencingNode Pop()
        {
            JobSequencingNode data = items[1];
            items[1] = items[top];
            items[top--] = null;

            int i = 1;
            int j = i * 2;

            while (j <= top)
            {
                if ((j + 1) <= top && items[j + 1].Cost < items[j].Cost)
                {
                    j += 1;
                }

                if (items[j].Cost < items[i].Cost)
                {
                    JobSequencingNode temp = items[j];
                    items[j] = items[i];
                    items[i] = temp;

                    i = j;
                    j = i * 2;
                }
                else
                    break;
            }

            return data;
        }

        internal int Count()
        {
            return top;
        }
    }
}

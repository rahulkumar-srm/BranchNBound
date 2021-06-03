using BranchNBound.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BranchNBound.Helper
{
    internal class JobSequencingProblem
    {
        int size = 4;
        private int[] penality = { 5, 10, 6, 3 };
        private int[] deadline = { 1, 3, 2, 1 };
        private int[] time = { 1, 2, 1, 1 };
        private int upperBound = 0;
        private JobSequencingPriorityQueue jobSequencingPriorityQueue;

        internal void SolveProblem()
        {
            jobSequencingPriorityQueue = new JobSequencingPriorityQueue(size);

            Console.WriteLine($"Penalities to be paid: {SolveJSP()}");
        }

        private int SolveJSP()
        {
            JobSequencingNode rootNode = new JobSequencingNode { Cost = 0, UpperBound = int.MaxValue, Path = 0, Index = -1 };

            jobSequencingPriorityQueue.Push(rootNode);

            while(jobSequencingPriorityQueue.Count() > 0)
            {
                JobSequencingNode minNode = jobSequencingPriorityQueue.Pop();

                for(int i = minNode.Index + 1; i < size; i++)
                {
                    if (minNode.TotalTime + time[i] <= FindMaxDeadLine(minNode.Index, i))
                    {
                        JobSequencingNode childNode = new JobSequencingNode { Index = i, TotalTime = minNode.TotalTime + time[i], Path = minNode.Path | 1 << i };
                        CalculateCost(childNode);

                        if (childNode.Cost <= upperBound)
                        {
                            jobSequencingPriorityQueue.Push(childNode);
                        }
                    }
                }

                if(jobSequencingPriorityQueue.Count() == 0)
                {
                    PrintSolution(minNode.Path);
                    return minNode.UpperBound;
                }
            }

            return 0;
        }

        private void CalculateCost(JobSequencingNode jobSequencingNode)
        {
            for(int i = 0; i < size; i++)
            {
                if((jobSequencingNode.Path & (1 << i)) == 0 && i < jobSequencingNode.Index + 1)
                {
                    jobSequencingNode.Cost += penality[i];
                }

                if((jobSequencingNode.Path & (1 << i)) == 0)
                {
                    jobSequencingNode.UpperBound += penality[i];
                }
            }

            if(jobSequencingNode.UpperBound < upperBound || upperBound == 0)
            {
                upperBound = jobSequencingNode.UpperBound;
            }
        }

        private void PrintSolution(int path)
        {
            Console.Write("\nJobs can be performed: ");

            for(int i = 0; i < size; i++)
            {
                if((path & (1 << i)) > 0)
                {
                    Console.Write($"J{i + 1}  ");
                }
            }

            Console.WriteLine();
        }

        private int FindMaxDeadLine(int a, int b)
        {
            a = a < 0 ? 0 : deadline[a];
            b = b < 0 ? 0 : deadline[b];
            return a > b ? a : b;
        }
    }
}

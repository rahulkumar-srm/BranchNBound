using BranchNBound.Model;
using BranchNBound.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BranchNBound.Helper
{
    internal class TravelingSalesmanProblem
    {
        static int size = 6;
        TSPMinPriorityQueue tSPMinPriorityQueue = new TSPMinPriorityQueue(size + 1);

        int[,] edges = {
            { 0, 0, 0, 0, 0, 0 },
            { 0, int.MaxValue, 20, 30, 10, 11 },
            { 0, 15, int.MaxValue, 16, 4, 2 },
            { 0, 3, 5, int.MaxValue, 2, 4 },
            { 0, 19, 6, 18, int.MaxValue, 3 },
            { 0, 16, 4, 7, 16, int.MaxValue }
        };

        internal void SolveProblem()
        {
            int totalCost = SolveTSP(edges, 1);

            if (totalCost > 0)
            {
                Console.WriteLine($"Total traveling cost : { totalCost }");
            }
            else
            {
                Console.WriteLine("Solution doesn't exist");
            }
        }
        
        private int SolveTSP(int[,] costMatrix, int startVertex)
        {
            List<int> path = new List<int>();
            TSPNode root = CreateNode(costMatrix, path, 0, -1, 1);
            root.Cost = CalculateCost(root.ReducedMatrix);

            tSPMinPriorityQueue.Push(root);

            while(tSPMinPriorityQueue.Count() > 0)
            {
                TSPNode min = tSPMinPriorityQueue.Pop();

                if(min.Path.Count == size - 1)
                {
                    min.Path.Add(startVertex);

                    // print list of cities visited;
                    Console.WriteLine($"\nTravelling Path: { String.Join(" -> ", min.Path) }");

                    // return optimal cost
                    return min.Cost;
                }

                for(int i = 1; i < size; i++)
                {
                    if (min.ReducedMatrix[min.Vertex, i] != int.MaxValue)
                    {
                        TSPNode childNode = CreateNode(min.ReducedMatrix, min.Path, min.Level + 1, min.Vertex, i);
                        childNode.Cost = min.ReducedMatrix[min.Vertex, i] + min.Cost + CalculateCost(childNode.ReducedMatrix);

                        tSPMinPriorityQueue.Push(childNode);
                    }
                }

                min = null;
            } 

            return 0;
        }

        private int ReduceRow(int[,] reducedMatrix)
        {
            int rowReductionCost = 0;

            for (int i = 1; i < size; i++)
            {
                int min = int.MaxValue;

                for (int j = 1; j < size; j++)
                {
                    if (reducedMatrix[i, j] < min)
                    {
                        min = reducedMatrix[i, j];
                    }
                }

                for (int j = 1; j < size; j++)
                {
                    if (reducedMatrix[i, j] != int.MaxValue)
                    {
                        reducedMatrix[i, j] -= min;
                    }
                    
                }

                rowReductionCost += min < int.MaxValue ? min : 0;
            }

            return rowReductionCost;
        }

        private int ReduceColumn(int[,] reducedMatrix)
        {
            int colReductionCost = 0;

            for (int i = 1; i < size; i++)
            {
                int min = int.MaxValue;

                for (int j = 1; j < size; j++)
                {
                    if (reducedMatrix[j, i] < min)
                    {
                        min = reducedMatrix[j, i];
                    }
                }
                for (int j = 1; j < size; j++)
                {
                    if (reducedMatrix[j, i] != int.MaxValue)
                    {
                        reducedMatrix[j, i] -= min;
                    }
                }

                colReductionCost += min < int.MaxValue ? min : 0;
            }

            return colReductionCost;
        }

        private TSPNode CreateNode(int[,] parentMatrix, List<int> path, int level, int parentVertex, int CurrentVertex)
        {
            TSPNode node = new TSPNode { ReducedMatrix = (int[,])parentMatrix.Clone(), Path = path.ToList(), Level = level, Vertex = CurrentVertex };
            node.ReducedMatrix[CurrentVertex, 1] = int.MaxValue;
            node.Path.Add(CurrentVertex);

            for (int k = 1; level != 0 && k < size; k++)
            {
                node.ReducedMatrix[parentVertex, k] = int.MaxValue;
                node.ReducedMatrix[k, CurrentVertex] = int.MaxValue;
            }

            return node;
        }

        private int CalculateCost(int[,] reducedMatrix)
        {
            return ReduceRow(reducedMatrix) + ReduceColumn(reducedMatrix);
        }
    }
}

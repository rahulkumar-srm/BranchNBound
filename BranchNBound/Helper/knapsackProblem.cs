using BranchNBound.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BranchNBound.Helper
{
    internal class knapsackProblem
    {
        private int[] weight;
        private int[] profit;
        private int capacity;
        private int itemCount;
        private int upperBound = 0;
        KnapsackPriorityQueue knapsackPriorityQueue;

        internal void SolveProblem()
        {
            ItemDetails();

            knapsackPriorityQueue = new KnapsackPriorityQueue(itemCount);

            Console.WriteLine($"\nMax profit can be made: {Math.Abs(GetMaxProfit())}");
        }

        private int GetMaxProfit()
        {
            KnapsackNode rootNode = new KnapsackNode { Index = 0, Mask = (1 << (itemCount - 1)) - 1 };
            CalCulateCost(rootNode);
            knapsackPriorityQueue.Push(rootNode);

            while (knapsackPriorityQueue.Count() > 0)
            {
                KnapsackNode minNode = knapsackPriorityQueue.Pop();

                if (minNode.Index == itemCount - 1)
                {
                    IncludedItems(minNode.Mask);

                    return minNode.UpperCost;
                }

                if (minNode.TotalWeight + weight[minNode.Index + 1] <= capacity)
                {
                    int index = minNode.Index + 1;

                    KnapsackNode lChildNode = new KnapsackNode { Index = index, Mask = minNode.Mask ^ (1 << (index - 1)) };
                    lChildNode.TotalWeight = minNode.TotalWeight;
                    CalCulateCost(lChildNode);

                    if(lChildNode.UpperCost <= upperBound)
                    {
                        knapsackPriorityQueue.Push(lChildNode);
                    }

                    KnapsackNode rChildNode = new KnapsackNode { Index = index, Mask = minNode.Mask ^ (0 << (index - 1)) };
                    rChildNode.TotalWeight = minNode.TotalWeight + weight[index];
                    CalCulateCost(rChildNode);

                    if (rChildNode.UpperCost <= upperBound)
                    {
                        knapsackPriorityQueue.Push(rChildNode);
                    }
                }
            }

            return 0;
        }

        private void CalCulateCost(KnapsackNode knapsackNode)
        {
            int remWeight = capacity;
            double cost = 0;
            int upperCost = 0;

            for(int i = 1; i < itemCount; i++)
            {
                if((knapsackNode.Mask & 1 << (i - 1)) > 0)
                {
                    if (remWeight - weight[i] >= 0)
                    {
                        remWeight -= weight[i];
                        cost += profit[i];
                        upperCost += profit[i];
                    }
                    else
                    {
                        double fraction = remWeight / (double)weight[i];
                        cost += (profit[i] * fraction);
                        remWeight -= (int)(weight[i] * fraction);
                    }                    
                }
            }

            knapsackNode.Cost = -cost;
            knapsackNode.UpperCost = -upperCost;

            if(knapsackNode.UpperCost < upperBound)
            {
                upperBound = knapsackNode.UpperCost;
            }
        }

        private void ItemDetails()
        {
            Console.Write("\nEnter the count of items to be inserted : ");
            itemCount = Convert.ToInt32(Console.ReadLine()) + 1;
            Console.WriteLine();

            weight = new int[itemCount];
            profit = new int[itemCount];

            for (int i = 1; i < itemCount; i++)
            {
                Console.Write($"Enter weight of item {i} : ");
                weight[i] = Convert.ToInt32(Console.ReadLine());

                Console.Write($"Enter the profit on the item {i} : ");
                profit[i] = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine();
            }

            Console.Write("\nEnter the capacity of the knapsack : ");
            capacity = Convert.ToInt32(Console.ReadLine());
        }

        private void IncludedItems(int path)
        {
            Console.WriteLine();

            for(int i = 0; i < itemCount - 1 ; i++)
            {
                if((path & (1 << i)) > 0)
                {
                    Console.WriteLine($"Item {i + 1} : Included");
                }
            }
        }
    }
}

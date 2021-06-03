using BranchNBound.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BranchNBound
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine(
                    Environment.NewLine + "1. Job Sequencing with deadline" + //Time Complexity()
                    Environment.NewLine + "2. 0/1 Knapsack Problem" + //Time Complexity()
                    Environment.NewLine + "3. Traveling Salesman Problem" + //Time Complexity()
                    Environment.NewLine + "0. Exit\n"
                );

                Console.Write("Please select an option : ");

                if (!int.TryParse(Console.ReadLine(), out int i))
                {
                    Console.WriteLine(Environment.NewLine + "Input format is not valid. Please try again." + Environment.NewLine);
                }

                if (i == 0)
                {
                    Environment.Exit(0);
                }
                else if (i == 1)
                {
                    JobSequencingProblem jobSequencingProblem = new JobSequencingProblem();
                    jobSequencingProblem.SolveProblem();
                }
                else if(i == 2)
                {
                    knapsackProblem knapsackProblem = new knapsackProblem();
                    knapsackProblem.SolveProblem();
                }
                else if(i == 3)
                {
                    TravelingSalesmanProblem travelingSalesmanProblem = new TravelingSalesmanProblem();
                    travelingSalesmanProblem.SolveProblem();
                }
                else
                {
                    Console.WriteLine("Please select a valid option.");
                }
            }
        }
    }
}

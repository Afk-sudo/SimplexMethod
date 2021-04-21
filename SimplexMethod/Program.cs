using System;

namespace SimplexMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            // int numberVariables, numberRestrictions;
            //
            // Console.WriteLine("Enter number of variables");
            // numberVariables = int.Parse(Console.ReadLine());
            //
            // Console.WriteLine("Enter number of restrictions");
            // numberRestrictions = int.Parse(Console.ReadLine());
            
            double[,] simplexTable;
            
            // simplexTable = GetTable(numberVariables, numberRestrictions);
            
            // simplexTable = new double[,] {
            //     {4, 1, 1, 0, 8},
            //     {-1, 1, 0, 1, 3},
            //     {3, 4, 0, 0, 0},
            // };
            
            simplexTable = new double[,] {
                {1, 2, 1, 0, 0, 4},
                {1, 1, 0, 1, 0, 3},
                {1, 1, 0, 0, 1, 8},
                {3, 4, 0, 0, 0, 0},
            };

            SimplexMethod simplexMethod = new SimplexMethod(simplexTable);
            simplexMethod.Calculate();
        }
        
        private static double[,] GetTable(int numberVariables, int numberRestrictions)
        {
            double[,] simplexTable = new double[numberRestrictions + 1, numberVariables + 1];
            
            Console.WriteLine("First enter restrictions and then target function");
            Console.WriteLine("Input pattern: 1, 3, 4, 5, 6");
            
            for(int i = 0; i < simplexTable.GetLength(0); i++)
            {
                Console.WriteLine($"{i + 1} line");
                string[] inputVariables = Console.ReadLine().Split(",");

                for(int j = 0; j < simplexTable.GetLength(1); j++)
                {
                    simplexTable[i, j] = int.Parse(inputVariables[j]);
                }
            }
            Console.Write("\n");
            return simplexTable;
        }
    }
}
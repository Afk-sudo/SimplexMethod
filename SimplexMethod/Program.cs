using System;

namespace SimplexMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberVariables, numberRestrictions;
            
            Console.WriteLine("Enter number of variables");
            numberVariables = int.Parse(Console.ReadLine());
            
            Console.WriteLine("Enter number of restrictions");
            numberRestrictions = int.Parse(Console.ReadLine());
            
            double[,] simplexTable;
            simplexTable = GetTable(numberVariables, numberRestrictions);
            
            // simplexTable =
            // {
            //     {4, 1, 1, 0, 8},
            //     {-1, 1, 0, 1, 3},
            //     {-3, -4, 0, 0, 0},
            // };

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
    public class SimplexMethod
    {
        public SimplexMethod(double[,] table)
        {
            _table = table;
            _bases = new int[table.GetUpperBound(0)];
        }
        private double[,] _table;
        private int[] _bases;
        
        public void Calculate()
        {
            while(IsEnd() == false)
            {
                EstablishBases();

                int permissiveColumnIndex = DetectedPermissiveColumn();
                int permissiveRowIndex = DetectedPermissiveRow(permissiveColumnIndex);

                for (int i = 0; i < _table.GetLength(0); i++)
                {
                    if(i == permissiveRowIndex)
                        continue;

                    double permissiveFactor = -(_table[i, permissiveColumnIndex] /
                                    _table[permissiveRowIndex, permissiveColumnIndex]);
                    
                    for (int j = 0; j < _table.GetLength(1); j++)
                    {
                        _table[i, j] = _table[i, j] + _table[permissiveRowIndex, j] * permissiveFactor;
                    }
                }
                for (int i = 0; i < _table.GetLength(0); i++)
                {
                    for (int j = 0; j < _table.GetLength(1); j++)
                    {
                        Console.Write($" {_table[i,j]}");
                    }
                    Console.Write("\n");
                }
                Console.Write("\n");
            }
            EstablishBases();

            for (int i = 0; i < _bases.Length; i++)
            {
                Console.WriteLine($"{_bases[i]} variable = {_table[i, _table.GetUpperBound(1)] / _table[i, _bases[i]]}");
            }
            Console.WriteLine($"Max = {_table[_table.GetUpperBound(0), _table.GetUpperBound(1)]}");
        }   
        public bool IsEnd()
        {
            for (int i = 0; i < _table.GetLength(1); i++)
            {
                if (_table[_table.GetUpperBound(0), i] < 0)
                    return false;
            }
            return true;
        }
        public void EstablishBases()
        {
            double[,] reverseTable = AdditionalService.ReverseTwoDimensionalArray(_table);
            for (int i = 0; i < reverseTable.GetLength(0); i++)
            {
                int counterPositiveNumber = 0;
                int lastPositiveIndex = 0;
                
                for (int j = 0; j < reverseTable.GetLength(1); j++)
                {
                    if (reverseTable[i, j] != 0)
                    {
                        counterPositiveNumber++;
                        lastPositiveIndex = j;
                    }
                }
                if (counterPositiveNumber == 1)
                {
                    _bases[lastPositiveIndex] = i;
                }
            }
        }
        public int DetectedPermissiveColumn()
        {
            int columnIndex = 0;
            double minimal = double.PositiveInfinity;
            
            for (int i = 0; i < _table.GetUpperBound(1); i++)
            {
                if (_table[_table.GetUpperBound(0), i] < minimal)
                {
                    minimal = _table[_table.GetUpperBound(0), i];
                    columnIndex = i;
                }
            }
            return columnIndex;
        }
        public int DetectedPermissiveRow(int permissiveColumn)
        {
            int rowIndex = 0;
            double minimal = double.PositiveInfinity;
            
            for (int i = 0; i < _table.GetUpperBound(0); i++)
            {
                double divisionResult = _table[i, 4] / _table[i, permissiveColumn];
                if(divisionResult < 0)
                    continue;
                if (divisionResult < minimal)
                {
                    minimal = divisionResult;
                    rowIndex = i;
                }
            }
            return rowIndex;
        }
    }
    public static class AdditionalService
    {
        public static double[,] ReverseTwoDimensionalArray(double[,] array)
        {
            double[,] reverseArray = new double[array.GetLength(1), array.GetLength(0)];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    reverseArray[j, i] = array[i, j];
                }
            }
            return reverseArray;
        }
    }
}
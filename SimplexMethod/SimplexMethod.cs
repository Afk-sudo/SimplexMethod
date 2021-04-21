using System;

namespace SimplexMethod
{
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
            ConverObjectiveFunction();
            while(IsEnd() == false)
            {
                EstablishBases();

                int permissiveColumnIndex = DetectedPermissiveColumn();
                int permissiveRowIndex = DetectedPermissiveRow(permissiveColumnIndex);
                double permissiveElement = _table[permissiveRowIndex, permissiveColumnIndex];

                for (int i = 0; i < _table.GetLength(0); i++)
                {
                    if (i == permissiveRowIndex)
                    { 
                        continue;
                    }

                    double permissiveFactor = -(_table[i, permissiveColumnIndex] / permissiveElement);
                    
                    for (int j = 0; j < _table.GetLength(1); j++)
                    {
                        _table[i, j] = _table[i, j] + _table[permissiveRowIndex, j] * permissiveFactor;
                    }
                }
                for (int j = 0; j < _table.GetLength(1); j++)
                {
                    _table[permissiveRowIndex, j] = _table[permissiveRowIndex, j]  / permissiveElement;
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
        private bool IsEnd()
        {
            for (int i = 0; i < _table.GetLength(1); i++)
            {
                if (_table[_table.GetUpperBound(0), i] < 0)
                    return false;
            }
            return true;
        }
        private void EstablishBases()
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
        private int DetectedPermissiveColumn()
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
        private int DetectedPermissiveRow(int permissiveColumn)
        {
            int rowIndex = 0;
            double minimal = double.PositiveInfinity;
            
            for (int i = 0; i < _table.GetUpperBound(0); i++)
            {
                double divisionResult = _table[i, _table.GetUpperBound(1)] / _table[i, permissiveColumn];
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

        private void ConverObjectiveFunction()
        {
            for (int i = 0; i < _table.GetLength(1); i++)
            {
                _table[_table.GetUpperBound(0), i] = -_table[_table.GetUpperBound(0), i];
            }
        }
    }
}
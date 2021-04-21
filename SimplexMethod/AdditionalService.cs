namespace SimplexMethod
{
    public class AdditionalService
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
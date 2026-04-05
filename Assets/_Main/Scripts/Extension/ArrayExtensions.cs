using System;

public static class ArrayExtensions
{
    public static void Shuffle<T>(this T[] array)
    {
        Random rng = new Random();
        int n = array.Length;
        for (int i = n - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            
            (array[i], array[j]) = (array[j], array[i]);
        }
    }
}
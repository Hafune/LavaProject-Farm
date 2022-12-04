using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lib
{
    public class MyFunctions
    {
        public static void ForEachDimensions(int[] dimensions, Action<int[]> callback)
        {
            var args = new int[dimensions.Length];
            RecursiveCallback(dimensions, args, 0, callback);
        }

        private static void RecursiveCallback(IReadOnlyList<int> dimensions, int[] array, int index, Action<int[]> callback)
        {
            for (int i = 0; i < dimensions[index]; i++)
            {
                array[index] = i;
                
                if (index < array.Length - 1)
                    RecursiveCallback(dimensions, array, index + 1, callback);
                else
                    callback.Invoke(array);
            }
        }
        
        public static void RepeatTimesIndexed(int t0, int t1, int t2, Action<int, int, int> callback)
        {
            for (int _t0 = 0; _t0 < t0; _t0++)
            for (int _t1 = 0; _t1 < t1; _t1++)
            for (int _t2 = 0; _t2 < t2; _t2++)
                callback.Invoke(_t0, _t1, _t2);
        }
    }
}
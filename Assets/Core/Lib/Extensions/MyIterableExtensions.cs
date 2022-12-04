using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Lib
{
    public static class MyIterableExtensions
    {
        private static Random random = new();

        public static T[,] InitializeWIth<T>(this T[,] array, Func<T> initFunc)
        {
            for (int t0 = 0; t0 < array.GetLength(0); t0++)
            for (int t1 = 0; t1 < array.GetLength(1); t1++)
                array[t0, t1] = initFunc.Invoke();

            return array;
        }

        public static T[,,] InitializeWIth<T>(this T[,,] array, Func<T> initFunc)
        {
            for (int t0 = 0; t0 < array.GetLength(0); t0++)
            for (int t1 = 0; t1 < array.GetLength(1); t1++)
            for (int t2 = 0; t2 < array.GetLength(2); t2++)
                array[t0, t1, t2] = initFunc.Invoke();

            return array;
        }

        public static T MinBy<T>(this List<T> list, Func<T, int> compare)
        {
            var value = list[0];
            int? min = null;

            foreach (var item in list)
            {
                int current = compare.Invoke(item);

                if (current >= min)
                    continue;

                value = item;
                min = current;
            }

            return value;
        }

        public static T MaxBy<T>(this List<T> list, Func<T, int> compare)
        {
            var value = list[0];
            int? max = null;

            foreach (var item in list)
            {
                int current = compare.Invoke(item);

                if (current <= max)
                    continue;

                value = item;
                max = current;
            }

            return value;
        }

        public static int SumBy<T>(this List<T> list, Func<T, int> callback) => list.Sum(callback.Invoke);

        public static float SumBy<T>(this List<T> list, Func<T, float> callback) => list.Sum(callback.Invoke);

        public static void Deconstruct<T>(this T[] array, out T t0) => t0 = array[0];

        public static void Deconstruct<T>(this T[] array, out T t0, out T t1)
        {
            t0 = array[0];
            t1 = array[1];
        }

        public static void Deconstruct<T>(this T[] array, out T t0, out T t1, out T t2)
        {
            t0 = array[0];
            t1 = array[1];
            t2 = array[2];
        }

        public static void ForEachIndexed<T>(this List<T> list, Action<T, int> callback)
        {
            for (int i = 0; i < list.Count; i++)
                callback.Invoke(list[i], i);
        }

        public static List<T> Initialize<T>(this List<T> list, int size, Func<T> initFoo)
        {
            for (int i = 0; i < size; i++)
                list.Add(initFoo());

            return list;
        }

        public static bool IsEmpty<T>(this List<T> list) => list.Count == 0;

        public static bool IsNotEmpty<T>(this List<T> list) => list.Count != 0;

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> callback)
        {
            foreach (var item in enumerable)
                callback.Invoke(item);
        }

        public static void ForEachIndexed<T>(this IEnumerable<T> enumerable, Action<T, int> callback)
        {
            int count = 0;

            foreach (var item in enumerable)
                callback.Invoke(item, count++);
        }

        public static T Random<T>(this IList<T> list) => list[random.Next(list.Count)];
    }
}
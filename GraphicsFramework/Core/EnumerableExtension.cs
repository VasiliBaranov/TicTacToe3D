using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics;

namespace GraphicsFramework.Core
{
    /// <summary>
    /// Provides utility and extension methods for lists and enumerables.
    /// </summary>
    public static class EnumerableExtension
    {
        /// <summary>
        /// Gets the range of the numbers from the start to the end with the given step.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="step">The step.</param>
        /// <returns></returns>
        public static List<double> GetRange(double start, double end, double step)
        {
            if (start > end)
            {
                throw new ArgumentOutOfRangeException("start", "Start should be less or equal to end.");
            }

            List<double> range = new List<double>();

            double current = start;

            while (current <= end )
            {
                range.Add(current);
                current += step;
            }

            if (!Number.AlmostEqual(range.Last(), end))
            {
                range.Add(end);
            }

            return range;
        }

        /// <summary>
        /// Determines whether the two lists are almosts equal.
        /// </summary>
        /// <param name="a">A.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        public static bool AlmostEqual(this IList<double> a, IList<double> b)
        {
            if (a.Count != b.Count)
            {
                return false;
            }

            for (int i = 0; i < a.Count; i++)
            {
                if (!Number.AlmostEqual(a[i], b[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Determines whether the specified list contains the approximate value.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// 	<c>true</c> if the specified list contains approximate; otherwise, <c>false</c>.
        /// </returns>
        public static bool ContainsApproximate(this List<double> list, double value)
        {
            return list.Exists(element => Number.AlmostEqual(element, value));
        }

        /// <summary>
        /// Generates the specified value for the given amount of times.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count">The count.</param>
        /// <param name="generator">The generator.</param>
        /// <returns></returns>
        public static List<T> Generate<T>(int count, Func<T> generator)
        {
            List<T> result = new List<T>(count);
            for (int i = 0; i < count; i++)
            {
                result.Add(generator());
            }
            return result;
        }

        /// <summary>
        /// Gets the norm of the difference of two lists.
        /// </summary>
        /// <param name="firstList">The first list.</param>
        /// <param name="secondList">The second list.</param>
        /// <returns></returns>
        public static double GetDifferenceNorm(this IEnumerable<double> firstList, IEnumerable<double> secondList)
        {
            double differenceNorm = firstList.DifferenceWithEach(secondList).Norm();
            return differenceNorm;
        }

        /// <summary>
        /// Gets the vector norm of the specified list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns></returns>
        public static double Norm(this IEnumerable<double> list)
        {
            //return list.Aggregate((norm, value)=>norm + value*value);
            double sumOfSquares = list.SquareEach().Sum();
            return Math.Sqrt(sumOfSquares);
        }

        /// <summary>
        /// Assigns the values and keys from the corresponding collections to the list of key-value pairs.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="keys">The keys.</param>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public static List<KeyValuePair<TKey, TValue>> AssignValues<TKey, TValue>
            (this IEnumerable<TKey> keys, IEnumerable<TValue> values)
        {
            int count = keys.Count();
            List<KeyValuePair<TKey, TValue>> pairs = new List<KeyValuePair<TKey, TValue>>(count);
            //or use inner join by index
            for (int i = 0; i < count; i++)
            {
                pairs.Add(new KeyValuePair<TKey, TValue>(keys.ElementAt(i), values.ElementAt(i)));
            }
            return pairs;
        }

        /// <summary>
        /// Squares each element.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns></returns>
        public static IEnumerable<double> SquareEach(this IEnumerable<double> list)
        {
            //return list.Select(value=>value*value);
            return list.MultiplyByEach(list);
        }

        /// <summary>
        /// Gets the scalar product of the lists.
        /// </summary>
        /// <param name="firstList">The first list.</param>
        /// <param name="secondList">The second list.</param>
        /// <returns></returns>
        public static double GetScalarProduct(this IEnumerable<double> firstList, IEnumerable<double> secondList)
        {
            return firstList.MultiplyByEach(secondList).Sum();
        }

        /// <summary>
        /// Multiplies each element in the first list by the corresponding element in the second list.
        /// </summary>
        /// <param name="firstList">The first list.</param>
        /// <param name="secondList">The second list.</param>
        /// <returns></returns>
        public static IEnumerable<double> MultiplyByEach(this IEnumerable<double> firstList, IEnumerable<double> secondList)
        {
            return firstList.ApplyToEach(secondList, (firstValue, secondValue) => firstValue * secondValue);
        }

        /// <summary>
        /// Divides each element in the first list by the corresponding element in the second list.
        /// </summary>
        /// <param name="firstList">The first list.</param>
        /// <param name="secondList">The second list.</param>
        /// <returns></returns>
        public static IEnumerable<double> DivideByEach(this IEnumerable<double> firstList, IEnumerable<double> secondList)
        {
            return firstList.ApplyToEach(secondList, (firstValue, secondValue) => firstValue / secondValue);
        }

        /// <summary>
        /// Substracts each element from the second list from the corresponding element in the first list.
        /// </summary>
        /// <param name="firstList">The first list.</param>
        /// <param name="secondList">The second list.</param>
        /// <returns></returns>
        public static IEnumerable<double> DifferenceWithEach(this IEnumerable<double> firstList, IEnumerable<double> secondList)
        {
            return firstList.ApplyToEach(secondList, (firstValue, secondValue) => firstValue - secondValue);
        }

        /// <summary>
        /// Adds each element from the second list to the corresponding element in the first list.
        /// </summary>
        /// <param name="firstList">The first list.</param>
        /// <param name="secondList">The second list.</param>
        /// <returns></returns>
        public static IEnumerable<double> SumWithEach(this IEnumerable<double> firstList, IEnumerable<double> secondList)
        {
            return firstList.ApplyToEach(secondList, (firstValue, secondValue) => firstValue + secondValue);
        }

        /// <summary>
        /// Applies the operation of type T x T -> T to the each pair of corresponding elements from two lists.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="firstList">The first list.</param>
        /// <param name="secondList">The second list.</param>
        /// <param name="operation">The operation.</param>
        /// <returns></returns>
        public static IEnumerable<T> ApplyToEach<T>(this IEnumerable<T> firstList, IEnumerable<T> secondList, Func<T, T, T> operation)
        {
            return firstList.Select((firstValue, index) => operation(firstValue, secondList.ElementAt(index)));
        }
    }
}
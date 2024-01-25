using System;
using System.Collections.Generic;

namespace GraphicsFramework.Core
{
    /// <summary>
    /// Provides utility and extension methods for lists.
    /// </summary>
    public static class ListExtension
    {
        public static List<T> DeepClone<T>(this List<T> source) where T : ICloneable
        {
            if(source==null)
            {
                throw new ArgumentNullException();
            }
            List<T> clone = new List<T>(source.Count);
            foreach (T element in source)
            {
                clone.Add((T)element.Clone());
            }
            return clone;
        }

        public static bool IsNullOrEmpty<T>(this List<T> source)
        {
            return source == null || source.Count == 0;
        }
    }
}

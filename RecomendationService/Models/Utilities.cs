using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationService.Models
{
    public class Utilities
    {
        /// <summary>
        /// Compare the likeness of the first list against the second one
        /// </summary>
        /// <param name="list1"></param>
        /// <param name="list2"></param>
        /// <returns></returns>
        public static float CompareStringLists(List<string> list1, List<string> list2)
        {
            int sum = list1.Select(s => list2.Aggregate("", (likeness, next) => next == s ? likeness += "1" : likeness, res => res.Length)).Sum();
            float i = ( sum * 1.0f) / list1.Count;
            return i;
        }
    }
}

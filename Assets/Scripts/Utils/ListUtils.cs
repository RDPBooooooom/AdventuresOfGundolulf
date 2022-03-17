using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    public static class ListUtils
    {

        /// <summary>
        /// Returns a Random Element form the List
        /// </summary>
        /// <param name="list">List with all items</param>
        /// <typeparam name="T">List Type</typeparam>
        /// <returns>Random element from the list</returns>
        public static T GetRandomElement<T>(List<T> list)
        {
            if (list.Count == 0) return default(T);
            
            Random rand = new Random();
            return list[rand.Next(0, list.Count - 1)];
        }

        /// <summary>
        /// Returns <see cref="numberOfElements"/> elements from <see cref="list"/>
        /// </summary>
        /// <param name="list"></param>
        /// <param name="numberOfElements">Number of elements to return</param>
        /// <param name="duplicatesAllowed">Are duplicate elements from the list allowed</param>
        /// <typeparam name="T">Type of the elements</typeparam>
        /// <returns>List with random elements</returns>
        public static List<T> GetRandomElements<T>(List<T> list, int numberOfElements, bool duplicatesAllowed = false)
        {
            List<T> toReturn = new List<T>();
            List<T> tempList = new List<T>(list);

            if (!duplicatesAllowed && numberOfElements > list.Count) numberOfElements = list.Count;
            
            for (int i = 0; i < numberOfElements; i++)
            {
                T randomElement = GetRandomElement<T>(tempList);
                
                if(!duplicatesAllowed) tempList.Remove(randomElement);

                toReturn.Add(randomElement);
            }

            return toReturn;
        }

        /// <summary>
        /// Based on Fisher-Yates shuffle https://en.wikipedia.org/wiki/Fisher–Yates_shuffle
        /// </summary>
        /// <param name="toShuffle">list to shuffle</param>
        /// <typeparam name="T">type of the elements</typeparam>
        /// <returns>Shuffled list</returns>
        public static List<T> ShuffleList<T>(List<T> toShuffle)
        {
            Random rng = new Random();
            
            int n = toShuffle.Count;
            while (n > 1) 
            {
                int k = rng.Next(n--);
                T temp = toShuffle[n];
                toShuffle[n] = toShuffle[k];
                toShuffle[k] = temp;
            }

            return toShuffle;
        }

    }
}
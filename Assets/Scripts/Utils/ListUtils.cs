using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    public static class ListUtils
    {

        public static T GetRandomElement<T>(List<T> list)
        {
            Random rand = new Random();
            return list[rand.Next(0, list.Count - 1)];
        }

        public static List<T> GetRandomElements<T>(List<T> list, int numberOfElements, bool duplicatesAllowed = false)
        {
            List<T> toReturn = new List<T>();

            for (int i = 0; i < numberOfElements; i++)
            {
                T randomElement = GetRandomElement<T>(list);
                
                if(!duplicatesAllowed) list.Remove(randomElement);

                toReturn.Add(randomElement);
            }

            return toReturn;
        }


    }
}
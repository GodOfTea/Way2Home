using System;
using System.Collections.Generic;

namespace Data.Scripts.Extension
{
    public class DictionaryExtensions
    {
        public static TValue GetRandom<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
        {
            Random random = new Random();
            var keys = new List<TKey>(dictionary.Keys);
            TKey randomKey = keys[random.Next(keys.Count)];
            return dictionary[randomKey];
        }
    }
}
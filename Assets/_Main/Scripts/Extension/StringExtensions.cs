using UnityEngine;

namespace Data.Scripts.Extension
{
    public static class GenericExtensions
    {
        public static string ToJson(this object obj) => JsonUtility.ToJson(obj);
        public static T ToDeserialized<T>(this string json) => JsonUtility.FromJson<T>(json);
    }
}
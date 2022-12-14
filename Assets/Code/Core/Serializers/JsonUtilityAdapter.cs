using UnityEngine;

namespace Code.Core.Serializers
{
    public class JsonUtilityAdapter : ISerializer
    {
        public string ToJson<T>(T data)
        {
            return JsonUtility.ToJson(data);
        }

        public T FromJson<T>(string data)
        {
            return JsonUtility.FromJson<T>(data);
        }
    }
}
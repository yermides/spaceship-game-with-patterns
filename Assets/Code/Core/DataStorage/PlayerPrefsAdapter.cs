using Code.Core.Serializers;
using UnityEngine;

namespace Code.Core.DataStorage
{
    public class PlayerPrefsAdapter : IDataStorage
    {
        private readonly ISerializer _serializer;

        public PlayerPrefsAdapter(ISerializer serializer)
        {
            _serializer = serializer;
        }
        
        public void SetData<T>(T data, string key)
        {
            var jsonData = _serializer.ToJson(data);
            PlayerPrefs.SetString(key, jsonData);
            PlayerPrefs.Save();
        }

        public T GetData<T>(string key)
        {
            var jsonData = PlayerPrefs.GetString(key);
            return _serializer.FromJson<T>(jsonData);
        }
    }
}
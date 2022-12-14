namespace Code.Core.Serializers
{
    public interface ISerializer
    {
        public string ToJson<T>(T data);
        public T FromJson<T>(string data);
    }
}
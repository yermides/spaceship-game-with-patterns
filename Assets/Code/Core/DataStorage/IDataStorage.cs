namespace Code.Core.DataStorage
{
    public interface IDataStorage
    {
        void SetData<T>(T data, string key);
        T GetData<T>(string key);
    }
}
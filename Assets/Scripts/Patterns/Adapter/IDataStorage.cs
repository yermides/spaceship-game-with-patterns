namespace Patterns.Adapter
{
    public interface IDataStorage
    {
        public void SetData<T>(T obj);
        public T GetData<T>();
    }
}
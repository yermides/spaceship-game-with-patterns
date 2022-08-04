namespace Patterns.Adapter
{
    public class PlayerPrefsAdapter : IDataStorage
    {
        public void SetData<T>(T obj)
        {
            throw new System.NotImplementedException();
        }

        public T GetData<T>()
        {
            throw new System.NotImplementedException();
        }
    }
}
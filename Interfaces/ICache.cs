namespace BackendGeneral.Interfaces
{
    public interface ICache
    {
        void Set(string identifier, object item);

        T Get<T>(string expression);

        void Remove(string expression);
    }
}

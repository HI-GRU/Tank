public interface IPooledObject
{
    void InitializeObject();
    void ReturnToPool();
}
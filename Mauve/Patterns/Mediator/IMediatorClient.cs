using System.Threading.Tasks;

namespace Mauve.Patterns
{
    public interface IMediatorClient<T>
    {
        void HandleIncomingData(T data);
        Task HandleIncomingDataAsync(T data);
    }
}

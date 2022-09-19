using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mauve.Patterns
{
    public interface IMediator<T>
    {
        void Send(T data, IMediatorClient<T> recipient);
        void Send(T data, IEnumerable<IMediatorClient<T>> recipients);
        Task SendAsync(T data, IMediatorClient<T> recipients);
        Task SendAsync(T data, IEnumerable<IMediatorClient<T>> recipients);
    }
}

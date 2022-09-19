using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mauve.Patterns
{
    public interface IMediatorClient<T>
    {
        void HandleIncomingData(T data, IMediatorClient<T> sender);
        Task HandleIncomingDataAsync(T data, IMediatorClient<T> sender);
    }
}

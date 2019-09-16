using System;
using System.Threading.Tasks;

namespace ShopInsights.Infrastructure.Services
{
    public class LazyAsync<T> : Lazy<Task<T>>
    {
        public LazyAsync(Func<Task<T>> valueFactory) : base(valueFactory)
        {

        }
    }
}
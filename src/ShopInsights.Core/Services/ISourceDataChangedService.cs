using System.Threading.Tasks;

namespace ShopInsights.Services
{
    public interface ISourceDataChangedService
    {
        Task Added<T>(T item) where T : class;
        Task Updated<T>(T item) where T : class;
        Task Removed<T>(T item) where T : class;
    }
}

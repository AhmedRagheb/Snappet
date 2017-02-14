
namespace Snappet.Models
{
    public class CacheResponse<T>
    {
        public bool IsLoadedFromCache { get; set; }
        public T Obj { get; set; }
    }
}

using Snappet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snappet.Business.Cache
{
    public interface ICacheHelper
    {
        void Add<T>(T o, string key);
        void Clear(string key);
        bool Exists(string key);
        CacheResponse<T> Get<T>(string key);
    }
}

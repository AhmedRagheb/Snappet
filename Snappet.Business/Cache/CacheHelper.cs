using Snappet.Helpers;
using Snappet.Models;
using System;
using System.Web;

namespace Snappet.Business.Cache
{
    public static class CacheHelper<T>
    {

        /// <summary>
        /// Simple cache helper
        /// </summary>
        /// <param name="key">The cache key used to reference the item.</param>
        /// <param name="function">The underlying method that referes to the object to be stored in the cache.</param>
        /// <returns>The item</returns>
        public static CacheResponse<T> Get(string key, Func<T> function)
        {
            var response = new CacheResponse<T>();

            var obj = HttpContext.Current.Cache[key];
            response.IsLoadedFromCache = true;

            if (obj == null)
            {
                response.IsLoadedFromCache = false;
                obj = function.Invoke();
                HttpContext.Current.Cache.Add(key, obj, null, DateTime.Now.AddHours(ConfigurationHelper.CacheExpiresHours), 
                    TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
            }
            response.Obj = (T)obj;
            return response;
        }
    }

   
}
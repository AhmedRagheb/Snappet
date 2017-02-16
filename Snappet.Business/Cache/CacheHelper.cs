using Snappet.Business.Cache;
using Snappet.Helpers;
using Snappet.Models;
using System;
using System.Web;

namespace Snappet.Business.Cache
{
    public class CacheHelper : ICacheHelper
    {
        /// <summary>
        /// Insert value into the cache using
        /// appropriate name/value pairs
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="o">Item to be cached</param>
        /// <param name="key">Name of item</param>
        public void Add<T>(T o, string key)
        {
            // NOTE: Apply expiration parameters as you see fit.
            // In this example, I want an absolute 
            // timeout so changes will always be reflected 
            // at that time. Hence, the NoSlidingExpiration.  
            HttpContext.Current.Cache.Insert(
                key,
                o,
                null,
                DateTime.Now.AddHours(
                    ConfigurationHelper.CacheExpiresHours),
                System.Web.Caching.Cache.NoSlidingExpiration);
        }

        /// <summary>
        /// Remove item from cache 
        /// </summary>
        /// <param name="key">Name of cached item</param>
        public void Clear(string key)
        {
            HttpContext.Current.Cache.Remove(key);
        }

        /// <summary>
        /// Check for item in cache
        /// </summary>
        /// <param name="key">Name of cached item</param>
        /// <returns></returns>
        public bool Exists(string key)
        {
            return HttpContext.Current.Cache[key] != null;
        }

        /// <summary>
        /// Retrieve cached item
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="key">Name of cached item</param>
        /// <returns>Cached item as type</returns>
        public CacheResponse<T> Get<T>(string key)
        {
            var response = new CacheResponse<T>();
            var obj = HttpContext.Current.Cache[key];
            if (obj != null)
            {
                response.IsLoadedFromCache = true;
                response.Obj = (T)HttpContext.Current.Cache[key];
            }

            return response;
        }
    }

   
}
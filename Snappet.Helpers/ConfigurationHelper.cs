using System;
using System.Configuration;

namespace Snappet.Helpers
{
    public static class ConfigurationHelper
    {
        internal static T Get<T>(string appSettingsKey, T defaultValue)
        {
            string text = ConfigurationManager.AppSettings[appSettingsKey];
            if (string.IsNullOrWhiteSpace(text))
                return defaultValue;

            try
            {
                var value = Convert.ChangeType(text, typeof(T));
                return (T)value;
            }
            catch
            {
                return defaultValue;
            }
        }

        static int _cacheExpiresHours = 0;
        public static int CacheExpiresHours
        {
            get
            {
                if (_cacheExpiresHours == 0)
                {
                    _cacheExpiresHours = Get("CacheExpiresHours", 2);
                }
                return _cacheExpiresHours;
            }
        }

        static string _cacheKeyAllData;
        public static string CacheKeyAllData
        {
            get
            {
                if(string.IsNullOrEmpty(_cacheKeyAllData))
                {
                    _cacheKeyAllData = Get("CacheKeyAllData", "AllData");
                }
                return _cacheKeyAllData;
            }
        }

        static string _cacheKeyTodayData;
        public static string CacheKeyTodayData
        {
            get
            {
                if (string.IsNullOrEmpty(_cacheKeyTodayData))
                {
                    _cacheKeyTodayData = Get("CacheKeyTodayData", "TodatData");
                }
                return _cacheKeyTodayData;
            }
        }
    }
}

using System;

namespace Snappet.Helpers
{
    public static class Helpers
    {
        public static double GetTime(this DateTime st)
        {
            DateTime OLDtime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = st.ToUniversalTime() - OLDtime;
            return Math.Floor(diff.TotalMilliseconds);
        }

        public static DateTime GetTodayDate()
        {
            var today = new DateTime(2015, 3, 24, 11, 30, 0);
            return today;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphite
{
    public static class Utility
    {
        private static DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime EpochToDateTime(long epoch)
        {
            return _epoch.AddSeconds(epoch);
        }
    }
}

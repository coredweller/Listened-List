using System.Globalization;
using System.Diagnostics;
using System;

namespace Core.Helpers
{

    public static class Constants
    {
        public static CultureInfo CurrentCulture
        {
            [DebuggerStepThrough]
            get
            {
                return CultureInfo.CurrentCulture;
            }
        }

        public static Func<DateTime> Now = () => DateTime.UtcNow;
    }
}

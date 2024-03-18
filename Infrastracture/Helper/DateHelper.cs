using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Helper
{
    public static class DateHelper
    {
        public static DateTime? GetDate(string date)
        {
            if (DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime formatDate))
            {
                return formatDate;
            }
            else
            {
                return null;
            }
        }
    }
}

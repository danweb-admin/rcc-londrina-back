using System;
namespace RccManager.Service.Helper
{
	public static class Utils
	{
        public static DateTime formatDate(string date)
        {

            date = date.Replace("/", "");
            var year = int.Parse(date.Substring(4));
            var day = int.Parse(date.Substring(0, 2));
            var month = int.Parse(date.Substring(2, 2));

            return new DateTime(year, month, day);
        }

        public static DateTime formaTime(string time)
        {
            time = time.Replace(":", "");
            var hours = int.Parse(time.Substring(0, 2));
            var minutes = int.Parse(time.Substring(2));

            var now = DateTime.Now;

            return new DateTime(now.Year, now.Month, now.Day, hours, minutes, 0);
        }
    }
}


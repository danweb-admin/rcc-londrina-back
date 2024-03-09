using System;
namespace RccManager.Domain.Helpers
{
	public static class Helpers
	{
        public static DateTime DateTimeNow()
        {
            DateTime dateTime = DateTime.UtcNow;
            TimeZoneInfo brasiliaTime = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, brasiliaTime);
        }
    }
}


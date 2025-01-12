namespace Dima.Core.Extensions;

public static class DateTimeExtensions
{
    public static DateTime GetFirstDayOfMonth(this DateTime date)
        => new(date.Year, date.Month, 1);

    public static DateTime GetLastDayOfMonth(this DateTime date)
        => new(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
}

using System.Diagnostics.CodeAnalysis;

namespace ShukujitsuSharp;

public partial class Shukujitsu
{
    public Shukujitsu(DateOnly date, string name)
    {
        Date = date;
        Name = name;
    }

    public DateOnly Date { get; }
    public string Name { get; }

    public static bool IsShukujitsu(DateOnly date)
    {
        EnsureAcceptableRange(date);

        return Dates.Any(d => d.Date == date);
    }


    /// <summary>
    /// 指定された年、月、日が祝日かどうかを判定します。
    /// </summary>
    /// <param name="year">年を表す整数。</param>
    /// <param name="month">月を表す整数。</param>
    /// <param name="day">日を表す整数。</param>
    /// <returns>指定された日付が祝日の場合は true、それ以外の場合は false。</returns>
    public static bool IsShukujitsu(int year, int month, int day)
    {
        return IsShukujitsu(new DateOnly(year, month, day));
    }

    public static bool Find(DateOnly date, [NotNullWhen(true)] out string? name)
    {
        EnsureAcceptableRange(date);

        var shukujitsu = Dates.SingleOrDefault(d => d.Date == date);
        name = shukujitsu?.Name;
        return shukujitsu is not null;
    }

    private static void EnsureAcceptableRange(DateOnly date)
    {
        var min = Dates.MinBy(d => d.Date)?.Date;
        var max = Dates.MaxBy(d => d.Date)?.Date;
        if (date < min || max < date)
        {
            throw new ArgumentOutOfRangeException(nameof(date));
        }
    }
}

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
    /// 指定された日付が祝日（日本の公休日）であるかどうかを判断します。
    /// </summary>
    /// <param name="year">日付の年の部分。</param>
    /// <param name="month">日付の月の部分。</param>
    /// <param name="day">日付の日の部分。</param>
    /// <returns>
    /// 指定された日付が祝日である場合は<c>true</c>。それ以外の場合は<c>false</c>。
    /// </returns>
    public static bool IsShukujitsu(int year, int month, int day)
    {
        if (year < 1 || month < 1 || month > 12 || day < 1 || day > DateTime.DaysInMonth(year, month))
        {
            throw new ArgumentOutOfRangeException(nameof(year), "引数が適切な年月日の形式ではありません。");
        }
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

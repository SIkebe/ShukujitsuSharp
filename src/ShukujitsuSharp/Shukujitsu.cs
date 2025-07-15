using System.Diagnostics.CodeAnalysis;

namespace ShukujitsuSharp;

/// <summary>
/// Represents a Japanese holiday (shukujitsu) with its date and name.
/// </summary>
public partial class Shukujitsu
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Shukujitsu"/> class.
    /// </summary>
    /// <param name="date">The date of the holiday.</param>
    /// <param name="name">The name of the holiday.</param>
    public Shukujitsu(DateOnly date, string name)
    {
        Date = date;
        Name = name;
    }

    /// <summary>
    /// Gets the date of the holiday.
    /// </summary>
    public DateOnly Date { get; }
    
    /// <summary>
    /// Gets the name of the holiday.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Determines whether the specified date is a Japanese holiday.
    /// </summary>
    /// <param name="date">The date to check.</param>
    /// <returns><c>true</c> if the specified date is a Japanese holiday; otherwise, <c>false</c>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the date is outside the supported range.</exception>
    public static bool IsShukujitsu(DateOnly date)
    {
        EnsureAcceptableRange(date);

        return Dates.Any(d => d.Date == date);
    }

    /// <summary>
    /// Searches for a Japanese holiday on the specified date and returns its name.
    /// </summary>
    /// <param name="date">The date to search for.</param>
    /// <param name="name">When this method returns, contains the name of the holiday if found; otherwise, <c>null</c>.</param>
    /// <returns><c>true</c> if a holiday is found on the specified date; otherwise, <c>false</c>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the date is outside the supported range.</exception>
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

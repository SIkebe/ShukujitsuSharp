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

    public static bool IsShukujitsu(DateOnly date) => Dates.Any(d => d.Date == date);

    public static bool Find(DateOnly date, [NotNullWhen(true)] out string? name)
    {
        var shukujitsu =  Dates.SingleOrDefault(d => d.Date == date);
        name = shukujitsu?.Name;
        return shukujitsu is not null;
    }
}

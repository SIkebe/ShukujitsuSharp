using System.Globalization;
using System.Reflection;
using System.Text;
using ShukujitsuSharp;

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

#pragma warning disable CA2000
var client = new HttpClient();
#pragma warning restore CA2000

var bytes = await client.GetByteArrayAsync(new Uri("https://www8.cao.go.jp/chosei/shukujitsu/syukujitsu.csv"));
var csv = Encoding.GetEncoding("shift_jis").GetString(bytes);
var allShukujitsu = csv
    .Split(Environment.NewLine)
    .Skip(1) // Skip header
    .Where(x => !string.IsNullOrEmpty(x)) // Skip the last empty line
    .Select(x => x.Split(","))
    .Select(x => new Shukujitsu(
        DateOnly.Parse(x[0], CultureInfo.InvariantCulture),
        x[1].Replace("\r", "", StringComparison.OrdinalIgnoreCase)
    )); // Remove "CR"

var builder = new StringBuilder();
using var writer = new StringWriter(builder) { NewLine = "\n" };

await writer.WriteLineAsync("""
    // Code generated by ShukujitsuSharp.Generator; DO NOT EDIT.
    namespace ShukujitsuSharp;

    public partial class Shukujitsu
    {
        public static IEnumerable<Shukujitsu> Dates => new List<Shukujitsu>
        {
    """);

foreach (var item in allShukujitsu)
{
    await writer.WriteLineAsync(@$"        new Shukujitsu(new DateOnly({item.Date.Year}, {item.Date.Month}, {item.Date.Day}), ""{item.Name}""),");
}

await writer.WriteLineAsync("""
        };
    }
    """);

var exe = Assembly.GetAssembly(typeof(Program))?.Location!;
var data = Path.Combine(Directory.GetParent(exe)!.Parent!.Parent!.Parent!.Parent!.FullName!, "ShukujitsuSharp/ShukujitsuData.cs");
await File.WriteAllTextAsync(data, builder.ToString());

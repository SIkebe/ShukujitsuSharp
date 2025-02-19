using Xunit;

namespace ShukujitsuSharp.Tests;

public class ShukujitsuSharpTest
{
    [Fact]
    public void Should_Be_Shukujitsu()
    {
        var date = new DateOnly(2021, 1, 1);
        var result = Shukujitsu.IsShukujitsu(date);
        Assert.True(result);
    }

    [Fact]
    public void Should_Not_Be_Shukujitsu()
    {
        var date = new DateOnly(2022, 1, 2);
        var result = Shukujitsu.IsShukujitsu(date);
        Assert.False(result);
    }

    [Fact]
    public void Should_Be_Shukujitsu_With_Year_Month_Day()
    {
        var result = Shukujitsu.IsShukujitsu(2021, 1, 1);
        Assert.True(result);
    }

    [Fact]
    public void Should_Not_Be_Shukujitsu_With_Year_Month_Day()
    {
        var result = Shukujitsu.IsShukujitsu(2022, 1, 2);
        Assert.False(result);
    }

    [Fact]
    public void Should_Find_Shukujitsu()
    {
        var date = new DateOnly(2022, 1, 1);
        var result = Shukujitsu.Find(date, out var name);
        Assert.True(result);
        Assert.Equal("元日", name);
    }

    [Fact]
    public void Should_Not_Find_Shukujitsu()
    {
        var date = new DateOnly(2022, 1, 2);
        var result = Shukujitsu.Find(date, out var name);
        Assert.False(result);
        Assert.Null(name);
    }

    [Fact]
    public void Should_Not_Accept_Undefined_Date()
    {
        var old = new DateOnly(1954, 12, 31);
        var @new = Shukujitsu.Dates.MaxBy(s => s.Date)!.Date.AddDays(1);

        var ex1 = Record.Exception(() => Shukujitsu.IsShukujitsu(old));
        var ex2 = Record.Exception(() => Shukujitsu.IsShukujitsu(@new));
        var ex3 = Record.Exception(() => Shukujitsu.Find(old, out _));
        var ex4 = Record.Exception(() => Shukujitsu.Find(@new, out _));

        Assert.IsType<ArgumentOutOfRangeException>(ex1);
        Assert.IsType<ArgumentOutOfRangeException>(ex2);
        Assert.IsType<ArgumentOutOfRangeException>(ex3);
        Assert.IsType<ArgumentOutOfRangeException>(ex4);
    }

    [Fact]
    public void Should_Throw_Exception_For_Invalid_Date()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Shukujitsu.IsShukujitsu(2021, 2, 20));
        Assert.Throws<ArgumentOutOfRangeException>(() => Shukujitsu.IsShukujitsu(2021, 13, 1));
        Assert.Throws<ArgumentOutOfRangeException>(() => Shukujitsu.IsShukujitsu(0, 1, 1));
    }
}

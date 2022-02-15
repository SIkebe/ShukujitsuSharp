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
}

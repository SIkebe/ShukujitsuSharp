# ShukujitsuSharp
This library is heavily inspired by [soh335 / shukujitsu](https://github.com/soh335/shukujitsu/).

ShukujitsuSharp determines Japanese holiday.  
Holidays are collected from https://www8.cao.go.jp/chosei/shukujitsu/syukujitsu.csv

## Prerequisites
* .NET 6 or later

## How to use this library
```csharp
using ShukujitsuSharp;

if (Shukujitsu.IsShukujitsu(new DateOnly(2022, 1, 1)))
{
    Console.WriteLine("shukujitsu!");
}

if (Shukujitsu.Find(new DateOnly(2022, 1, 1), out var name))
{
    // 2022-01-01 is 元日.
    Console.WriteLine($"2022-01-01 is {name}.");
}
```

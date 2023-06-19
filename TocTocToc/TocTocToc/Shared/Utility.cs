using System;
using System.Linq;

namespace TocTocToc.Shared;

public class Utility
{
    public static string AlphanumericValueRandom(int length)
    {
        var returnValue = "";
        const string alphaNumericCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        var random = new Random();

        for (var i = 0; i < length; i++)
            returnValue = new string(Enumerable.Repeat(alphaNumericCharacters, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());

        return returnValue;
    }

    public static string DateTimeAlphanumericValueRandom(int length)
    {
        var dateTime = DateTime.Now;


        var returnValue = "";
        const string alphaNumericCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        var random = new Random();

        for (var i = 0; i < length; i++)
            returnValue = new string(Enumerable.Repeat(alphaNumericCharacters, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());

        return returnValue + dateTime.DayOfYear + dateTime.Millisecond;
    }
}
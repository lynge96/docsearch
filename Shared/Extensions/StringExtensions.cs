using System;

namespace Shared.Extensions;
public static class StringExtensions
{
    public static string[] QuerySplitter(this string input)
    {
        return input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
    }


}

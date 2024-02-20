using System;
using ConsoleSearch.Model.Settings;

namespace ConsoleSearch.Extensions;

public static class CommandExtensions
{
    public static void CaseSensitiveCommand(this string input)
    {
        // Check if the input starts with "/casesensitive="
        if (input.StartsWith("/casesensitive=", StringComparison.OrdinalIgnoreCase))
        {
            // Extract the value after "="
            var value = input.Substring("/casesensitive=".Length).Trim();

            // Check if the value is "on" or "off" (case-insensitive)
            if (string.Equals(value, "on", StringComparison.OrdinalIgnoreCase))
            {
                AdvancedSettings.IsCaseSensitive = true;

                Console.WriteLine("Inputs are case sensitive.\n");
            }
            else if (string.Equals(value, "off", StringComparison.OrdinalIgnoreCase))
            {
                AdvancedSettings.IsCaseSensitive = false;

                Console.WriteLine("Inputs are not case sensitive.\n");
            }

        }

    }
}


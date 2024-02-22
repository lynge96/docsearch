using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleSearch.Model.Settings;

namespace ConsoleSearch.Extensions;

public static class CommandExtensions
{
    private static readonly Dictionary<string, Action<string>> Commands = new()
    {
        { "/help", HelpCommand },
        { "/casesensitive=", CaseSensitiveCommand },
        { "/timestamp=", TimeStampCommand }
    };

    public static void AdvancedSettingsCommand(this string input)
    {
        foreach (var command in Commands.Where(command => input.StartsWith(command.Key, StringComparison.OrdinalIgnoreCase)))
        {
            command.Value.Invoke(input);
            return;
        }

        Console.WriteLine("Invalid command.\n");
        
    }

    private static void HelpCommand(string input)
    {
        Console.WriteLine("\nAvailable commands: [on/off]");

        foreach (var command in Commands.Skip(1))
        {
            Console.WriteLine($"{command.Key}");
        }

        Console.WriteLine();
    }

    private static void CaseSensitiveCommand(this string input)
    {
        // Extract the value after "="
        var value = input.Substring("/casesensitive=".Length).Trim();

        if (string.Equals(value, "on", StringComparison.OrdinalIgnoreCase) || string.Equals(value, "off", StringComparison.OrdinalIgnoreCase))
        {
            AdvancedSettings.IsCaseSensitive = string.Equals(value, "on", StringComparison.OrdinalIgnoreCase);

            Console.WriteLine($"Inputs are {(AdvancedSettings.IsCaseSensitive ? "case sensitive" : "not case sensitive")}.\n");
        }
        else
        {
            Console.WriteLine("Invalid value. Please use 'on' or 'off'.\n");
        }

    }

    private static void TimeStampCommand(this string input)
    {
        // Extract the value after "="
        var value = input.Substring("/timestamp=".Length).Trim();

        if (string.Equals(value, "on", StringComparison.OrdinalIgnoreCase) || string.Equals(value, "off", StringComparison.OrdinalIgnoreCase))
        {
            AdvancedSettings.ViewTimeStamp = string.Equals(value, "on", StringComparison.OrdinalIgnoreCase);

            Console.WriteLine($"Time stamps for search hits: {(AdvancedSettings.ViewTimeStamp ? "On" : "Off")}.\n");
        }
        else
        {
            Console.WriteLine("Invalid value. Please use 'on' or 'off'.\n");
        }
    }


}


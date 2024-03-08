namespace Application.Extensions;

public static class StringExtension
{
    public static string[] QuerySplitter(this string input)
    {
        if (input == null)
        {
            return Array.Empty<string>();
        }

        return input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
    }
}
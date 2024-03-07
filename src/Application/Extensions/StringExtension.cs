namespace Application.Extensions;

public static class StringExtension
{
    public static string[] QuerySplitter(this string input)
    {
        return input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
    }


}

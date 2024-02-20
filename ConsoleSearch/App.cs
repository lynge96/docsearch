using ConsoleSearch.Model.Settings;
using System;
using ConsoleSearch.Extensions;

namespace ConsoleSearch;

public class App
{
    public void Run()
    {
        var mSearchLogic = new SearchLogic(new Database());

        Console.WriteLine("Console Search\n");
        
        while (true)
        {
            Console.WriteLine("Enter search terms - q for quit");
            var input = Console.ReadLine();

            if (input is "q") break;

            input.AdvancedSettingsCommand();

            var query = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            
            var result = mSearchLogic.Search(query, 10);

            if (result.Ignored.Count > 0) {
                Console.WriteLine($"Ignored: {string.Join(',', result.Ignored)}");
            }

            var idx = 1;
            foreach (var doc in result.DocumentHits) {
                Console.WriteLine($"{idx} : {doc.Document.mUrl} -- contains {doc.NoOfHits} search terms");
                Console.WriteLine($"Index time: {doc.Document.mIdxTime}");
                Console.WriteLine($"Missing: {ArrayAsString(doc.Missing.ToArray())}");
                idx++;
            }
            Console.WriteLine($"Documents: {result.Hits}. Time: {result.TimeUsed.TotalMilliseconds:F2} ms\n");
        }
    }

    string ArrayAsString(string[] s) {
        return s.Length == 0?"[]":$"[{String.Join(',', s)}]";
        //foreach (var str in s)
        //    res += str + ", ";
        //return res.Substring(0, res.Length - 2) + "]";
    }
}


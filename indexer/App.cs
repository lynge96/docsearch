using Shared;
using System;
using System.Collections.Generic;
using System.IO;

namespace Indexer
{
    public class App
    {
        public void Run(){
            Database db = new Database();
            Crawler crawler = new Crawler(db);

            var root = new DirectoryInfo(Paths.FOLDER);

            DateTime start = DateTime.Now;

            crawler.IndexFilesIn(root, new List<string> { ".txt"});        

            TimeSpan used = DateTime.Now - start;
            Console.WriteLine("DONE! used " + used.TotalMilliseconds);

            var all = db.GetAllWords();

            Console.WriteLine($"Indexed {db.GetDocumentCounts()} documents");
            Console.WriteLine($"Number of different words: {all.Count}");

            
            while (true)
            {
                Console.WriteLine("\nEnter the amount of top words by occurrence you want to see:");
                var input = Console.ReadLine();

                if (int.TryParse(input, out var wordCount) && wordCount > 0)
                {
                    Console.WriteLine($"Finding the top {wordCount} words:\n");

                    var topWords = db.GetTopWordCounts(wordCount);

                    Console.WriteLine("<Word, WordId> - Count\n");
                    foreach (var word in topWords)
                    {
                        Console.WriteLine($"<{word.Word}, {word.WordId}> - {word.Count}");
                    }

                    break;
                }

                Console.WriteLine("Invalid input. Please enter a valid positive number.");
            }

        }
    }
}

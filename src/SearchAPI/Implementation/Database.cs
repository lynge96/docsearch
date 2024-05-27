using Core.Models;
using Core.Settings;
using Microsoft.Data.Sqlite;
using SearchAPI.Interfaces;

// Denne fil indeholder en implementering af en databaseklasse til SearchAPI.
// Database-klassen håndterer forbindelse til en SQLite-database og udfører forskellige databaseoperationer,
// såsom at hente dokumenter, søge efter ord og opdatere indstillinger.

namespace Application;

public class Database : IDatabase
{
    private SqliteConnection _connection;

    // Cache til ord og deres tilsvarende ID'er
    private Dictionary<string, int> mWords = null;

    // Constructor der initialiserer databasen og åbner en forbindelse
    public Database()
    {
        var connectionStringBuilder = new SqliteConnectionStringBuilder();

        connectionStringBuilder.DataSource = Environment.GetEnvironmentVariable("DB_Path");


        _connection = new SqliteConnection(connectionStringBuilder.ConnectionString);

        _connection.Open();

    }

    // Udfører en SQL-kommando uden at returnere resultater
    private void Execute(string sql)
    {
        var cmd = _connection.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }


    // Henter dokumenter baseret på en liste af ord-ID'er
    // Returnerer en liste af nøgle-/værdipar, hvor nøglen er dokumentets ID og værdien er antallet af søgeord i dokumentet
    public List<KeyValuePair<int, int>> GetDocuments(List<int> wordIds)
    {
        var res = new List<KeyValuePair<int, int>>();

        /* Example sql statement looking for doc id's that
           contain words with id 2 and 3
        
           SELECT docId, COUNT(wordId) as count
             FROM Occ
            WHERE wordId in (2,3)
         GROUP BY docId
         ORDER BY COUNT(wordId) DESC 
         */

        var sql = "SELECT docId, COUNT(wordId) as count FROM Occ where ";
        sql += "wordId in " + AsString(wordIds) + " GROUP BY docId ";
        sql += "ORDER BY count DESC;";

        var selectCmd = _connection.CreateCommand();
        selectCmd.CommandText = sql;

        using (var reader = selectCmd.ExecuteReader())
        {
            while (reader.Read())
            {
                var docId = reader.GetInt32(0);
                var count = reader.GetInt32(1);

                res.Add(new KeyValuePair<int, int>(docId, count));
            }
        }

        return res;
    }

    // Konverterer en liste af heltal til en kommasepareret streng, indpakket i parenteser
    // Dette bruges til at bygge SQL-forespørgsler
    private string AsString(List<int> x) => $"({string.Join(',', x)})";


    // Henter alle ord fra databasen med deres tilsvarende ID'er
    // Returnerer en ordbog, hvor nøglen er ordet og værdien er ordets ID
    private Dictionary<string, int> GetAllWords(bool caseSensitive = false)
    {
        var res = new Dictionary<string, int>();

        var selectCmd = _connection.CreateCommand();
        selectCmd.CommandText = "SELECT * FROM word";

        using (var reader = selectCmd.ExecuteReader())
        {
            while (reader.Read())
            {
                var id = reader.GetInt32(0);
                var w = reader.GetString(1);

                res.Add(w, id);
            }
        }
        return res;
    }

    // Henter detaljer om dokumenter baseret på en liste af dokument-ID'er
    // Returnerer en liste af BEDocument-objekter med dokumentets detaljer
    public List<BEDocument> GetDocDetails(List<int> docIds)
    {
        List<BEDocument> res = new List<BEDocument>();

        var selectCmd = _connection.CreateCommand();
        selectCmd.CommandText = "SELECT * FROM document where id in " + AsString(docIds);

        using (var reader = selectCmd.ExecuteReader())
        {
            while (reader.Read())
            {
                var id = reader.GetInt32(0);
                var url = reader.GetString(1);
                var idxTime = reader.GetString(2);
                var creationTime = reader.GetString(3);

                res.Add(new BEDocument { mId = id, mUrl = url, mIdxTime = idxTime, mCreationTime = creationTime });
            }
        }
        return res;
    }

    // Returnerer en liste af ord-ID'er der mangler i et specifikt dokument
    // Sammenligner de forventede ord-ID'er med de faktiske ord-ID'er i dokumentet
    public List<int> GetMissing(int docId, List<int> wordIds)
    {
        var sql = "SELECT wordId FROM Occ where ";
        sql += "wordId in " + AsString(wordIds) + " AND docId = " + docId;
        sql += " ORDER BY wordId;";

        var selectCmd = _connection.CreateCommand();
        selectCmd.CommandText = sql;

        List<int> present = new List<int>();

        using (var reader = selectCmd.ExecuteReader())
        {
            while (reader.Read())
            {
                var wordId = reader.GetInt32(0);
                present.Add(wordId);
            }
        }
        var result = new List<int>(wordIds);
        foreach (var w in present)
            result.Remove(w);


        return result;
    }

    // Henter ord baseret på en liste af ord-ID'er
    // Returnerer en liste af ord
    public List<string> WordsFromIds(List<int> wordIds)
    {
        var sql = "SELECT name FROM Word where ";
        sql += "id in " + AsString(wordIds);

        var selectCmd = _connection.CreateCommand();
        selectCmd.CommandText = sql;

        var result = new List<string>();

        using (var reader = selectCmd.ExecuteReader())
        {
            while (reader.Read())
            {
                var wordId = reader.GetString(0);
                result.Add(wordId);
            }
        }
        return result;
    }

    // Henter ord-ID'er baseret på en søgeforespørgsel og returnerer også en liste af ignorerede ord
    // Returnerer en liste af ord-ID'er der matcher søgeforespørgslen, samt en liste af ord der ikke blev fundet
    public List<int> GetWordIds(string[] query, out List<string> outIgnored)
    {
        if (mWords == null)
        {
            mWords = GetAllWords();
        }

        var res = new List<int>();
        var ignored = new List<string>();

        foreach (var aWord in query)
        {
            if (!AdvancedSettings.IsCaseSensitive)
            {
                var matchingWords = mWords.Keys
                    .Where(word => string.Equals(word, aWord, StringComparison.OrdinalIgnoreCase));

                if (matchingWords.Any())
                {
                    // Hvis der er matchende ord, tilføj deres tilsvarende ID'er
                    res.AddRange(matchingWords.Select(word => mWords[word]));
                }
            }
            else if (mWords.ContainsKey(aWord))
            {
                res.Add(mWords[aWord]);
            }
            else
            {
                ignored.Add(aWord);
            }
        }
        outIgnored = ignored;

        return res;
    }
}


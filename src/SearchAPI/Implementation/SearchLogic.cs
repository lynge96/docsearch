using Core.Models;
using Core.Settings;
using SearchAPI.Interfaces;

// Denne fil indeholder implementeringen af SearchLogic-klassen, som udfører søgninger
// i dokumenter baseret på en forespørgsel. SearchLogic bruger en database til at hente
// ord-ID'er, dokumentdetaljer og manglende ord i dokumenter. Den indeholder også logik
// til at generere filstier baseret på brugernavn.

namespace SearchAPI.Services;

public class SearchLogic : ISearchLogic
{
    IDatabase mDatabase;

    // Cache til ord og deres tilsvarende ID'er
    Dictionary<string, int> mWords;

    // Constructor der initialiserer SearchLogic med en database
    public SearchLogic(IDatabase database)
    {
        mDatabase = database;
    }

    // Udfører en søgning efter dokumenter, der indeholder ord fra forespørgslen.
    //Resultatet vil indeholde detaljer om op til maxAmount dokumenter.
    //Derefter konverterer den ordene til deres tilsvarende ID'er ved hjælp af databasen. Den henter derefter dokument-ID'er, der indeholder disse ord, og begrænser antallet af resultater til maxAmount.
    //For hvert dokument hentes detaljer og manglende ord, og resultatet sammensættes til sidst og returneres.
    public SearchResult Search(string[] query, int? maxAmount)
    {
        query = query.Where(word => !word.StartsWith("/")).ToArray();

        List<string> ignored;

        var start = DateTime.Now;

        // Filtrerer forespørgslen for at fjerne ord, der starter med "/"
        var wordIds = mDatabase.GetWordIds(query, out ignored);

        // Udfører søgningen - henter alle dokument-ID'er
        var docIds =  mDatabase.GetDocuments(wordIds);

        // Henter ID'erne for de første maxAmount dokumenter             
        var top = new List<int>();

        // Brug af null-coalescing operatoren for at standardisere til docIds.Count, hvis maxAmount = null
        foreach (var p in docIds.GetRange(0, Math.Min(maxAmount ?? docIds.Count, docIds.Count)))
            top.Add(p.Key);

        // Sammensætter resultatet med alle DocumentHit
        var docresult = new List<DocumentHit>();
        var idx = 0;

        foreach (var doc in mDatabase.GetDocDetails(top))
        {
            // Henter manglende ord i dokumentet
            var missing = mDatabase.WordsFromIds(mDatabase.GetMissing(doc.mId, wordIds));

            // Tilføjer et nyt DocumentHit til resultatet
            docresult.Add(new DocumentHit(doc, docIds[idx++].Value, missing));
        }

        // Returnerer søgningens resultat
        return new SearchResult(query, docIds.Count, docresult, ignored, DateTime.Now - start);
    }

    //Genererer filstien baseret på brugernavn og lokal sti
    //Den bestemmer først en nøgle baseret på brugernavnet og søger derefter efter denne nøgle i den lokale sti.
    //Hvis nøglen findes, genereres den fulde filsti ved at kombinere basePath og den resterende del af den lokale sti. Hvis nøglen ikke findes, kastes en FileNotFoundException.
    public string GetFilePath(string localPath)
    {
        var username = Environment.GetEnvironmentVariable("User");
        var key = "";

        // Bestemmer nøgle baseret på brugernavn
        if (username == "Allen")
        {
            key = "medium1/";
        } else if (username == "Arnold")
        {
            key = "medium2/";
        }
        else
        {
            throw new ArgumentException("Invalid username");
        }

        var index = localPath.IndexOf(key);
            
        if (index >= 0)
        {
            // Genererer filstien ved at kombinere basePath og lokal sti
            string basePath = Environment.GetEnvironmentVariable("FilePath");
            return Path.Combine(basePath, localPath.Substring(index + key.Length));
        }
        else
        {
            throw new FileNotFoundException();
        }
    }
}

using Application.Interfaces;
using Core.Models;

namespace Application;

public class SearchLogic : ISearchLogic
{
    IDatabase mDatabase;

    Dictionary<string, int> mWords;

    public SearchLogic(IDatabase database)
    {
        mDatabase = database;
    }

    /* Perform search of documents containing words from query. The result will
     * contain details about amost maxAmount of documents.
     */
    public SearchResult Search(string[] query, int? maxAmount)
    {
        query = query.Where(word => !word.StartsWith("/")).ToArray();

        List<string> ignored;

        var start = DateTime.Now;

        // Convert words to wordids
        var wordIds = mDatabase.GetWordIds(query, out ignored);

        // perform the search - get all docIds
        var docIds =  mDatabase.GetDocuments(wordIds);

        // get ids for the first maxAmount             
        var top = new List<int>();

        // using ´??´/null-coalescing operator to default to docIds.Count, if maxAmount = null
        foreach (var p in docIds.GetRange(0, Math.Min(maxAmount ?? docIds.Count, docIds.Count)))
            top.Add(p.Key);

        // compose the result.
        // all the documentHit
        var docresult = new List<DocumentHit>();
        var idx = 0;

        foreach (var doc in mDatabase.GetDocDetails(top))
        {
            var missing = mDatabase.WordsFromIds(mDatabase.GetMissing(doc.mId, wordIds));
              
            docresult.Add(new DocumentHit(doc, docIds[idx++].Value, missing));
        }

        return new SearchResult(query, docIds.Count, docresult, ignored, DateTime.Now - start);
    }
}

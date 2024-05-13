using SearchAPI.Interfaces;

namespace SearchAPI.Implementation;

public class DocsReader : IDocsReader
{

    public string CopyContent(string documentPath)
    {
        // var path = " C:\\Users\\lynge\\Desktop\\docsearch\\data\\arnold-j\\_sent_mail\\917.txt";

        string? docContent;

        try
        {
            docContent = File.ReadAllText(documentPath);

            return docContent;
        }
        catch (Exception e)
        {
            throw new Exception("Error while trying to get search result", e);

        }
    }
}

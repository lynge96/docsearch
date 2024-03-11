using Application.Interfaces;

namespace Application.Factory;

public class SearchFactory
{
    public static ISearchService GetSearchLogic()
    {
        return new SearchService(new HttpClient());
    }

}

using Application.Interfaces;
using Application.Services;

namespace Application.Factory;

public class SearchFactory
{
    public static ISearchService GetSearchLogic()
    {
        return new SearchService();
    }

}

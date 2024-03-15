using Application.Interfaces;
using Application.Services;
using Loadbalancer;

namespace Application.Factory;

public class SearchFactory
{
    public static ISearchService GetSearchLogic()
    {
        return new SearchService(new LoadBalancer());
    }

}

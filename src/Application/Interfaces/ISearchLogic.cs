using Core.Models;

namespace Application.Interfaces;

public interface ISearchLogic
{
    SearchResult Search(string[] query, int? maxAmount);


}

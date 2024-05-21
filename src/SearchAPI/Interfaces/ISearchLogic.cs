using Core.Models;

namespace SearchAPI.Interfaces;

public interface ISearchLogic
{
    SearchResult Search(string[] query, int? maxAmount);

    String GetFilePath(string localPath);
}

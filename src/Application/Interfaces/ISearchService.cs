using Core.DTOs;

namespace Application.Interfaces;

public interface ISearchService
{
    public Task<SearchResultDTO?> SearchAsync(string[] query, string username);
    
    public Task<String> GetFileContentAsync(string fileName, string username);
}

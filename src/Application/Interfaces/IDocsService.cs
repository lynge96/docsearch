namespace Application.Interfaces;

public interface IDocsService
{
    public Task<string?> GetDocumentContent(string documentPath);
}

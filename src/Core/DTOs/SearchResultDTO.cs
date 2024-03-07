namespace Core.DTOs;

public class SearchResultDTO
{
    public string[] Query { get; set; }

    public int Hits { get; set; }

    public List<DocumentHitDTO> DocumentHits { get; set; }

    public List<string> Ignored { get; set; }

    public TimeSpan TimeUsed { get; set; }
}
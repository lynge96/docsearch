namespace Core.DTOs;

public class DocumentHitDTO
{
    public BEDocumentDTO Document { get; set; }

    public int NoOfHits { get; set; }

    public List<string> Missing { get; set;  }
}
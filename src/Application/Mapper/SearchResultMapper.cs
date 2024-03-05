using Core.Models;
using Riok.Mapperly.Abstractions;
using Core.DTOs;

namespace Application.Mapper;

[Mapper]
public static partial class SearchResultMapper
{
    public static partial SearchResultDTO SearchResultToDTO(SearchResult searchResult);

}

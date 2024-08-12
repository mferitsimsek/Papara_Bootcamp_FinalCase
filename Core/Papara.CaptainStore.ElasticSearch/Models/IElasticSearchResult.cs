namespace Papara.CaptainStore.ElasticSearch.Models;

public interface IElasticSearchResult
{
    public bool Success { get; }
    public string? Message { get; }
}

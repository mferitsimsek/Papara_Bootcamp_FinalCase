using Nest;

namespace Papara.CaptainStore.ElasticSearch.Models;

public class SearchParameters
{
    public string IndexName { get; set; }
    public int From { get; set; } = 0;
    public int Size { get; set; } = 10;
    public string Query { get; set; } = string.Empty;

    public SearchParameters()
    {
        IndexName = string.Empty;
    }

    public SearchParameters(string indexName, int from, int size,string query)
    {
        IndexName = indexName;
        From = from;
        Size = size;
        Query = query;
    }
}

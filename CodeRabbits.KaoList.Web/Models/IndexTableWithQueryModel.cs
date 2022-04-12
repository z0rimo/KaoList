namespace CodeRabbits.KaoList.Web.Models
{
    public class IndexTableWithQueryModel<T> : IIndexTableMetaData
    {
        public int Index { get; set; }
        public int CountPerPage { get; set; }
        public string? Query { get; set; }
        public IEnumerable<T>? Items { get; set; }
        public int TotalCount => Items?.Count() ?? 0;
    }
}

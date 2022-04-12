namespace CodeRabbits.KaoList.Web.Models
{
    public interface IIndexTableMetaData
    {
        public int Index { get; }
        public int CountPerPage { get; }
        public string? Query { get; }
        public int TotalCount { get; }
    }
}

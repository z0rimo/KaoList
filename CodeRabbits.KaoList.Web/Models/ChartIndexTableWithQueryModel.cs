namespace CodeRabbits.KaoList.Web.Models
{
    public class ChartIndexTableWithQueryModel
    {
        public int Index { get; set; }
        public int Count { get; set; }
        public string? Query { get; set; }
        public IEnumerable<ChartItemModel>? Items { get; set; }
    }
}

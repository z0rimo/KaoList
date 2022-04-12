namespace CodeRabbits.KaoList.Web.Models
{
    public interface IChartLayoutModel
    {
        public IndexTableWithQueryModel<ChartItemModel>? Chart { get; }
    }
}

using CodeRabbits.KaoList.Web.Models;

namespace CodeRabbits.KaoList.Web.Pages.Chart
{
    public class NewChartModel : ChartPage
    {
        public ChartExplainModel? ChartExplain { get; set; } 
        public NewChartModel(IConfiguration configuration) : base(configuration)
        {
            PageName = "신곡차트";            
        }

        public override void OnGet(int? index)
        {
            base.OnGet(index);
            ChartExplain = new ChartExplainModel
            {
                PageName = PageName,
                UpdateDate = DateTime.Now
            };
        }

        protected override IEnumerable<ChartItemModel> GetCharts(string? query)
        {            
            var i = 1;
            return Enumerable.Repeat(_dummyItem, 201)
                             .Select(item => new ChartItemModel
                             {
                                 Artist = item.Artist,
                                 Bookmark = true,
                                 Ky = item.Ky,
                                 Title = item.Title + i++,
                                 Tj = item.Tj,
                                 YouTube = item.YouTube,
                             })
                             .ToArray();
        }
    }
}
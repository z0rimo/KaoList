using CodeRabbits.KaoList.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeRabbits.KaoList.Web.Pages.Chart
{


    public class PopularChartModel : ChartPage
    {
        public PopularChartModel(IConfiguration configuration) : base(configuration)
        {
            PageName = "인기차트";
        }
        public ChartExplainModel? ChartExplain { get; set; }

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
                             .Select(item =>
                             {
                                 var now = i++;
                                 return new ChartItemModel
                                 {
                                     Rank = now,
                                     Artist = item.Artist,
                                     Bookmark = true,
                                     Ky = item.Ky,
                                     Title = item.Title + now,
                                     Tj = item.Tj,
                                     YouTube = item.YouTube,
                                 };
                             })
                             .ToArray();
        }
    }
}

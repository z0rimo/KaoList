using CodeRabbits.KaoList.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeRabbits.KaoList.Web.Pages
{
    public abstract class ChartPage : PageModel, IChartLayoutModel
    {
#warning A dummy code is included.
        protected static ChartItemModel _dummyItem = new()
        {
            Artist = "아티스트",
            Bookmark = true,
            Ky = 12345,
            Title = "신곡 타이틀 ",
            Tj = 13215,
            YouTube = "123"
        };

        [BindProperty(SupportsGet = true)]
        public virtual string? Query { get; set; }
        public int Index { get; set; }
        public int Count { get; set; }
        public string PageName { get; set; }
        public ChartIndexTableWithQueryModel? Chart => new()
        {
            Index = Index,
            Items = GetCharts(Query),
            Count = Count,
        };


        public ChartPage(IConfiguration configuration)
        {
            Count = configuration.GetValue<int>("DefaultChartCount");

        }

        public virtual void OnGet(int? index)
        {
            Index = index ?? 1;
        }

        protected abstract IEnumerable<ChartItemModel> GetCharts(string? query);

    }
}

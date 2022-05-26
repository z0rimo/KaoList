using CodeRabbits.KaoList.Web.Models;

namespace CodeRabbits.KaoList.Web.Pages.Chart
{
    public class BookmarkModel : ChartPage
    {
        public BookmarkModel(IConfiguration configuration) : base(configuration)
        {
            PageName = "북마크";
        }

        protected override IEnumerable<ChartItemModel> GetCharts(string? query)
        {

            var i = 1;
            IEnumerable<ChartItemModel> bookmarks = Enumerable
                .Repeat(_dummyItem, 201)
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

            if (!string.IsNullOrEmpty(query))
            {
                bookmarks = bookmarks.Where(item => item.Title!.Contains(query));
            }

            return bookmarks;
        }

    }
}

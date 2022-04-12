using CodeRabbits.KaoList.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeRabbits.KaoList.Web.Controllers
{
    public class CommunityController : Controller
    {
        public IActionResult Index()
        {
            var model = new IndexTableWithQueryModel<CommunityItemModel>
            {
                Items = Array.Empty<CommunityItemModel>(),
                CountPerPage = 20,
                Index = 0,
            };

            return View(model);
        }
        public IActionResult Detail()
        {
            var model = new IndexTableWithQueryModel<CommunityItemModel>
            {
                Items = Array.Empty<CommunityItemModel>(),
                CountPerPage = 20,
                Index = 0,
            };

            return View(model);
        }
        public IActionResult Write()
        {
            return View();
        }

        public static CommunityItemModel _dummyItem = new()
        {
            Header = "말머리",
            Title = "제목",
            Writer = "글쓴이",
            Viewers = 1227,
            Recommend = 1557,
            Date = DateTime.Today
        };

        public IEnumerable<CommunityItemModel> GetCommunities(string? query)
        {
            var i = 1;
            IEnumerable<CommunityItemModel> CommunityTable = Enumerable
                .Repeat(_dummyItem, 201)
                .Select(item => new CommunityItemModel
                {
                    Header = item.Header,
                    Title = item.Title,
                    Writer = item.Writer,
                    Viewers = item.Viewers + i,
                    Recommend = item.Recommend + i,
                    Date = item.Date
                })
                .ToArray();

            return CommunityTable;
        }
    }
}

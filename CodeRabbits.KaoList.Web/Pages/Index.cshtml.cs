using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeRabbits.KaoList.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IEnumerable<dynamic> UpdateSongs { get; set; } = Array.Empty<dynamic>();
        public IEnumerable<dynamic> PoupularSongs { get; set; } = Array.Empty<dynamic>();

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
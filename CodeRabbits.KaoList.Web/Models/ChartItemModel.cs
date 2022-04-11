using System.ComponentModel;

namespace CodeRabbits.KaoList.Web.Models
{
    public class ChartItemModel
    {
        [DisplayName("순위")]
        public int? Rank { get; set; }
        [DisplayName("유튜브")]
        public string? YouTube { get; set; }
        [DisplayName("곡명")]
        public string? Title { get; set; }
        [DisplayName("아티스트")]
        public string? Artist { get; set; }
        [DisplayName("TJ")]
        public int? Tj { get; set; }
        [DisplayName("KY")]
        public int? Ky { get; set; }
        [DisplayName("북마크")]
        public bool? Bookmark { get; set; }
    }
}

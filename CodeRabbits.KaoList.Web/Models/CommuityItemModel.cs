using System.ComponentModel;

namespace CodeRabbits.KaoList.Web.Models
{
    public class CommunityItemModel
    {
        [DisplayName("말머리")]
        public string? Header { get; set; }
        [DisplayName("제목")]
        public string? Title { get; set; }
        [DisplayName("글쓴이")]
        public string? Writer { get; set; }
        [DisplayName("조회수")]
        public int? Viewers { get; set; }
        [DisplayName("추천")]
        public int? Recommend { get; set; }
        [DisplayName("작성일")]
        public DateTime? Date { get; set; }
    }
}

using Postgrest.Attributes;
using Postgrest.Models;

namespace TriviaWebRazorPages.Models
{
    [Table("question")]
    public class Question : BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("category_id")]
        public int Category_Id { get; set; }

        [Column("text")]
        public string Text { get; set; } = string.Empty;

        [Column("difficulty")]
        public string Difficulty { get; set; } = string.Empty;

        [Column("type")]
        public string Type { get; set; } = string.Empty;

        [Column("answer")]
        public string Answer { get; set; } = string.Empty;

        [Column("explanation")]
        public string? Explanation { get; set; }
    }
}

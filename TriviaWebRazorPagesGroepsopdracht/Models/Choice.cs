using Postgrest.Attributes;
using Postgrest.Models;

namespace TriviaWebRazorPages.Models
{
    [Table("choice")]
    public class Choice : BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("question_id")]
        public int Question_Id { get; set; }

        [Column("text")]
        public string Text { get; set; } = string.Empty;

        [Column("is_correct")]
        public bool Is_Correct { get; set; }
    }
}

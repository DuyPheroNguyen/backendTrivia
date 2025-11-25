using System.ComponentModel.DataAnnotations;

namespace TriviaWebRazorPages.Models
{
    public class TriviaQuestion
    {
        public int Id { get; set; }

        [Required]
        public string Category { get; set; } = string.Empty; // e.g. "Science", "Movies"

        [Required]
        public string Question { get; set; } = string.Empty;

        [Required]
        public string AnswerA { get; set; } = string.Empty;

        [Required]
        public string AnswerB { get; set; } = string.Empty;

        [Required]
        public string AnswerC { get; set; } = string.Empty;

        [Required]
        public string CorrectAnswer { get; set; } = string.Empty; // "A", "B", or "C"
    }
}

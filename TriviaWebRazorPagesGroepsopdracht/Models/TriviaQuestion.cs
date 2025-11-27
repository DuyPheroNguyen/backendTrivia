using System.ComponentModel.DataAnnotations;

namespace TriviaWebRazorPages.Models
{
    public class TriviaQuestion
    {
        public int Id { get; set; } // primary key

        [Required]
        public string Category { get; set; } = string.Empty; // the category of the question

        [Required]
        public string Question { get; set; } = string.Empty; // the question text

        [Required]
        public string AnswerA { get; set; } = string.Empty; // possible answer A

        [Required]
        public string AnswerB { get; set; } = string.Empty; // possible answer B

        [Required]
        public string AnswerC { get; set; } = string.Empty; // possible answer C

        [Required]
        public string CorrectAnswer { get; set; } = string.Empty; // "A", "B", or "C"
    }
}

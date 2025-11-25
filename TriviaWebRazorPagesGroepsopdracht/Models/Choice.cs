namespace TriviaWebRazorPages.Models
{
    public class Choice
    {
        public int Id { get; set; }
        public int Question_Id { get; set; }     // foreign key
        public string Text { get; set; } = string.Empty;         // mogelijk antwoord
        public bool Is_Correct { get; set; }     // true / false

        public Question? Question { get; set; }
    }
}

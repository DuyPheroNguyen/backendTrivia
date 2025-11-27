namespace TriviaWebRazorPages.Models
{
    public class Question
    {
        public int Id { get; set; }                      // primary key
        public int Category_Id { get; set; }        // foreign key
        public string Text { get; set; } = string.Empty;           // de vraag zelf
        public string Difficulty { get; set; } = string.Empty;      // makkelijk, gemiddeld, moeilijk
        public string Type { get; set; } = string.Empty;            // multiple choice of iets anders
        public string Explanation { get; set; } = string.Empty;     // optioneel

        public Category? Category { get; set; }        // de categorie waar deze vraag bij hoort
        public List<Choice>? Choices { get; set; } = new();      // mogelijke antwoorden
    }
}
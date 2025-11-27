using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TriviaWebRazorPages.Data;
using TriviaWebRazorPages.Models;

namespace TriviaWebRazorPages.Pages.Trivia
{
    public class CategoryModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public CategoryModel(ApplicationDbContext db) 
        {
            _db = db;
        }

        [BindProperty(SupportsGet = true)] // zodat we de categorie via query string kunnen ontvangen
        public string? Category { get; set; } // de gekozen categorie naam

        [BindProperty(SupportsGet = true)] // om de huidige vraagindex bij te houden via query string
        public int CurrentQuestionIndex { get; set; } = 0; // standaard op 0 zetten (eerste vraag van elke category gepakt)

        [BindProperty] // om de gekozen antwoordindex te binden bij post
        public int? ChoiceIndex { get; set; } // index van aangeklikte keuze (dus de verschillende genre die wordt geklikt)

        public Question? CurrentQuestion { get; set; }  // de huidige vraag die getoond wordt
        public List<Question> QuestionsInCategory { get; set; } = new(); // alle vragen in de gekozen categorie gepakt
        public string? AnswerResult { get; set; } // resultaat van het antwoord, dus goed/fout is

        public void OnGet() // bij het laden van de pagina
        {
            LoadQuestions(); // laadt ALLE vragen van de gekozen categorie
        }

        public void OnPost() // bij het versturen van een gekozen antwoord
        {
            LoadQuestions(); // laadt ALLE vragen van de gekozen categorie

            if (ChoiceIndex.HasValue && CurrentQuestion != null) // controleer of er een keuze is gemaakt en of er een huidige vraag is
            {
                // ICollection naar List voor index
                var choices = CurrentQuestion.Choices.ToList(); // keuzes van de huidige vraag

                if (ChoiceIndex.Value >= 0 && ChoiceIndex.Value < choices.Count) // controleer of de gekozen index geldig is
                {
                    var selected = choices[ChoiceIndex.Value]; // de gekozen keuze
                    AnswerResult = selected.Is_Correct ? "Correct!" : "Helaas, fout antwoord."; // bepaal of het correct is
                }
            }
        }

        private void LoadQuestions() // laadt ALLE vragen van de gekozen categorie
        {
            if (string.IsNullOrEmpty(Category)) //  controleer of er een categorie is opgegeven
                return;

            // Haal ALLE vragen van de categorie
            QuestionsInCategory = _db.Questions
                .Include(q => q.Choices) // inclusief keuzes
                .Include(q => q.Category) // inclusief categorie
                .Where(q => q.Category.Name == Category) // filter op categorie naam
                .ToList(); // zet om naar lijst

            if (QuestionsInCategory.Any()) // controleer of er vragen zijn in de categorie
            {
                // Gebruik CurrentQuestionIndex om de juiste vraag te tonen
                CurrentQuestion = QuestionsInCategory[CurrentQuestionIndex % QuestionsInCategory.Count];
            }
        }
    }
}

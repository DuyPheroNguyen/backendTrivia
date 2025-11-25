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

        [BindProperty(SupportsGet = true)]
        public string? Category { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentQuestionIndex { get; set; } = 0;

        [BindProperty]
        public int? ChoiceIndex { get; set; }

        public Question? CurrentQuestion { get; set; }
        public List<Question> QuestionsInCategory { get; set; } = new();
        public string? AnswerResult { get; set; }

        public void OnGet()
        {
            LoadQuestions();
        }

        public void OnPost()
        {
            LoadQuestions();

            if (ChoiceIndex.HasValue && CurrentQuestion != null)
            {
                // ICollection naar List voor index
                var choices = CurrentQuestion.Choices.ToList();

                if (ChoiceIndex.Value >= 0 && ChoiceIndex.Value < choices.Count)
                {
                    var selected = choices[ChoiceIndex.Value];
                    AnswerResult = selected.Is_Correct ? "Correct!" : "Helaas, fout antwoord.";
                }
            }
        }

        private void LoadQuestions()
        {
            if (string.IsNullOrEmpty(Category))
                return;

            // Haal ALLE vragen van de categorie
            QuestionsInCategory = _db.Questions
                .Include(q => q.Choices)
                .Include(q => q.Category)
                .Where(q => q.Category.Name == Category)
                .ToList();

            if (QuestionsInCategory.Any())
            {
                // Gebruik CurrentQuestionIndex om de juiste vraag te tonen
                CurrentQuestion = QuestionsInCategory[CurrentQuestionIndex % QuestionsInCategory.Count];
            }
        }
    }
}

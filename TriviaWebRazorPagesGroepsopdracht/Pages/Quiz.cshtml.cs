using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TriviaWebRazorPages.Pages
{
    public class QuizModel : PageModel
    {
        [BindProperty]
        public int? ChoiceIndex { get; set; }   // index van aangeklikte keuze

        public Question CurrentQuestion { get; set; } = default!;
        public string? AnswerResult { get; set; }

        public void OnGet()
        {
            LoadQuestion();
        }

        public void OnPost()
        {
            LoadQuestion(); // Zorg dat CurrentQuestion gevuld blijft

            if (ChoiceIndex.HasValue && ChoiceIndex.Value >= 0 && ChoiceIndex.Value < CurrentQuestion.Choices.Count)
            {
                var selected = CurrentQuestion.Choices[ChoiceIndex.Value];
                AnswerResult = selected.Is_Correct ? "Correct!" : "Helaas, fout antwoord.";
            }
        }

        private void LoadQuestion()
        {
            // Mock data
            CurrentQuestion = new Question
            {
                Text = "Wat is de hoofdstad van België?",
                Choices = new List<Choice>
                {
                    new Choice { Text = "Brussel", Is_Correct = true },
                    new Choice { Text = "Antwerpen", Is_Correct = false },
                    new Choice { Text = "Gent", Is_Correct = false }
                }
            };
        }

        public class Question
        {
            public string Text { get; set; } = string.Empty;
            public List<Choice> Choices { get; set; } = new();
        }

        public class Choice
        {
            public string Text { get; set; } = string.Empty;
            public bool Is_Correct { get; set; }
        }
    }
}

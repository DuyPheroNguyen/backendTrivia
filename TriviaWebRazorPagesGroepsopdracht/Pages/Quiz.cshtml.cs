using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TriviaWebRazorPages.Pages
{
    public class QuizModel : PageModel
    {
        [BindProperty]
        public int? ChoiceIndex { get; set; }   // index van aangeklikte keuze

        public Question CurrentQuestion { get; set; } = default!; // de huidige vraag die getoond wordt
        public string? AnswerResult { get; set; } // resultaat van het antwoord, dus goed/fout is

        public void OnGet() // bij het laden van de pagina
        {
            LoadQuestion(); // Laad een voorbeeldvraag
        }

        public void OnPost() // bij het versturen van een gekozen antwoord
        {
            LoadQuestion(); // Zorg dat CurrentQuestion gevuld blijft

            if (ChoiceIndex.HasValue && ChoiceIndex.Value >= 0 && ChoiceIndex.Value < CurrentQuestion.Choices.Count) // controleer of de gekozen index geldig is
            {
                var selected = CurrentQuestion.Choices[ChoiceIndex.Value]; // de gekozen keuze
                AnswerResult = selected.Is_Correct ? "Correct!" : "Helaas, fout antwoord."; // bepaal of het correct is
            }
        }

        private void LoadQuestion() // Laad een voorbeeldvraag
        {
            // Mock data
            CurrentQuestion = new Question // voorbeeldvraag
            {
                Text = "Wat is de hoofdstad van België?", // de vraag zelf
                Choices = new List<Choice> //   mogelijke antwoorden
                {
                    new Choice { Text = "Brussel", Is_Correct = true }, // correct antwoord
                    new Choice { Text = "Antwerpen", Is_Correct = false }, // fout antwoord
                    new Choice { Text = "Gent", Is_Correct = false }    // fout antwoord
                }   
            };
        }

        public class Question // de vraag Category
        {
            public string Text { get; set; } = string.Empty; // de vraag zelf
            public List<Choice> Choices { get; set; } = new(); // mogelijke antwoorden
        }

        public class Choice // mogelijke antwoorden
        {
            public string Text { get; set; } = string.Empty; // het antwoord zelf
            public bool Is_Correct { get; set; }    // of het correct is, want in de database staat dit ook (true/false)
        }
    }
}

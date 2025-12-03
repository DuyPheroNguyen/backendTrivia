using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TriviaWebRazorPages.Pages
{
    public class QuizModel : PageModel
    {
        [BindProperty]
        public int? ChoiceIndex { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentQuestionIndex { get; set; } = 0;

        public Question? CurrentQuestion { get; set; }
        public List<Choice> CurrentChoices { get; set; } = new();
        public List<Question> AllQuestions { get; set; } = new();
        public string? AnswerResult { get; set; }
        public int Score { get; set; } = 0;

        public async Task OnGetAsync()
        {
            await LoadRandomQuestionAsync();
            Score = (int)(TempData["Score"] ?? 0);
            TempData.Keep("Score");
        }

        public async Task OnPostAsync()
        {
            await LoadRandomQuestionAsync();

            if (ChoiceIndex.HasValue && CurrentChoices.Any())
            {
                if (ChoiceIndex.Value >= 0 && ChoiceIndex.Value < CurrentChoices.Count)
                {
                    var selected = CurrentChoices[ChoiceIndex.Value];
                    AnswerResult = selected.Is_Correct ? "Correct!" : "Helaas, fout antwoord.";
                    
                    Score = (int)(TempData["Score"] ?? 0);
                    if (selected.Is_Correct)
                    {
                        Score++;
                    }
                    TempData["Score"] = Score;
                }
            }
        }

        private async Task LoadRandomQuestionAsync()
        {
            try
            {
                await SupabaseService.InitializeAsync();

                var questionsResponse = await SupabaseService.Client
                    .From<Question>()
                    .Get();

                AllQuestions = questionsResponse.Models;

                if (AllQuestions.Any())
                {
                    var questionIndex = CurrentQuestionIndex % AllQuestions.Count;
                    CurrentQuestion = AllQuestions[questionIndex];

                    var choicesResponse = await SupabaseService.Client
                        .From<Choice>()
                        .Where(c => c.Question_Id == CurrentQuestion.Id)
                        .Get();

                    CurrentChoices = choicesResponse.Models;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading question: {ex.Message}");
            }
        }
    }
}

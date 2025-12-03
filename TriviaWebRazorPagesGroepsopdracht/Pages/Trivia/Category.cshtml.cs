using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TriviaWebRazorPages.Pages.Trivia
{
    public class CategoryModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? Category { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentQuestionIndex { get; set; } = 0;

        [BindProperty]
        public int? ChoiceIndex { get; set; }

        public Question? CurrentQuestion { get; set; }
        public List<Choice> CurrentChoices { get; set; } = new();
        public List<Question> QuestionsInCategory { get; set; } = new();
        public string? AnswerResult { get; set; }
        public int Score { get; set; } = 0;
        public int TotalQuestions { get; set; } = 0;

        public async Task OnGetAsync()
        {
            await LoadQuestionsAsync();
        }

        public async Task OnPostAsync()
        {
            await LoadQuestionsAsync();

            if (ChoiceIndex.HasValue && CurrentQuestion != null && CurrentChoices.Any())
            {
                if (ChoiceIndex.Value >= 0 && ChoiceIndex.Value < CurrentChoices.Count)
                {
                    var selected = CurrentChoices[ChoiceIndex.Value];
                    AnswerResult = selected.Is_Correct ? "Correct!" : "Helaas, fout antwoord.";
                    
                    if (selected.Is_Correct)
                    {
                        Score = (int)(TempData["Score"] ?? 0) + 1;
                        TempData["Score"] = Score;
                    }
                }
            }
        }

        private async Task LoadQuestionsAsync()
        {
            if (string.IsNullOrEmpty(Category))
                return;

            try
            {
                await SupabaseService.InitializeAsync();

                // Get category by name
                var categoryResponse = await SupabaseService.Client
                    .From<global::Category>()
                    .Where(c => c.Name == Category)
                    .Get();

                if (!categoryResponse.Models.Any())
                    return;

                var categoryId = categoryResponse.Models.First().Id;

                // Get questions for this category
                var questionsResponse = await SupabaseService.Client
                    .From<Question>()
                    .Where(q => q.Category_Id == categoryId)
                    .Get();

                QuestionsInCategory = questionsResponse.Models;
                TotalQuestions = QuestionsInCategory.Count;

                if (QuestionsInCategory.Any())
                {
                    CurrentQuestion = QuestionsInCategory[CurrentQuestionIndex % QuestionsInCategory.Count];

                    // Get choices for current question
                    var choicesResponse = await SupabaseService.Client
                        .From<Choice>()
                        .Where(c => c.Question_Id == CurrentQuestion.Id)
                        .Get();

                    CurrentChoices = choicesResponse.Models;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading questions: {ex.Message}");
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TriviaWebRazorPages.Pages
{
    public class TriviaModel : PageModel
    {
        public List<Category> Categories { get; set; } = new();

        public async Task OnGetAsync()
        {
            try
            {
                await SupabaseService.InitializeAsync();
                
                var response = await SupabaseService.Client
                    .From<Category>()
                    .Get();

                Categories = response.Models;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading categories: {ex.Message}");
                Categories = new List<Category>();
            }
        }
    }
}

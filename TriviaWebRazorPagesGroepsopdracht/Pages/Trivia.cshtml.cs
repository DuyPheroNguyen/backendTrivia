using Microsoft.AspNetCore.Mvc.RazorPages;
using TriviaWebRazorPages.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            catch
            {
                Categories = new List<Category>();
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TriviaWebRazorPagesGroepsopdracht.Pages
{
    public class TestSupabaseModel : PageModel
    {
        public bool IsConnected { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public List<Category> Categories { get; set; } = new List<Category>();
        public int CategoryCount => Categories?.Count ?? 0;

        public async Task OnGetAsync()
        {
            try
            {
                // Initialiseer de Supabase client
                await SupabaseService.InitializeAsync();

                // Probeer categorieën op te halen als test
                var response = await SupabaseService.Client
                    .From<Category>()
                    .Get();

                Categories = response.Models;
                IsConnected = true;
            }
            catch (Exception ex)
            {
                IsConnected = false;
                ErrorMessage = ex.Message;
                Categories = new List<Category>();
            }
        }
    }
}

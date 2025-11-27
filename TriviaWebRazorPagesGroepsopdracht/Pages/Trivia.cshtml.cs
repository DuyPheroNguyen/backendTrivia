using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TriviaWebRazorPages.Data;
using TriviaWebRazorPages.Models;

namespace TriviaWebRazorPages.Pages
{
    public class TriviaModel : PageModel
    {
        private readonly ApplicationDbContext _db;      // database context

        public List<Question> Questions { get; set; } = new(); // lijst van alle vragen

        public TriviaModel(ApplicationDbContext db) // constructor met database context injectie
        {
            _db = db;
        }

        public void OnGet() // bij het laden van de pagina
        {
            Questions = _db.Questions // haal alle vragen op
                .Include(q => q.Category) // inclusief categorie
                .Include(q => q.Choices) // inclusief keuzes
                .ToList(); // zet om naar lijst
        }
    }
}

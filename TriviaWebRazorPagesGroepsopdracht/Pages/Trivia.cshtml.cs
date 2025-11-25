using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TriviaWebRazorPages.Data;
using TriviaWebRazorPages.Models;

namespace TriviaWebRazorPages.Pages
{
    public class TriviaModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public List<Question> Questions { get; set; } = new();

        public TriviaModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            Questions = _db.Questions
                .Include(q => q.Category)
                .Include(q => q.Choices)
                .ToList();
        }
    }
}

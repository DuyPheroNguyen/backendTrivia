using Postgrest.Attributes;
using Postgrest.Models;

[Table("category")]
public class Category : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; } // primary key
    
    [Column("name")]
    public string Name { get; set; } = string.Empty; // naam van de categorie

    public ICollection<Question> Questions { get; set; } = new List<Question>(); // alle vragen in deze categorie
}

public class Question
{
    public int Id { get; set; } //  primary key
    public int Category_Id { get; set; } // foreign key
    public string Text { get; set; } = string.Empty; // de vraag zelf
    public string Difficulty { get; set; } = string.Empty; // makkelijk, gemiddeld, moeilijk
    public string Type { get; set; } = string.Empty; // multiple choice of iets anders
    public string Explanation { get; set; } = string.Empty; // optioneel

    public Category? Category { get; set; } // de categorie waar deze vraag bij hoort
    public ICollection<Choice> Choices { get; set; } = new List<Choice>(); // mogelijke antwoorden
}

public class Choice 
{
    public int Id { get; set; }     // primary key
    public int Question_Id { get; set; }    // foreign key
    public string Text { get; set; } = string.Empty;        // mogelijk antwoord
    public bool Is_Correct { get; set; }    // true / false

    public Question? Question { get; set; } // de vraag waar dit antwoord bij hoort
}

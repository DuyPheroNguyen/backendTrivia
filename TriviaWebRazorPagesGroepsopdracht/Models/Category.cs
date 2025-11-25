public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<Question> Questions { get; set; } = new List<Question>();
}

public class Question
{
    public int Id { get; set; }
    public int Category_Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public string Difficulty { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Explanation { get; set; } = string.Empty;

    public Category? Category { get; set; }
    public ICollection<Choice> Choices { get; set; } = new List<Choice>();
}

public class Choice
{
    public int Id { get; set; }
    public int Question_Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public bool Is_Correct { get; set; }

    public Question? Question { get; set; }
}

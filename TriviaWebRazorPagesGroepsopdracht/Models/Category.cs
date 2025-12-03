using Postgrest.Attributes;
using Postgrest.Models;

[Table("category")]
public class Category : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }
    
    [Column("name")]
    public string Name { get; set; } = string.Empty;
}

[Table("question")]
public class Question : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }
    
    [Column("category_id")]
    public int Category_Id { get; set; }
    
    [Column("text")]
    public string Text { get; set; } = string.Empty;
    
    [Column("difficulty")]
    public string Difficulty { get; set; } = string.Empty;
    
    [Column("type")]
    public string Type { get; set; } = string.Empty;
    
    [Column("answer")]
    public string Answer { get; set; } = string.Empty;
    
    [Column("explanation")]
    public string? Explanation { get; set; }
}

[Table("choice")]
public class Choice : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }
    
    [Column("question_id")]
    public int Question_Id { get; set; }
    
    [Column("text")]
    public string Text { get; set; } = string.Empty;
    
    [Column("is_correct")]
    public bool Is_Correct { get; set; }
}

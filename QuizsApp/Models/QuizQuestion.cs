namespace QuizsApp.Models;

public class QuizQuestion
{
    public int Id { get; set; }
    public string QuestionText { get; set; }
    public string Choice1 { get; set; }
    public string Choice2 { get; set; }
    public string Choice3 { get; set; }
    public string Choice4 { get; set; }
    public int CorrectAnswerIndex { get; set; }
    public string Explanation { get; set; }
}

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace QuizsApp.Pages;

public class QuizResultModel : PageModel
{
    public int CorrectAnswers { get; set; }
    public int TotalQuestions { get; set; }

    public void OnGet(int correctAnswers, int totalQuestions)
    {
        CorrectAnswers = correctAnswers;
        TotalQuestions = totalQuestions;
    }
}

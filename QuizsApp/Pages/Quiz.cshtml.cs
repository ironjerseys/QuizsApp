using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizsApp.Models;
using QuizsApp.Services;

namespace QuizsApp.Pages;

public class QuizModel : PageModel
{
    private readonly QuizService _quizService;

    public QuizModel(QuizService quizService)
    {
        _quizService = quizService;
    }

    public QuizQuestion CurrentQuestion { get; set; }
    public bool IsAnswerSubmitted { get; set; }
    public bool IsCorrect { get; set; }
    public int CurrentQuestionIndex { get; set; } = 0;
    public int TotalQuestions { get; set; }
    public int CorrectAnswersCount { get; set; } = 0; // Compte les bonnes r�ponses

    public async Task OnGetAsync()
    {
        TotalQuestions = await _quizService.GetTotalQuestionsAsync();
        CurrentQuestion = await _quizService.GetQuestionAsync(CurrentQuestionIndex);
    }

    public async Task<IActionResult> OnPostAsync(int answer, int currentIndex)
    {
        // Mise � jour de l'index actuel
        CurrentQuestionIndex = currentIndex;

        TotalQuestions = await _quizService.GetTotalQuestionsAsync();
        CurrentQuestion = await _quizService.GetQuestionAsync(CurrentQuestionIndex);

        IsAnswerSubmitted = true;
        IsCorrect = _quizService.IsCorrectAnswer(CurrentQuestion, answer);

        // Si la r�ponse est correcte, incr�mente le compteur
        if (IsCorrect)
        {
            CorrectAnswersCount++;
        }

        // V�rifie si c'�tait la derni�re question
        if (CurrentQuestionIndex < TotalQuestions - 1)
        {
            CurrentQuestionIndex++;
            // Charger la question suivante
            CurrentQuestion = await _quizService.GetQuestionAsync(CurrentQuestionIndex);
        }
        else
        {
            // Redirige vers la page des r�sultats � la fin du quiz
            return RedirectToPage("Result", new { correctAnswers = CorrectAnswersCount, totalQuestions = TotalQuestions });
        }

        return Page();
    }
}

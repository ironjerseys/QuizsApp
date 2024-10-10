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
    public int CorrectAnswersCount { get; set; } = 0; // Compte les bonnes réponses

    public async Task OnGetAsync()
    {
        TotalQuestions = await _quizService.GetTotalQuestionsAsync();
        CurrentQuestion = await _quizService.GetQuestionAsync(CurrentQuestionIndex);
    }

    public async Task<IActionResult> OnPostAsync(int answer, int currentIndex)
    {
        // Mise à jour de l'index actuel
        CurrentQuestionIndex = currentIndex;

        TotalQuestions = await _quizService.GetTotalQuestionsAsync();
        CurrentQuestion = await _quizService.GetQuestionAsync(CurrentQuestionIndex);

        IsAnswerSubmitted = true;
        IsCorrect = _quizService.IsCorrectAnswer(CurrentQuestion, answer);

        // Si la réponse est correcte, incrémente le compteur
        if (IsCorrect)
        {
            CorrectAnswersCount++;
        }

        // Vérifie si c'était la dernière question
        if (CurrentQuestionIndex < TotalQuestions - 1)
        {
            CurrentQuestionIndex++;
            // Charger la question suivante
            CurrentQuestion = await _quizService.GetQuestionAsync(CurrentQuestionIndex);
        }
        else
        {
            // Redirige vers la page des résultats à la fin du quiz
            return RedirectToPage("Result", new { correctAnswers = CorrectAnswersCount, totalQuestions = TotalQuestions });
        }

        return Page();
    }
}

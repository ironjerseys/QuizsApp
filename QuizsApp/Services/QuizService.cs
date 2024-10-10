using Microsoft.EntityFrameworkCore;
using QuizsApp.Data;
using QuizsApp.Models;

namespace QuizsApp.Services;

public class QuizService
{
    private readonly QuizDbContext _context;

    public QuizService(QuizDbContext context)
    {
        _context = context;
    }

    public async Task<QuizQuestion> GetQuestionAsync(int index)
    {
        return await _context.QuizQuestions.Skip(index).FirstOrDefaultAsync();
    }

    public bool IsCorrectAnswer(QuizQuestion question, int chosenIndex)
    {
        return question.CorrectAnswerIndex == chosenIndex;
    }

    public async Task<int> GetTotalQuestionsAsync()
    {
        return await _context.QuizQuestions.CountAsync();
    }
}

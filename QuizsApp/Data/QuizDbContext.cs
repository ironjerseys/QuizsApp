using Microsoft.EntityFrameworkCore;
using QuizsApp.Models;

namespace QuizsApp.Data;

public class QuizDbContext : DbContext
{
    public QuizDbContext(DbContextOptions<QuizDbContext> options)
        : base(options)
    {
    }

    public DbSet<QuizQuestion> QuizQuestions { get; set; }
}

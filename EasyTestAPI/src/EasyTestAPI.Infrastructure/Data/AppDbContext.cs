using Ardalis.EFCore.Extensions;
using EasyTestAPI.Core.Entities;
using EasyTestAPI.Core.TestAggregate;
using EasyTestAPI.SharedKernel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyTestAPI.Infrastructure.Data;
public class AppDbContext : DbContext
{
  private readonly IMediator? _mediator;

  //public AppDbContext(DbContextOptions options) : base(options)
  //{
  //}

  public AppDbContext(DbContextOptions<AppDbContext> options, IMediator? mediator)
      : base(options)
  {
    _mediator = mediator;
  }

  public DbSet<User> Users => Set<User>();
  public DbSet<Role> Roles => Set<Role>();
  public DbSet<Test> Tests => Set<Test>();
  public DbSet<Question> Question => Set<Question>();
  public DbSet<QuestionType> QuestionTypes => Set<QuestionType>();
  public DbSet<Answer> Answers => Set<Answer>();
  public DbSet<AnsweredTest> AnsweredTests => Set<AnsweredTest>();
  public DbSet<TestAnswer> TestAnswers => Set<TestAnswer>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.ApplyAllConfigurationsFromCurrentAssembly();

    // alternately this is built-in to EF Core 2.2
    //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }

  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
  {
    int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

    // ignore events if no dispatcher provided
    if (_mediator == null) return result;

    // dispatch events only if save was successful
    var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
        .Select(e => e.Entity)
        .Where(e => e.Events.Any())
        .ToArray();

    foreach (var entity in entitiesWithEvents)
    {
      var events = entity.Events.ToArray();
      entity.Events.Clear();
      foreach (var domainEvent in events)
      {
        await _mediator.Publish(domainEvent).ConfigureAwait(false);
      }
    }

    return result;
  }

  public override int SaveChanges()
  {
    return SaveChangesAsync().GetAwaiter().GetResult();
  }
}

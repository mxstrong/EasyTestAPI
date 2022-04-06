using EasyTestAPI.Core.TestAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyTestAPI.Infrastructure.Data.Config;
public class TestAnswerConfiguration : IEntityTypeConfiguration<TestAnswer>
{
  public void Configure(EntityTypeBuilder<TestAnswer> builder)
  {
    builder.Property(t => t.TestAnswerId).IsRequired();
    builder.Property(t => t.AnsweredTestId).IsRequired();
    builder.Property(t => t.Answer).IsRequired();
    builder.Property(t => t.QuestionId).IsRequired();
    builder.HasOne<AnsweredTest>(t => t.AnsweredTest).WithMany(a => a.TestAnswers).HasForeignKey(t => t.AnsweredTestId);
    builder.HasOne<Question>(t => t.Question).WithMany(q => q.TestAnswers).HasForeignKey(t => t.QuestionId);
  }
}

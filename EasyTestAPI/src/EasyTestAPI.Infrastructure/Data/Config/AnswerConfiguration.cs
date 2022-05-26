using EasyTestAPI.Core.TestAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyTestAPI.Infrastructure.Data.Config;
public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
{
  public void Configure(EntityTypeBuilder<Answer> builder)
  {
    builder.Property(a => a.AnswerId).IsRequired();
    builder.Property(a => a.AnswerText).IsRequired();
    builder.Property(a => a.QuestionId).IsRequired();
    builder.HasOne<Question>(a => a.Question).WithMany(q => q.Answers).HasForeignKey(a => a.QuestionId);
  }
}

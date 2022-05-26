using EasyTestAPI.Core.TestAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyTestAPI.Infrastructure.Data.Config;
public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
  public void Configure(EntityTypeBuilder<Question> builder)
  {
    builder.Property(q => q.QuestionId).IsRequired();
    builder.Property(q => q.Text).IsRequired();
    builder.Property(q => q.TypeId).IsRequired();
    builder.Property(q => q.TestId).IsRequired();
    builder.HasOne<Test>(q => q.Test).WithMany(t => t.Questions).HasForeignKey(q => q.TestId);
    builder.HasOne<QuestionType>(q => q.QuestionType).WithMany(qt => qt.Questions).HasForeignKey(q => q.TypeId);
  }
}

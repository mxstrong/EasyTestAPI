using EasyTestAPI.Core.Entities;
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
  }
}

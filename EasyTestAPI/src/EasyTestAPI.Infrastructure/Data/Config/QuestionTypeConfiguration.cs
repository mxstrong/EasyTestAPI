using EasyTestAPI.Core.Entities;
using EasyTestAPI.Core.TestAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyTestAPI.Infrastructure.Data.Config;
public class QuestionTypeConfiguration : IEntityTypeConfiguration<QuestionType>
{
  public void Configure(EntityTypeBuilder<QuestionType> builder)
  {
    builder.Property(q => q.QuestionTypeId).IsRequired();
    builder.Property(q => q.Name).IsRequired();
  }
}

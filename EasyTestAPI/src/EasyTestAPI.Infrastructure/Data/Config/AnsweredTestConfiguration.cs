using EasyTestAPI.Core.TestAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyTestAPI.Infrastructure.Data.Config;
public class AnsweredTestConfiguration : IEntityTypeConfiguration<AnsweredTest>
{
  public void Configure(EntityTypeBuilder<AnsweredTest> builder)
  {
    builder.Property(a => a.AnsweredTestId).IsRequired();
    builder.Property(a => a.TestId).IsRequired();
    builder.HasOne<Test>(a => a.Test).WithMany(t => t.AnsweredTests).HasForeignKey(a => a.TestId);
  }
}

using EasyTestAPI.Core.TestAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyTestAPI.Infrastructure.Data.Config;
public class TestConfiguration : IEntityTypeConfiguration<Test>
{
  public void Configure(EntityTypeBuilder<Test> builder)
  {
    builder.Property(t => t.TestId).IsRequired();
    builder.Property(t => t.Name).IsRequired();
    builder.Property(t => t.Code).IsRequired();
    builder.Property(t => t.CreatedById).IsRequired();
  }
}

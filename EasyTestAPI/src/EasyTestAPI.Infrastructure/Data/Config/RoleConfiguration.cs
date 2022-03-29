using EasyTestAPI.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyTestAPI.Infrastructure.Data.Config;
public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
  public void Configure(EntityTypeBuilder<Role> builder)
  {
    builder.Property(r => r.RoleId).IsRequired();
    builder.Property(r => r.Name).IsRequired();
  }
}

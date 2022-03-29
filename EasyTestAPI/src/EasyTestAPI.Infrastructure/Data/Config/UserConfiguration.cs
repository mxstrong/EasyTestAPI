using EasyTestAPI.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyTestAPI.Infrastructure.Data.Config;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.Property(u => u.UserId).IsRequired();
    builder.Property(u => u.Email).IsRequired();
    builder.Property(u => u.DisplayName).IsRequired();
    builder.Property(u => u.PasswordHash).IsRequired();
    builder.Property(u => u.PasswordSalt).IsRequired();
    builder.Property(u => u.RoleId).IsRequired();
    builder.Property(u => u.Activated).IsRequired();
    builder.Property(u => u.CreatedAt).IsRequired();
  }
}

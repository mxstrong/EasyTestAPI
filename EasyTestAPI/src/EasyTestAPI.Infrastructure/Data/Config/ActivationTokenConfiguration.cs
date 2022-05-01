using EasyTestAPI.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyTestAPI.Infrastructure.Data.Config;
public class ActivationTokenConfiguration : IEntityTypeConfiguration<ActivationToken>
{
  public void Configure(EntityTypeBuilder<ActivationToken> builder)
  {
    builder.Property(at => at.ActivationTokenId).IsRequired();
    builder.Property(at => at.UserId).IsRequired();
    builder.Property(at => at.CreatedAt).IsRequired();
    builder.HasOne<User>(at => at.User).WithMany(u => u.ActivationTokens).HasForeignKey(at => at.UserId);
  }
}

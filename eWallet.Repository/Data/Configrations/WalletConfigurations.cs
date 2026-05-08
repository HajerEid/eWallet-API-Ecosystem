using eWallet.Core.Entities.eWallet;
using eWallet.Core.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eWallet.Repository.Data.Configrations
{
    public class WalletConfigurations : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.Property(w => w.Balance).HasColumnType("decimal(18,2)");

            builder.Property(w => w.AppUserId).IsRequired();
            builder.HasIndex(w => w.AppUserId).IsUnique();

            //builder.HasOne<AppUser>()
            //    .WithMany()
            //    .HasForeignKey(w => w.AppUserId);
            //builder.Ignore(w => w.AppUser);
            builder.ToTable("Wallets");
        }
    }
}

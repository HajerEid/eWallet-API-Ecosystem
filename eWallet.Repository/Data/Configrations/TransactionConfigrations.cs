using eWallet.Core.Entities.eWallet;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace E_Wallet.Repository.Data.Configrations
{
    public class TransactionConfigrations : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.Property(t=>t.Amount).HasColumnType("decimal(18,2)");
            //Wallet
            builder.HasOne(t=>t.Wallet)
                .WithMany(w => w.Transactions)
                .HasForeignKey(t=>t.WalletId)
                .OnDelete(DeleteBehavior.Cascade);
            //Category
            builder.HasOne(t => t.Category)
               .WithMany()
               .HasForeignKey(t => t.CategoryId)
               .OnDelete(DeleteBehavior.SetNull);

        }
    }
}

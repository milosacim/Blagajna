using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MivexBlagajna.Data.Models;

namespace MivexBlagajna.DataAccess.Configurations
{
    public class KontoConfiguration : IEntityTypeConfiguration<Konto>
    {
        public void Configure(EntityTypeBuilder<Konto> builder)
        {
            builder.ToTable("Konto");

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1, 1);

            builder.Property(e => e.Naziv)
                .IsRequired()
                .HasMaxLength(250);

            builder.HasMany(t => t.Transakcije)
                .WithOne(v => v.Konto)
                .HasForeignKey(v => v.Konto_Id)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

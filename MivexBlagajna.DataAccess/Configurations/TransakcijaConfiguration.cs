using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MivexBlagajna.Data.Models;

namespace MivexBlagajna.DataAccess.Configurations
{
    public class TransakcijaConfiguration : IEntityTypeConfiguration<Transakcija>
    {
        public void Configure(EntityTypeBuilder<Transakcija> builder)
        {
            builder.ToTable("Transakcije");

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1, 1);

            builder.Property(e => e.Nalog)
                .IsRequired();

            builder.Property(e => e.Uplata)
                .HasColumnType("decimal(20, 5)")
                .HasDefaultValue(0.0000M);

            builder.Property(e => e.Isplata)
                .HasColumnType("decimal(20, 5)")
                .HasDefaultValue(0.0000M);
        }
    }
}

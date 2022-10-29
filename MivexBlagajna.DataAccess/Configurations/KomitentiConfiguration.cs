using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MivexBlagajna.Data.Models;

namespace MivexBlagajna.DataAccess.Configurations
{
    public class KomitentiConfiguration : IEntityTypeConfiguration<Komitent>
    {
        public void Configure(EntityTypeBuilder<Komitent> builder)
        {
            builder.ToTable("Komitent");

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1, 1);

            builder.Property(e => e.Naziv)
                .HasMaxLength(250);

            builder.Property(e => e.Naziv2)
                .HasMaxLength(250);

            builder.Property(e => e.Jmbg)
                .HasMaxLength(13);

            builder.Property(e => e.PostanskiBroj)
                .HasMaxLength(5);

            builder.HasOne(k => k.MestoTroska)
                .WithMany(m => m.Komitenti)
                .HasForeignKey(k => k.MestoTroska_Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.Transakcije)
                .WithOne(t => t.Komitent)
                .HasForeignKey(t => t.Komitent_Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

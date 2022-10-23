using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MivexBlagajna.Data.Models;

namespace MivexBlagajna.DataAccess.Configurations
{
    public class MestoTroskaConfiguration : IEntityTypeConfiguration<MestoTroska>
    {
        public void Configure(EntityTypeBuilder<MestoTroska> builder)
        {
            builder.ToTable("MestaTroska");

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1, 1);

            builder.Property(e => e.Naziv)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(e => e.Nivo)
                .IsRequired();

            builder.HasMany(m => m.Komitenti)
                .WithOne(k => k.MestoTroska)
                .HasForeignKey(k => k.MestoTroska_Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(m => m.DecaMestoTroska)
                .WithOne(m => m.RoditeljMestoTroska)
                .HasForeignKey(m => m.NadredjenoMesto_Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(t => t.Transakcije)
                .WithOne(v => v.MestoTroska)
                .HasForeignKey(v => v.MestoTroska_Id)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

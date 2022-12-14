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

            builder.HasMany(m => m.DecaMestoTroska)
                .WithOne(m => m.RoditeljMestoTroska)
                .HasForeignKey(m => m.NadredjenoMesto_Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(m => m.Transakcije)
                .WithOne(t => t.MestoTroska)
                .HasForeignKey(t => t.MestoTroska_Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

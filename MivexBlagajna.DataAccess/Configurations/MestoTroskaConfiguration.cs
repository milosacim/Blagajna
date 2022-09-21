using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MivexBlagajna.Data.Models;

namespace MivexBlagajna.DataAccess.Configurations
{
    public class MestoTroskaConfiguration : IEntityTypeConfiguration<MestoTroska>
    {
        public void Configure(EntityTypeBuilder<MestoTroska> builder)
        {
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1, 1);

            builder.Property(e => e.Naziv)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(e => e.Nivo)
                .IsRequired();

            builder.HasMany(t => t.Komitenti)
                .WithOne(v => v.MestoTroska)
                .HasForeignKey(v => v.MestoTroska_id)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MivexBlagajna.Data.Models;

namespace MivexBlagajna.DataAccess.Configurations
{
    public class VrsteNalogaConfiguration : IEntityTypeConfiguration<VrsteNaloga>
    {
        public void Configure(EntityTypeBuilder<VrsteNaloga> builder)
        {
            builder.ToTable("VrsteNaloga");

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1, 1);

            builder.HasMany(v => v.Transakcije)
                .WithOne(t => t.VrsteNaloga)
                .HasForeignKey(t => t.VrsteNaloga_Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

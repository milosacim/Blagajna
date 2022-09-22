using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MivexBlagajna.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MivexBlagajna.DataAccess.Configurations
{
    public class KontoConfiguration : IEntityTypeConfiguration<Konto>
    {
        public void Configure(EntityTypeBuilder<Konto> builder)
        {
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1, 1);

            builder.Property(e => e.Naziv)
                .IsRequired()
                .HasMaxLength(250);
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Configuration
{
    public class ProductConfiguration : BaseConfiguration<Product>
    {
        public override void Configuration(ref EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(ProductConsts.NameLength);

            builder.Property(x => x.Amount)
                .IsRequired()
                .HasMaxLength(ProductConsts.AmountLength);
        }
    }

    public static class ProductConsts
    {
        public const int NameLength = 50;
        public const int AmountLength = 20;
    }
}

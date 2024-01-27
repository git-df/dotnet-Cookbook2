using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entities.Configuration
{
    public class ProductConfiguration : BaseConfiguration<Product>
    {
        public const int NameLength = 50;
        public const int AmountLength = 20;

        public override void Configuration(ref EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(NameLength);

            builder.Property(x => x.Amount)
                .IsRequired()
                .HasMaxLength(AmountLength);
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entities.Configuration
{
    public class RecipeConfiguration : BaseConfiguration<Recipe>
    {
        public const int NameLength = 50;
        public const int ShortLength = 250;
        public const int DescriptionLength = 2000;

        public override void Configuration(ref EntityTypeBuilder<Recipe> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(NameLength);

            builder.Property(x => x.Short)
                .IsRequired()
                .HasMaxLength(ShortLength);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(DescriptionLength);
        }
    }
}
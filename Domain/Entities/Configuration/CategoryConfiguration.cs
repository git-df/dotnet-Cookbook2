using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entities.Configuration
{
    public class CategoryConfiguration : BaseConfiguration<Category>
    {
        public const int NameLength = 20;

        public override void Configuration(ref EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(NameLength);
        }
    }
}

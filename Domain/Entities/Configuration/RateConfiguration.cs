using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entities.Configuration
{
    public class RateConfiguration : BaseConfiguration<Rate>
    {
        public override void Configuration(ref EntityTypeBuilder<Rate> builder)
        {
            builder.Property(x => x.Value)
                .IsRequired();
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entities.Configuration
{
    public class CommentConfiguration : BaseConfiguration<Comment>
    {
        public const int ContentLength = 250;

        public override void Configuration(ref EntityTypeBuilder<Comment> builder)
        {
            builder.Property(x => x.Content)
                .IsRequired()
                .HasMaxLength(ContentLength);
        }
    }
}

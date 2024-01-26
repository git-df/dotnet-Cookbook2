using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Configuration
{
    public class CommentConfiguration : BaseConfiguration<Comment>
    {
        public override void Configuration(ref EntityTypeBuilder<Comment> builder)
        {
            builder.Property(x => x.Content)
                .IsRequired()
                .HasMaxLength(CommentConsts.ContentLength);
        }
    }

    public static class CommentConsts
    {
        public const int ContentLength = 250;
    }
}

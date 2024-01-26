using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Configuration
{
    public class CategoryConfiguration : BaseConfiguration<Category>
    {
        public override void Configuration(ref EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(CategoryConsts.NameLength);
        }
    }

    public static class CategoryConsts
    {
        public const int NameLength = 20;
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Configuration
{
    public class RecipeConfiguration : BaseConfiguration<Recipe>
    {
        public override void Configuration(ref EntityTypeBuilder<Recipe> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(RecipeConsts.NameLength);

            builder.Property(x => x.Short)
                .IsRequired()
                .HasMaxLength(RecipeConsts.ShortLength);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(RecipeConsts.DescriptionLength);
        }
    }

    public static class RecipeConsts
    {
        public const int NameLength = 50;
        public const int ShortLength = 250;
        public const int DescriptionLength = 2000;
    }
}
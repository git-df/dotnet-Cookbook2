using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

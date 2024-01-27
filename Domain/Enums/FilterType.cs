using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum FilterType
    {
        EqualValue = 1,
        ManyEqualValues = 2,
        NotEqualValue = 3,
        ManyNotEqualValues = 4,
        ContainsValue = 5,
        ManyContainsValue = 6,
        ValueBetween = 7
    }
}

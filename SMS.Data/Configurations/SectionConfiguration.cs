using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMS.Data.Entities;

namespace SMS.Data.Configurations
{
    public class SectionConfiguration : EntityBaseConfiguration<Section>
    {
        public SectionConfiguration()
        {
            Property(r => r.SectionName).IsRequired().HasMaxLength(50);
        }
    }
}

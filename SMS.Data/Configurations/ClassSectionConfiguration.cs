using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMS.Data.Entities;

namespace SMS.Data.Configurations
{
    public class ClassSectionConfiguration : EntityBaseConfiguration<ClassSection>
    {
        public ClassSectionConfiguration()
        {
            Property(cs => cs.ClassId).IsRequired();
            Property(cs => cs.SectionId).IsRequired();
        }
    }
}

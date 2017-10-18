using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMS.Data.Entities;

namespace SMS.Data.Configurations
{
    public class ClassConfiguration : EntityBaseConfiguration<Class>
    {
        public ClassConfiguration()
        {
            Property(u => u.ClassName).IsRequired().HasMaxLength(10);
        }
    }
}

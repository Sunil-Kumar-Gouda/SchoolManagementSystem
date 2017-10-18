using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMS.Data.Entities;

namespace SMS.Data.Configurations
{
    public class ResultConfiguration: EntityBaseConfiguration<Result>
    {
        public ResultConfiguration()
        {
            Property(e => e.SecuredMarks).IsRequired();
        }
    }
}

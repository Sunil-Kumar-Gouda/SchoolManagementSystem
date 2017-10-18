using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMS.Data.Entities;

namespace SMS.Data.Configurations
{
    public class ExamConfiguration : EntityBaseConfiguration<Exam>
    {
        public ExamConfiguration()
        {
            Property(e => e.ExamName).IsRequired().HasMaxLength(45);
        }
    }
}

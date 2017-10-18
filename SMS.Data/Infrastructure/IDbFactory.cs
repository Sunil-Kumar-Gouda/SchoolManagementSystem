using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMS.Data.Entities;

namespace SMS.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        SMSDbContext Init();
    }
}

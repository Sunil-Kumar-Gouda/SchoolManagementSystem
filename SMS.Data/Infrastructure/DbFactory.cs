using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMS.Data.Entities;

namespace SMS.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {

        SMSDbContext dbContext;
        public SMSDbContext Init() //interface implementation should be public
        {
            return dbContext ?? (dbContext = new SMSDbContext());
        }

        protected override void DisposeCore()
        {
            if(dbContext != null){
                dbContext.Dispose();
            }
        }
    }
}

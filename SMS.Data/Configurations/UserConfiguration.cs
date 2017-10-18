using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMS.Data.Entities;

namespace SMS.Data.Configurations
{
    public class UserConfiguration : EntityBaseConfiguration<User>
    {
        public UserConfiguration()
        {
            Property(u => u.UserName).IsRequired().HasMaxLength(256);
            Property(u => u.Email).IsRequired();
            Property(u => u.PasswordHash).IsRequired();
            Property(u => u.SecurityStamp).IsRequired();
            Property(u => u.PhoneNumber).IsRequired();
        }
    }
}

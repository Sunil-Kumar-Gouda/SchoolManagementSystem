using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Data.Entity.ModelConfiguration.Conventions;
using SMS.Data.Entities;
using SMS.Data.Configurations;

namespace SMS.Data
{
    public partial class SMSDbContext : DbContext
    {
        public SMSDbContext()
            : base("name=SMSEntities") //SMSDbContext  -> SMSEntities
        {
            Database.SetInitializer<SMSDbContext>(null);
        }


        public virtual void Commit()
        {
            base.SaveChanges();
        }

        #region Entity Sets
        
        public IDbSet<Class> ClassSet { get; set; }
        public IDbSet<ClassSection> ClassSectionSet { get; set; }
        public IDbSet<Exam> ExamSet { get; set; }
        public IDbSet<Result> ResultSet { get; set; }
        public IDbSet<Role> RoleSet { get; set; }
        public IDbSet<Section> SectionSet { get; set; }
        public IDbSet<Student> StudentSet { get; set; }
        public IDbSet<Subject> SubjectSet { get; set; }
        public IDbSet<Teacher> TeacherSet { get; set; }
        public IDbSet<User> UserSet { get; set; }
        public IDbSet<Error> ErrorSet { get; set; }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new ClassConfiguration());
            modelBuilder.Configurations.Add(new ClassSectionConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new ExamConfiguration());
            modelBuilder.Configurations.Add(new ResultConfiguration());
            modelBuilder.Configurations.Add(new SectionConfiguration());
            modelBuilder.Configurations.Add(new StudentConfiguration());
            modelBuilder.Configurations.Add(new SubjectConfiguration());
            modelBuilder.Configurations.Add(new TeacherConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            //modelBuilder.Configurations.Add(new UserRoleConfiguration());
        }
    }
}

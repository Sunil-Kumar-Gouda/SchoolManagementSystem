namespace SMS.Data.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SMSEntities : DbContext
    {
        public SMSEntities()
            : base("name=SMSEntities")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<ClassSection> ClassSections { get; set; }
        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<Result> Results { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<UserClaim> UserClaims { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>()
                .Property(e => e.ClassName)
                .IsUnicode(false);

            modelBuilder.Entity<Class>()
                .HasMany(e => e.Subjects)
                .WithRequired(e => e.Class)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Exam>()
                .Property(e => e.ExamName)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Roles)
                .Map(m => m.ToTable("UserRole").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<Section>()
                .Property(e => e.SectionName)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.EmailId)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.MobileNo)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.ClassSections)
                .WithRequired(e => e.Student)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Subject>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Subject>()
                .Property(e => e.SubjectCode)
                .IsUnicode(false);

            modelBuilder.Entity<Subject>()
                .HasMany(e => e.Exams)
                .WithRequired(e => e.Subject)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Teacher>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Teacher>()
                .Property(e => e.EmailId)
                .IsUnicode(false);

            modelBuilder.Entity<Teacher>()
                .Property(e => e.MobileNo)
                .IsUnicode(false);

            modelBuilder.Entity<Teacher>()
                .Property(e => e.Address)
                .IsUnicode(false);
        }
    }
}

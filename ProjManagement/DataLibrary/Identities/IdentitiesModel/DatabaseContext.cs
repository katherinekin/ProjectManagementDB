namespace DataLibrary.Identities.IdentitiesModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("name=IdentitiesConnection")
        {
        }

        public virtual DbSet<DEPARTMENT> DEPARTMENT { get; set; }
        public virtual DbSet<EMPLOYEE> EMPLOYEE { get; set; }
        public virtual DbSet<EMPLOYEE_PROFESSION> EMPLOYEE_PROFESSION { get; set; }
        public virtual DbSet<EMPLOYEE_STATUS> EMPLOYEE_STATUS { get; set; }
        public virtual DbSet<EMPLOYEE_TYPE> EMPLOYEE_TYPE { get; set; }
        public virtual DbSet<LOGIN> LOGIN { get; set; }
        public virtual DbSet<PROJECT> PROJECT { get; set; }
        public virtual DbSet<PROJECT_STATUS> PROJECT_STATUS { get; set; }
        public virtual DbSet<RESOURCE_STATUS> RESOURCE_STATUS { get; set; }
        public virtual DbSet<RESOURCE_TYPE> RESOURCE_TYPE { get; set; }
        public virtual DbSet<RESOURCES> RESOURCES { get; set; }
        public virtual DbSet<ACTIVITIES> ACTIVITIES { get; set; }
        public virtual DbSet<BUDGET> BUDGET { get; set; }
        public virtual DbSet<PROJ_RESOURCES> PROJ_RESOURCES { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DEPARTMENT>()
                .Property(e => e.Dname)
                .IsUnicode(false);

            modelBuilder.Entity<DEPARTMENT>()
                .Property(e => e.Phone_Number)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DEPARTMENT>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<DEPARTMENT>()
                .HasMany(e => e.EMPLOYEE)
                .WithOptional(e => e.DEPARTMENT)
                .HasForeignKey(e => e.EDname);

            modelBuilder.Entity<DEPARTMENT>()
                .HasMany(e => e.PROJECT)
                .WithOptional(e => e.DEPARTMENT)
                .HasForeignKey(e => e.PDname);

            modelBuilder.Entity<EMPLOYEE>()
                .Property(e => e.Fname)
                .IsUnicode(false);

            modelBuilder.Entity<EMPLOYEE>()
                .Property(e => e.Lname)
                .IsUnicode(false);

            modelBuilder.Entity<EMPLOYEE>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<EMPLOYEE>()
                .Property(e => e.Gender)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<EMPLOYEE>()
                .Property(e => e.EDname)
                .IsUnicode(false);

            modelBuilder.Entity<EMPLOYEE>()
                .HasMany(e => e.DEPARTMENT1)
                .WithOptional(e => e.EMPLOYEE1)
                .HasForeignKey(e => e.Manager_ID);

            modelBuilder.Entity<EMPLOYEE>()
                .HasMany(e => e.ACTIVITIES)
                .WithOptional(e => e.EMPLOYEE)
                .HasForeignKey(e => e.AEmployee_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<EMPLOYEE>()
                .HasOptional(e => e.LOGIN)
                .WithRequired(e => e.EMPLOYEE)
                .WillCascadeOnDelete();

            modelBuilder.Entity<EMPLOYEE_PROFESSION>()
                .Property(e => e.EPDescription)
                .IsUnicode(false);

            modelBuilder.Entity<EMPLOYEE_PROFESSION>()
                .HasMany(e => e.EMPLOYEE)
                .WithOptional(e => e.EMPLOYEE_PROFESSION)
                .HasForeignKey(e => e.Profession);

            modelBuilder.Entity<EMPLOYEE_STATUS>()
                .Property(e => e.ESDescription)
                .IsUnicode(false);

            modelBuilder.Entity<EMPLOYEE_STATUS>()
                .HasMany(e => e.EMPLOYEE)
                .WithOptional(e => e.EMPLOYEE_STATUS)
                .HasForeignKey(e => e.Estatus);

            modelBuilder.Entity<EMPLOYEE_TYPE>()
                .Property(e => e.ETDescription)
                .IsUnicode(false);

            modelBuilder.Entity<EMPLOYEE_TYPE>()
                .HasMany(e => e.EMPLOYEE)
                .WithOptional(e => e.EMPLOYEE_TYPE)
                .HasForeignKey(e => e.Type);

            modelBuilder.Entity<LOGIN>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<LOGIN>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<PROJECT>()
                .Property(e => e.Pname)
                .IsUnicode(false);

            modelBuilder.Entity<PROJECT>()
                .Property(e => e.PDname)
                .IsUnicode(false);

            modelBuilder.Entity<PROJECT>()
                .Property(e => e.Client)
                .IsUnicode(false);

            modelBuilder.Entity<PROJECT>()
                .HasMany(e => e.ACTIVITIES)
                .WithOptional(e => e.PROJECT)
                .HasForeignKey(e => e.AProject_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<PROJECT>()
                .HasMany(e => e.BUDGET)
                .WithRequired(e => e.PROJECT)
                .HasForeignKey(e => e.BProject_ID);

            modelBuilder.Entity<PROJECT>()
                .HasMany(e => e.PROJ_RESOURCES)
                .WithRequired(e => e.PROJECT)
                .HasForeignKey(e => e.RProject_ID);

            modelBuilder.Entity<PROJECT_STATUS>()
                .Property(e => e.PSDescription)
                .IsUnicode(false);

            modelBuilder.Entity<PROJECT_STATUS>()
                .HasMany(e => e.PROJECT)
                .WithOptional(e => e.PROJECT_STATUS)
                .HasForeignKey(e => e.Pstatus);

            modelBuilder.Entity<RESOURCE_STATUS>()
                .Property(e => e.RSDescription)
                .IsUnicode(false);

            modelBuilder.Entity<RESOURCE_STATUS>()
                .HasMany(e => e.RESOURCES)
                .WithOptional(e => e.RESOURCE_STATUS)
                .HasForeignKey(e => e.Rstatus);

            modelBuilder.Entity<RESOURCE_TYPE>()
                .Property(e => e.RTDescription)
                .IsUnicode(false);

            modelBuilder.Entity<RESOURCE_TYPE>()
                .HasMany(e => e.RESOURCES)
                .WithOptional(e => e.RESOURCE_TYPE)
                .HasForeignKey(e => e.Rtype);

            modelBuilder.Entity<RESOURCES>()
                .Property(e => e.Resource_Name)
                .IsUnicode(false);

            modelBuilder.Entity<RESOURCES>()
                .Property(e => e.Amount_Available)
                .HasPrecision(5, 2);

            modelBuilder.Entity<RESOURCES>()
                .Property(e => e.Total_Cost)
                .HasPrecision(10, 2);

            modelBuilder.Entity<ACTIVITIES>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<ACTIVITIES>()
                .Property(e => e.Weekly_Hours)
                .HasPrecision(2, 2);

            modelBuilder.Entity<BUDGET>()
                .Property(e => e.Estimated_Income)
                .HasPrecision(10, 2);

            modelBuilder.Entity<BUDGET>()
                .Property(e => e.Estimated_Expense)
                .HasPrecision(10, 2);

            modelBuilder.Entity<BUDGET>()
                .Property(e => e.Estimated_Profit)
                .HasPrecision(10, 2);

            modelBuilder.Entity<PROJ_RESOURCES>()
                .Property(e => e.Resource_Name)
                .IsUnicode(false);

            modelBuilder.Entity<PROJ_RESOURCES>()
                .Property(e => e.Amount_Needed)
                .HasPrecision(5, 2);
        }
    }
}

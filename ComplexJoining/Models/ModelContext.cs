using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ComplexJoining.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Examlevel> Examlevels { get; set; }
        public virtual DbSet<Monthlvl> Monthlvls { get; set; }
        public virtual DbSet<Student1> Student1s { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1522))(CONNECT_DATA=(SERVICE_NAME=XEPDB1)));Persist Security Info=True;User Id=projectjoining1;Password=oracle;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("PROJECTJOINING1")
                .HasAnnotation("Relational:Collation", "USING_NLS_COMP");

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.Crid);

                entity.ToTable("COURSE");

                entity.Property(e => e.Crid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("CRID");

                entity.Property(e => e.Crname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CRNAME");

                entity.Property(e => e.Did)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("DID");

                entity.Property(e => e.Exid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("EXID");

                entity.Property(e => e.Mid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("MID");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.Did);

                entity.ToTable("DEPARTMENT");

                entity.Property(e => e.Did)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("DID");

                entity.Property(e => e.Dname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DNAME");
            });

            modelBuilder.Entity<Examlevel>(entity =>
            {
                entity.HasKey(e => e.Exid);

                entity.ToTable("EXAMLEVEL");

                entity.Property(e => e.Exid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("EXID");

                entity.Property(e => e.Exname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EXNAME");
            });

            modelBuilder.Entity<Monthlvl>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("MONTHLVL");

                entity.Property(e => e.Mid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("MID");

                entity.Property(e => e.Mname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("MNAME");
            });

            modelBuilder.Entity<Student1>(entity =>
            {
                entity.HasKey(e => e.Did);

                entity.ToTable("STUDENT1");

                entity.Property(e => e.Did)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("DID");

                entity.Property(e => e.Sid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("SID");

                entity.Property(e => e.Sname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("SNAME");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

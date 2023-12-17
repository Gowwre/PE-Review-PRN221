using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObjects.Entities {
    public partial class CandidateManagementContext : DbContext {
        public CandidateManagementContext() {
        }

        public CandidateManagementContext(DbContextOptions<CandidateManagementContext> options)
            : base(options) {
        }

        public virtual DbSet<CandidateProfile> CandidateProfiles { get; set; } = null!;
        public virtual DbSet<HrAccount> Hraccounts { get; set; } = null!;
        public virtual DbSet<JobPosting> JobPostings { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https: //go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(
                    GetConnectionStrings());
            }
        }

        private string GetConnectionStrings() {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .SetBasePath(Directory.GetCurrentDirectory())
                .Build();
            return config.GetConnectionString("DefaultDB");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<CandidateProfile>(entity => {
                entity.HasKey(e => e.CandidateId)
                    .HasName("PK__Candidat__DF539BFC1F9FE8BC");

                entity.ToTable("CandidateProfile");

                entity.Property(e => e.CandidateId)
                    .HasMaxLength(20)
                    .HasColumnName("CandidateID");

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.Fullname).HasMaxLength(100);

                entity.Property(e => e.PostingId)
                    .HasMaxLength(20)
                    .HasColumnName("PostingID");

                entity.Property(e => e.ProfileShortDescription).HasMaxLength(250);

                entity.Property(e => e.ProfileUrl)
                    .HasMaxLength(150)
                    .HasColumnName("ProfileURL");

                entity.HasOne(d => d.Posting)
                    .WithMany(p => p.CandidateProfiles)
                    .HasForeignKey(d => d.PostingId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Candidate__Posti__267ABA7A");
            });

            modelBuilder.Entity<HrAccount>(entity => {
                entity.HasKey(e => e.Email)
                    .HasName("PK__HRAccoun__A9D1053592C51CE5");

                entity.ToTable("HRAccount");

                entity.Property(e => e.Email).HasMaxLength(20);

                entity.Property(e => e.FullName).HasMaxLength(30);

                entity.Property(e => e.Password).HasMaxLength(40);
            });

            modelBuilder.Entity<JobPosting>(entity => {
                entity.HasKey(e => e.PostingId)
                    .HasName("PK__JobPosti__C31796A51F0A81FD");

                entity.ToTable("JobPosting");

                entity.Property(e => e.PostingId)
                    .HasMaxLength(20)
                    .HasColumnName("PostingID");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.JobPostingTitle).HasMaxLength(100);

                entity.Property(e => e.PostedDate).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
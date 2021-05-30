using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace chatApp.Database
{
    public partial class chatContext : DbContext
    {
        public chatContext()
        {
        }

        public chatContext(DbContextOptions<chatContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Friends> Friends { get; set; }
        public virtual DbSet<Histories> Histories { get; set; }
        public virtual DbSet<UserAdresses> UserAdresses { get; set; }
        public virtual DbSet<UserImages> UserImages { get; set; }
        public virtual DbSet<UserTypes> UserTypes { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UsersUserTypes> UsersUserTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=chat;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Friends>(entity =>
            {
                entity.Property(e => e.DateAdded).HasColumnType("datetime");

                entity.Property(e => e.UserIdprimary).HasColumnName("UserIDPrimary");

                entity.Property(e => e.UserIdsecondary).HasColumnName("UserIDSecondary");

                entity.HasOne(d => d.UserIdprimaryNavigation)
                    .WithMany(p => p.FriendsUserIdprimaryNavigation)
                    .HasForeignKey(d => d.UserIdprimary)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Friends_Users");

                entity.HasOne(d => d.UserIdsecondaryNavigation)
                    .WithMany(p => p.FriendsUserIdsecondaryNavigation)
                    .HasForeignKey(d => d.UserIdsecondary)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Friends_Users-Secondary");
            });

            modelBuilder.Entity<Histories>(entity =>
            {
                entity.Property(e => e.Message).HasMaxLength(500);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.UserIdPrimaryNavigation)
                    .WithMany(p => p.HistoriesUserIdPrimaryNavigation)
                    .HasForeignKey(d => d.UserIdPrimary)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Histories_Users-Primary");

                entity.HasOne(d => d.UserIdSecondaryNavigation)
                    .WithMany(p => p.HistoriesUserIdSecondaryNavigation)
                    .HasForeignKey(d => d.UserIdSecondary)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Histories_Users-Secondary");
            });

            modelBuilder.Entity<UserAdresses>(entity =>
            {
                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ZipCode)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<UserImages>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserImages)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserImages_Users");
            });

            modelBuilder.Entity<UserTypes>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("CS_Email")
                    .IsUnique();

                entity.HasIndex(e => e.Telephone)
                    .HasName("CS_Telephone")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.PasswordSalt).HasMaxLength(500);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Telephone)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.UserAdress)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserAdressId)
                    .HasConstraintName("FK_Users_UserAdresses");
            });

            modelBuilder.Entity<UsersUserTypes>(entity =>
            {
                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserTypeId).HasColumnName("UserTypeID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UsersUserTypes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsersUserTypes_Users");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.UsersUserTypes)
                    .HasForeignKey(d => d.UserTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsersUserTypes_UserTypes");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

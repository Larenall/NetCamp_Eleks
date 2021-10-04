using System;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.Common.Interfaces;

#nullable disable

namespace Infrastructure.Persistance
{
    public partial class NetCampContext : DbContext, IDbContext
    {
        public NetCampContext()
        {
        }

        public NetCampContext(DbContextOptions<NetCampContext> options)
            : base(options)
        {
        }

        public virtual DbSet<UserSubscription> UserSubscriptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<UserSubscription>(entity =>
            {
                entity.ToTable("UserSubscription");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ChatId).HasColumnName("chatId");

                entity.Property(e => e.Symbol)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("symbol");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        void IDbContext.SaveChanges()
        {
           base.SaveChanges();
        }
    }
}

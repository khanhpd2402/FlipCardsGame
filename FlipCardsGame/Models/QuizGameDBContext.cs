using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace FlipCardsGame.Models
{
    public partial class QuizGameDBContext : DbContext
    {
        public QuizGameDBContext()
        {
        }

        public QuizGameDBContext(DbContextOptions<QuizGameDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<QuizItem> QuizItems { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                                          .SetBasePath(Directory.GetCurrentDirectory())
                                          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyCnn"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuizItem>(entity =>
            {
                entity.HasKey(e => e.QuestionId)
                    .HasName("PK__QuizItem__0DC06F8CEEDEF3DC");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

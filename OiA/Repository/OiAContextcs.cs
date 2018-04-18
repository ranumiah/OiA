using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace OiA.Repository
{
    public class OiAContextcs : DbContext
    {
        public DbSet<FileDetail> FileSystem { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($"Server = {Environment.MachineName}; Database = OiA; Trusted_Connection = True; ");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FileDetail>()
                .HasIndex(x => x.Md5Hash)
                .IsUnique(false)
                .ForSqlServerIsClustered(false);

            modelBuilder.Entity<FileDetail>()
                .HasIndex(x => x.Sha256Hash)
                .IsUnique(false)
                .ForSqlServerIsClustered(false);

            modelBuilder.Entity<FileDetail>()
                .HasIndex(x => x.Sha512Hash)
                .IsUnique(false)
                .ForSqlServerIsClustered(false);
        }
    }
}

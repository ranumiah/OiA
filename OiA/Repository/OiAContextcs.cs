using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace OiA.Repository
{
    public class OiAContextcs : DbContext
    {
        public static readonly LoggerFactory MyConsoleLoggerFactory = new LoggerFactory(new[]
        {
            new ConsoleLoggerProvider((category, level)
                => category == DbLoggerCategory.Database.Command.Name
                   && level == Microsoft.Extensions.Logging.LogLevel.Information, true)
        });

        public DbSet<FileDetail> FileSystem { get; set; }
        public DbSet<PendingFile> PendingFile { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                //.UseLoggerFactory(MyConsoleLoggerFactory)
                //.EnableSensitiveDataLogging(true)
                .UseSqlServer($"Server = {Environment.MachineName}; Database = OiA; Trusted_Connection = True; ");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FileDetail>()
                .HasIndex(x => x.Md5Hash)
                .IsUnique(false)
                .ForSqlServerIsClustered(false);
        }
    }
}

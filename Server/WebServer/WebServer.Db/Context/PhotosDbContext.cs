using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebServer.Db.Models;

namespace WebServer.Db.Context
{
    public class PhotosDbContext : DbContext
    {
        //Constants
        private const string DbType = "Postgres";
        // ReSharper disable once IdentifierTypo
        private const string AppsettingsJsonFileName = "appsettings.json";
        
        //Database sets
        public DbSet<MediasFileInfo> MediasFileInfos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public PhotosDbContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(AppsettingsJsonFileName)
                    .Build();
                var connectionString = configuration.GetConnectionString(DbType);
                optionsBuilder.UseNpgsql(connectionString);
            }
            optionsBuilder.UseNpgsql();
        }
    }
}
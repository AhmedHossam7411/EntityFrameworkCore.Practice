using EFcore.data.Configurations;
using EFcore.domain; // domain project
using EFcore.domain;
using Microsoft.EntityFrameworkCore;  //EF core package
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;


namespace EFcore.data
{

    public class FootballLeagueDbContext : DbContext  // code embodiment of db
    {

        public FootballLeagueDbContext(DbContextOptions<FootballLeagueDbContext> options) : base(options)
        {

        }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<league> leagues { get; set; }
        public DbSet<TeamsAndLeaguesView> TeamsAndLeaguesView { get; set; }

        public string dbPath { get; set; }

        //start by typing override
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TeamConfigurations());         // Apply the TeamConfigurations class to configure the Team entity
            modelBuilder.ApplyConfiguration(new leagueConfigurations());  // Apply the leagueConfigurations class to configure the Team entity
            modelBuilder.Entity<TeamsAndLeaguesView>(entity =>
            {
                entity.HasNoKey(); // This is a view, so it doesn't have a primary key
                entity.ToView("LeagueDetails"); // Specify the name of the view in the database
            });
        }
    }
    public class FootballLeagueDbContextFactory : IDesignTimeDbContextFactory<FootballLeagueDbContext>
    {
        public FootballLeagueDbContext CreateDbContext(string[] args)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var dbPath = Path.Combine(path, configuration.GetConnectionString("SqliteDatabaseConnectionString"));

            var optionsBuilder = new DbContextOptionsBuilder<FootballLeagueDbContext>();
            optionsBuilder.UseSqlite($"Data Source={dbPath}");

            return new FootballLeagueDbContext(optionsBuilder.Options);
        }
    }
}

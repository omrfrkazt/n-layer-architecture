using Entities;
using Entities.StaticTables.Menu;
using Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ExampleConnection"));
        }

        public virtual DbSet<GenUser> Users { get; set; }
        public virtual DbSet<GenRoles> Roles { get; set; }
        public virtual DbSet<GenRolesAuthories> RolesAuthories { get; set; }
        public virtual DbSet<GenRolesAndUsers> RolesAndUsers { get; set; }
        public virtual DbSet<GenMenus> Menus { get; set; }
    }
}

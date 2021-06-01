using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiGames_Demo.Models;

namespace WebApiGames_Demo.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public DbSet<Category> Categories { get; set; }
        public DbSet<Game> Games { get; set; }


    }
}

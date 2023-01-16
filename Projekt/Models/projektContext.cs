using System;
using Microsoft.EntityFrameworkCore;

namespace Projekt.Models
{
    public partial class projektContext : DbContext
    {
        public projektContext(DbContextOptions<projektContext> options) : base(options) { }
        public DbSet<Projekt.Models.Account> Accounts { get; set; }
        public DbSet<Projekt.Models.User> Users { get; set; }
        public DbSet<Projekt.Models.Publisher> Publishers { get; set; }
        public DbSet<Projekt.Models.Genre> Genres { get; set; }
        public DbSet<Projekt.Models.Game> Games { get; set; }
        public DbSet<Projekt.Models.Review> Reviews { get; set; }

    }
}

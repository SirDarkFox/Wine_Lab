using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Wine_Lab.Data.Models;

namespace Wine_Lab.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public override DbSet<User> Users { get; set; }
        public DbSet<Regulation> Regulations { get; set; }
        public DbSet<Article> Articles { get; set; }
    }
}

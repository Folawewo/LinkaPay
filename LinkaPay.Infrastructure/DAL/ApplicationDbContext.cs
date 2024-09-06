using System;
using Microsoft.EntityFrameworkCore;    
using LinkaPay.Domain.Entities;
using System.Collections.Generic;

namespace LinkaPay.Infrastructure.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
    }
}


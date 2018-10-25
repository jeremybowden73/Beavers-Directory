using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BeaversDirectory.Models
{
    public class BeaversDbContext : IdentityDbContext<IdentityUser>
    {
        public BeaversDbContext(DbContextOptions<BeaversDbContext> options) : base(options)
        {
        }

        // specify the names of the db tables to create
        public DbSet<Beaver> Beavers { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Enquire> Enquires { get; set; }
    }
}

using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using Streetunes.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;


namespace Streetunes.Data
{
    public class ApplicationDBcontext : IdentityDbContext<AppUser>
    {

      
            public ApplicationDBcontext(DbContextOptions<ApplicationDBcontext> options) : base(options)
            {
           
            }
            public DbSet<Event> Events { get; set; }
            public DbSet<Address> Addresses { get; set; }
            public DbSet<Comment> Comments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Many-to-Many between AppUser and Club
            modelBuilder.Entity<Event>()
                .HasMany(c => c.Followers)
                .WithMany(u => u.Events)
                .UsingEntity(j => j.ToTable("EventFollower"));
        }


    }
}

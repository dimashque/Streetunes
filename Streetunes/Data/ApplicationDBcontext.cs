using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using Streetunes.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;


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

            modelBuilder.Entity<AppUser>()
        .HasMany(e => e.CreatedEvents)
        .WithOne(e => e.Owner)
        .HasForeignKey(e => e.OwnerId).OnDelete(DeleteBehavior.Restrict); ;

            modelBuilder.Entity<Event>()
         .HasMany(e => e.Followers)
         .WithMany(u => u.Events)
         .UsingEntity<Dictionary<string, object>>(
             "EventFollowers",
             j => j
                 .HasOne<AppUser>() // ✅ Fix: Reference the correct entity (AppUser)
                 .WithMany()
                 .HasForeignKey("UserId")
                 .OnDelete(DeleteBehavior.Restrict), // ✅ Prevent cascade delete
             j => j
                 .HasOne<Event>() // ✅ Fix: Reference the correct entity (Event)
                 .WithMany()
                 .HasForeignKey("EventId")
                 .OnDelete(DeleteBehavior.Restrict) // ✅ Prevent cascade delete
         );

            modelBuilder.Entity<Event>()
           .HasOne(e => e.Address) // An Event has one Address
           .WithMany(a => a.Events) // An Address can have many Events
           .HasForeignKey(e => e.AddressId) // Foreign Key in Event for Address
           .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Event>()
        .HasMany(e => e.Comments)
        .WithOne(e => e.StreetEvent)
        .HasForeignKey(e => e.EventId);


        }

    }
}



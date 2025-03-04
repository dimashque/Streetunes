using Streetunes.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Streetunes.Data.Enum;

namespace Streetunes.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDBcontext>();

                context.Database.EnsureCreated();

                // Seeding Address Data
               
                    context.Addresses.AddRange(new List<Address>
            {
                new Address
                {
                    City = "New York",
                    Region = "NY",
                    PostalCode = "10001",
                    street = "789 Broadway Ave",
                    Country = "USA"
                },
                new Address
                {
                    City = "San Francisco",
                    Region = "CA",
                    PostalCode = "94105",
                    street = "101 Market St",
                    Country = "USA"
                },
                new Address
                {
                    City = "Los Angeles",
                    Region = "CA",
                    PostalCode = "90001",
                    street = "555 Sunset Blvd",
                    Country = "USA"
                }
            });
                    context.SaveChanges();
                

                // Seeding Event Data
               
                    var address1 = context.Addresses.FirstOrDefault(a => a.City == "New York");
                    var address2 = context.Addresses.FirstOrDefault(a => a.City == "San Francisco");
                    var address3 = context.Addresses.FirstOrDefault(a => a.City == "Los Angeles");

                    context.Events.AddRange(new List<Event>
            {
                new Event
                {
                    Title = "Tech Conference 2025",
                    Date = DateTime.Now.AddMonths(3),
                    Image = "https://example.com/techconference.jpg",
                    Description = "A premier tech conference featuring top speakers and innovations.",
                    AddressId = address1.AddressId,  // Using AddressId from seeded address
                    EventCategory = EventCategory.Rock,
                    OwnerId = "d1d73ccc-ec2b-4461-85cc-f2384c3d59ea"  // Assume this ID exists in AppUser
                },
                new Event
                {
                    Title = "Startup Pitch Night",
                    Date = DateTime.Now.AddMonths(2),
                    Image = "https://example.com/startuppitch.jpg",
                    Description = "A night for startups to pitch their ideas to investors.",
                    AddressId = address2.AddressId,  // Using AddressId from seeded address
                    EventCategory = EventCategory.Acustic,
                    OwnerId = "d1d73ccc-ec2b-4461-85cc-f2384c3d59ea"  // Assume this ID exists in AppUser
                },
                new Event
                {
                    Title = "Movie Premiere: 'The Future'",
                    Date = DateTime.Now.AddMonths(4),
                    Image = "https://example.com/moviepremiere.jpg",
                    Description = "Join us for the premiere of the highly anticipated sci-fi movie.",
                    AddressId = address3.AddressId,  // Using AddressId from seeded address
                    EventCategory = EventCategory.Performance,
                    OwnerId = "d45be0ef-750b-405b-ab64-2b094a0cdb47"  // Assume this ID exists in AppUser
                }
            });
                    context.SaveChanges();
                

                // Seeding Comments Data
                
                    var event1 = context.Events.FirstOrDefault(e => e.Title == "Tech Conference 2025");
                    var event2 = context.Events.FirstOrDefault(e => e.Title == "Startup Pitch Night");
                    var event3 = context.Events.FirstOrDefault(e => e.Title == "Movie Premiere: 'The Future'");

                    // Assuming AppUser exists with these IDs
                    context.Comments.AddRange(new List<Comment>
            {
                new Comment
                {
                    CommentText = "Excited to learn from industry leaders!",
                    CreatedDate = DateTime.Now,
                    CommentorId = "d45be0ef-750b-405b-ab64-2b094a0cdb47",  // Assuming "app-user" exists
                    EventId = event1.Id
                },
                new Comment
                {
                    CommentText = "This is a great opportunity for startups to shine!",
                    CreatedDate = DateTime.Now,
                    CommentorId = "d45be0ef-750b-405b-ab64-2b094a0cdb47",  // Assuming "app-user" exists
                    EventId = event2.Id
                },
                new Comment
                {
                    CommentText = "Can't wait to see the movie!",
                    CreatedDate = DateTime.Now,
                    CommentorId = "d1d73ccc-ec2b-4461-85cc-f2384c3d59ea",  // Assuming "app-user" exists
                    EventId = event3.Id
                }
            });
                    context.SaveChanges();
                
            }
        }


        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "admin@streetunes.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "adminuser",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Password123!");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@streetunes.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "appuser",
                        Email = appUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "Password123!");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}

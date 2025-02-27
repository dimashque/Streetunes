using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using Streetunes.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Streetunes.Data
{
    public class ApplicationDBcontext : IdentityDbContext<AppUser>
    {

      
            public ApplicationDBcontext(DbContextOptions<ApplicationDBcontext> options) : base(options)
            {
            }
            public DbSet<Event> Events { get; set; }
            
        
    }
}

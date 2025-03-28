﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Streetunes.Data;
using Streetunes.Models;
using System.Diagnostics.Eventing.Reader;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace Streetunes.Repository
{
    public class EventRepository : IEventReposiroty
    {
        private readonly ApplicationDBcontext _context; 

        public EventRepository(ApplicationDBcontext context)
        {
            _context = context;
        }

        public bool Add(Event streetEvent)
        {
            _context.Add(streetEvent);
            return Save();
        }

       public bool Delete(Event streetEvent)
        {
            _context.Remove(streetEvent);
            return Save();
        }

        public async Task<IEnumerable<Event>> GetAll()
        {
            return await _context.Events.Include(a => a.Address).ToListAsync();
        }

        public async Task<Event> GetByIdAsync(int id)
        {
            return await _context.Events.Include(a => a.Address).Include(o => o.Owner).Include(e => e.Followers).Include(c => c.Comments).ThenInclude(p => p.Commentor).FirstOrDefaultAsync(x => x.Id == id);  
                    // Eagerly load the related Address
                      // Eagerly load the related Owner (AppUser)
         // Eagerly load the related Comments


            //.ThenInclude(p => p.Commentor)   // Eagerly load the related Commentor (AppUser)

        }
        public async Task<Event> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Events.Include(a => a.Address).Include(o => o.Owner).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Event>> GetEventByCity(string city)
        {
            return await _context.Events.Where(e => e.Address.City.Contains(city)).ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetEventByRegion(string region)
        {
            return await _context.Events.Where(e => e.Address.Region.Contains(region)).ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetEventByPLZ(string plz)
        {
            return await _context.Events.Where(e => e.Address.PostalCode.Contains(plz)).ToListAsync();
        }

        public async Task<AppUser> GetCurrentUser(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public bool AddComment(Comment comment)
        {
             _context.Comments.Add(comment);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Event streetEvent)
        {
            _context.Update(streetEvent);
            return Save();
        }
    }
}

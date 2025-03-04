
using Streetunes.Interfaces;

using Microsoft.EntityFrameworkCore;
using Streetunes.Data;
using Streetunes.Models;

namespace Streetunes.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBcontext _context;

        public UserRepository(ApplicationDBcontext context) { _context = context; }
        public bool Add(AppUser user)
        {
            throw new NotImplementedException();
        }

        public bool Delete(AppUser user)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AppUser>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<AppUser> GetById(string id)
        {
            return await _context.Users
        .Include(u => u.CreatedEvents)
            .ThenInclude(e => e.Address) // Ensuring Address is loaded
        .Include(u => u.Events)
            .ThenInclude(e => e.Address)
        .FirstOrDefaultAsync(x => x.Id == id);


        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(AppUser user)
        {
            throw new NotImplementedException();
        }
    }
}
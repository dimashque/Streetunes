using Streetunes.Models;
using Microsoft.AspNetCore.Components.Web;


namespace Streetunes.Interfaces
{
    public interface IUserRepository
    {

        Task<IEnumerable<AppUser>> GetAllUsers();

        Task<AppUser> GetById(string id);

        bool Add(AppUser user);
        bool Update(AppUser user);
        bool Delete(AppUser user);

        bool Save();
    }
}
using Microsoft.AspNetCore.Mvc;
using Streetunes.Interfaces;
using Streetunes.Models;
using Streetunes.Repository;
using Streetunes.Api.Dto;

namespace Streetunes.Api
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserApiController(IUserRepository userRepository) { 
            _userRepository = userRepository;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<AppUserDto>> GetUser(string id)
        {
            var appUser = await _userRepository.GetById(id);

            var userDto = new AppUserDto
            {
                Id = appUser.Id,
                UserName = appUser.UserName,
                AvatarUrl = appUser.AvatarUrl,
            };
            return Ok(userDto);
        }

    }
}

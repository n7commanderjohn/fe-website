using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using FEWebsite.API.Data.BaseServices;
using AutoMapper;
using FEWebsite.API.DTOs.UserDTOs;
using System.Collections.Generic;

namespace FEWebsite.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserInfoRepository RepoUserInfo { get; }

        private IMapper Mapper { get; }

        public UsersController(IUserInfoRepository repoUserInfo, IMapper mapper)
        {
            this.RepoUserInfo = repoUserInfo;
            this.Mapper = mapper;
        }

        // GET api/users
        [HttpGet]
        public async Task<OkObjectResult> GetUsers()
        {
            var users = await this.RepoUserInfo
                .GetUsers()
                .ConfigureAwait(false);

            var usersDto = this.Mapper.Map<IEnumerable<UserForListDto>>(users);

            return this.Ok(usersDto);
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public async Task<OkObjectResult> GetUser(int id)
        {
            var user = await this.RepoUserInfo
                .GetUser(id)
                .ConfigureAwait(false);

            var userDto = this.Mapper.Map<UserForDetailedDto>(user);

            return this.Ok(userDto);
        }

        // POST api/users
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // PUT api/users/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}

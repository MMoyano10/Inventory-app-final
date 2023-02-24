using InventoryApp.DTO;
using InventoryApp.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace InventoryApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DBContext DBContext;

        public UserController(DBContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> LoginUser(LoginDTO login) 
        {
            var user = await DBContext.Users.FirstOrDefaultAsync(x => x.UserName == login.UserName.ToLower());

            if (user == null) return Unauthorized("Invalid username");
            else if( user.PasswordHash == login.Password) return Ok(user);
            return Ok();
        }


            [HttpGet("GetUsers")]
        public async Task<ActionResult<List<UserDTO>>> Get()
        {
            var List = await DBContext.Users.Select(
                s => new UserDTO
                {
                    IdUser = s.IdUser,
                    UserName = s.UserName,
                    PasswordHash = s.PasswordHash,
                    PasswordSalt = s.PasswordSalt,
                    Created = s.Created
                }
            ).ToListAsync();

            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }

        [HttpGet("GetUserById")]
        public async Task<ActionResult<UserDTO>> GetUserById(int Id)
        {
            UserDTO User = await DBContext.Users.Select(
                    s => new UserDTO
                    {
                        IdUser = s.IdUser,
                        UserName = s.UserName,
                        PasswordHash = s.PasswordHash,
                        PasswordSalt = s.PasswordSalt,
                        Created = s.Created
                    })
                .FirstOrDefaultAsync(s => s.IdUser == Id);

            if (User == null)
            {
                return NotFound();
            }
            else
            {
                return User;
            }
        }

        [HttpPost("InsertUser")]
        public async Task<HttpStatusCode> InsertUser(UserDTO User)
        {
            var entity = new User()
            {
                IdUser = User.IdUser,
                UserName = User.UserName,
                PasswordHash = User.PasswordHash,
                PasswordSalt = User.PasswordSalt,
                Created = User.Created
            };

            DBContext.Users.Add(entity);
            await DBContext.SaveChangesAsync();

            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateUser")]
        public async Task<HttpStatusCode> UpdateUser(UserDTO User)
        {
            var entity = await DBContext.Users.FirstOrDefaultAsync(s => s.IdUser == User.IdUser);

            entity.UserName = User.UserName;
            entity.PasswordHash = User.PasswordHash;
            entity.PasswordSalt = User.PasswordSalt;
            entity.Created = User.Created;

            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeleteUser/{Id}")]
        public async Task<HttpStatusCode> DeleteUser(int Id)
        {
            var entity = new User()
            {
                IdUser = Id
            };
            DBContext.Users.Attach(entity);
            DBContext.Users.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}

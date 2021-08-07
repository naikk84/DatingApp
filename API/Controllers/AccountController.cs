using System.Threading.Tasks;
using API.Data;
using Microsoft.AspNetCore.Mvc;
using API.Entities;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers

{
    public class AccountController : BaseApiController

    {
        private readonly DataContext _context;
       public AccountController(DataContext context)
       {
           _context= context;
       } 
       [HttpPost("register")]
       public async Task<ActionResult<AppUser>> Register(string username, string Password){
           using var hmac = new HMACSHA512();

           var user = new AppUser
           {
               UserName = username,
               PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Password)),
               PasswordSalt= hmac.Key,
           };
           _context.Users.Add(user);
           await _context.SaveChangesAsync();
           return user;
       }
       
    }
}
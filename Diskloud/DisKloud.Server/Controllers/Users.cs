using DisKloud.Server.Contexts;
using DisKloud.Server.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt;

namespace DisKloud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Users : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly string SercretKey;
        public Users(AppDbContext context,IConfiguration conf) 
        {
            _context = context;
            SercretKey = conf["SercretKey"];

        }

        [HttpGet]
        public IActionResult Get(string username, string password)
        {
            if(!_context.Users.Where(u => u.Name == username).Any()) return NotFound("user not found");

            Model.Users DbUser = _context.Users.Where(u => u.Name == username).First();

            if (EncryptProvider.Sha256(password) != DbUser.password) return StatusCode(403);

            ApiKey newKey = new ApiKey(10);

            _context.ApiKey.Add(newKey);
            _context.SaveChanges();

            return Ok(newKey.Key);

        }



        [HttpPost]
        public IActionResult CreateNewUser(string username, string password ,string password2 , string sercretKey)
        {
            if (sercretKey != SercretKey) return StatusCode(403);
            if (password != password2 ) return BadRequest("password don't match");
            if (username == null || password == null || password2 == null) return BadRequest("user or password is null");
            if (_context.Users.Where(u => u.Name == username).Any()) return BadRequest("user already exist");

            _context.Users.Add(new Model.Users(username, EncryptProvider.Sha256(password)));
            _context.SaveChanges();


            return Ok();

        }

        
    }
}

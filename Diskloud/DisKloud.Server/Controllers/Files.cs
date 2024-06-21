using DisKloud.Server.Contexts;
using DisKloud.Server.Model;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.IO.Pipes;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DisKloud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Files : ControllerBase
    {
        private AppDbContext _dbContext;

        public Files(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/<Files>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<Files>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Files>
        [HttpPost]
        public async Task<IActionResult> Post(IFormFile file ,string path, Guid UserID)
        {
            FileData data;
            /*IQueryable<FileData> Bddata = _dbContext.FileData.Where(f => f.Name == file.Name && f.path == path);
            if (Bddata.Count() > 0)
            {
                data = Bddata.First();
            }
            else
            {*/


            Users? fileOwner = _dbContext.Users.Find(UserID) ;

            if (fileOwner == null) throw new Exception("User Not Found");

            data = new FileData(file,path, fileOwner);
            _dbContext.FileData.Add(data);

            FileStream newfile = System.IO.File.Create($".\\Files\\{data.Id}");
            file.CopyTo(newfile);
            newfile.Close();





            await _dbContext.SaveChangesAsync();
           
            return Ok(data.Id);

        }

        
        // PUT api/<Files>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Files>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

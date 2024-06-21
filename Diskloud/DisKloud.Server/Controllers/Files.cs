using DisKloud.Server.Contexts;
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
        public AppDbContext test;

        public Files(AppDbContext a)
        {
            test = a;
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
        public IActionResult Post(IFormFile file ,string name)
        {
            string dir = $"./{name}./";
            if (! Directory.Exists(dir)) System.IO.Directory.CreateDirectory(dir);
            FileStream newfile = System.IO.File.Create($"{dir}\\{file.FileName}");
            file.CopyTo(newfile);
            newfile.Close();
            return Ok();

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

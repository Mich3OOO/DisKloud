using DisKloud.Server.Contexts;
using DisKloud.Server.Model;
using Microsoft.AspNetCore.Http;
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

        private readonly string FilesPath;
        private AppDbContext _dbContext;

        public Files(AppDbContext dbContext,IConfiguration conf)
        {
            _dbContext = dbContext;
            FilesPath = conf["FilesPath"] + "/";
            Directory.CreateDirectory(conf["FilesPath"]);
        }

        // GET: api/<Files>
        [HttpGet("data/{FileId}")]
        public IActionResult GetbyId(Guid FileId)
        {
            if (!chekKey()) return StatusCode(403) ;
            return Ok(_dbContext.FileData.Find(FileId));
        }


        [HttpGet("data/User/{UserId}")]
        public IActionResult GetbyOwner(Guid UserId)
        {
            if (!chekKey()) return StatusCode(403);
            return Ok(_dbContext.FileData.Where(f=>f.Owner.Id == UserId));
        }


        [HttpGet("{FileId}")]
        public IActionResult Get(Guid FileId)
        {
            if (!chekKey()) return StatusCode(403);
            FileData? filedata = _dbContext.FileData.Find(FileId);

            if(filedata == null) return NotFound();

            FileStream file = System.IO.File.Open(FilesPath + FileId.ToString(), FileMode.Open);


            return File(file, filedata.ContentType, filedata.Name);
        }

        // POST api/<Files>
        [HttpPost]
        public async Task<IActionResult> Post(IFormFile file ,string path, Guid UserID)
        {
            if (!chekKey()) return StatusCode(403);
            FileData data;



            Model.Users? fileOwner = _dbContext.Users.Find(UserID) ;

            if (fileOwner == null) throw new Exception("User Not Found");

            data = new FileData(file,path, fileOwner);
            _dbContext.FileData.Add(data);

            FileStream newfile = System.IO.File.Create(FilesPath + data.Id.ToString());
            await file.CopyToAsync(newfile);
            newfile.Close();



            await _dbContext.SaveChangesAsync();
           
            return Ok(data.Id);

        }

        
        // PUT api/<Files>/5
        [HttpPut]
        public async Task<IActionResult> Put(Guid FileId, IFormFile file, string path)
        {
            if (!chekKey()) return StatusCode(403);
            FileData? localdata = _dbContext.FileData.Find(FileId);
            if (localdata == null) return NotFound();



            FileStream localfile =  System.IO.File.OpenWrite(FilesPath + FileId.ToString());

            

            await file.CopyToAsync(localfile);

            localfile.Dispose();
            localfile.Close();

            localdata.update(file, path);

            _dbContext.Update(localdata);
            _dbContext.SaveChanges();
            return Ok();

        }

        // DELETE api/<Files>/5
        [HttpDelete]
        public IActionResult Delete(Guid FileId)
        {
            if (!chekKey()) return StatusCode(403);
            FileData? localdata = _dbContext.FileData.Find(FileId);
            if (localdata == null) return NotFound();

            System.IO.File.Delete(FilesPath + FileId.ToString());

            _dbContext.Remove(localdata);
            _dbContext.SaveChanges();
            return Ok();
        }

        private bool chekKey()
        {
            ApiKey.GarbageCollector(_dbContext);
            return ApiKey.ValidateKey(_dbContext, Request.Headers["DisKloud-Key"]);
        }
    }
}

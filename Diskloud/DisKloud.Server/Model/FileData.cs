namespace DisKloud.Server.Model
{
    public class FileData
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public DateTime VersionDate { get; set; }
        public string Path {  get; set; }

        public Users Owner { get; set; }

        public string ContentType { get; set; }

        public FileData()
        {

        }
        public FileData(IFormFile file,string filePath, Users user)
        {
            Id = Guid.NewGuid();
            Name = file.FileName;
            VersionDate = DateTime.Parse(file.Headers.LastModified);

            if (VersionDate == null) VersionDate = DateTime.UtcNow;

            Path = filePath;
            Owner =user;
            ContentType = file.ContentType;
        }

        public void update(IFormFile file, string filePath)
        {
            
            Name = file.FileName;
            VersionDate = DateTime.Parse(file.Headers.LastModified);

            if (VersionDate == null) VersionDate = DateTime.UtcNow;

            Path = filePath;
            ContentType = file.ContentType;


        }
    }

}

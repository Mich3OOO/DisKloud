namespace DisKloud.Server.Model
{
    public class FileData
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public DateTime VersionDate { get; set; }
        public string path {  get; set; }

        public Users Owner { get; set; }

        public string ContentType { get; set; }

        public FileData()
        {

        }
        public FileData(IFormFile file,string filePath, Users user)
        {
            Id = Guid.NewGuid();
            Name = file.FileName;
            VersionDate = DateTime.UtcNow;
            path = filePath;
            Owner =user;
            ContentType = file.ContentType;
        }

    }

}

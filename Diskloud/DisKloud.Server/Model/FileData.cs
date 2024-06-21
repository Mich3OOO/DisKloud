namespace DisKloud.Server.Model
{
    public class FileData
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string VersionDate { get; set; }
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
            VersionDate = file.Headers.LastModified;

            if (VersionDate == null) VersionDate = DateTime.Now.ToString();

            path = filePath;
            Owner =user;
            ContentType = file.ContentType;
        }

        public void update(IFormFile file, string filePath)
        {
            
            Name = file.FileName;
            VersionDate = file.Headers.LastModified;

            if (VersionDate == null) VersionDate = DateTime.Now.ToString();

            path = filePath;
            ContentType = file.ContentType;


        }
    }

}

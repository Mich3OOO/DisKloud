namespace DisKloud.Server.Model
{
    public class FileData
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public DateTime VersionDate { get; set; }
        public string path {  get; set; }

        public Users Owner { get; set; }
    }
}

namespace DisKloud.Server.Model
{
    public class Users
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string password { get; set; }

        public Users() { }
        public Users(string name, string password)
        {
            Id = Guid.NewGuid();
            Name = name;
            this.password = password;
        }
    }
}

using DisKloud.Server.Contexts;

namespace DisKloud.Server.Model
{
    public class ApiKey
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public DateTime expiredate { get; set; }

        public ApiKey() { }
        public ApiKey(int exTime) 
        {
            Id = Guid.NewGuid();
            Key = "";
            Random ran = new Random();
            for (int i = 0; i < 256; i++)
            {
                Key += Convert.ToChar( ran.Next(32,126));
            }
            expiredate = DateTime.UtcNow.Add(TimeSpan.FromMinutes(exTime)) ;
        }
        public void renovateExpireDate(int exTime)
        {
            expiredate = DateTime.UtcNow.Add(TimeSpan.FromMinutes(exTime));
        }


        public static bool ValidateKey(AppDbContext context, string apikey)
        {
            if (apikey == null || apikey.Length==0) return false;
            bool result;

            result = context.ApiKey.Where(a => a.Key == apikey).Any();

            if (result)
            {
                ApiKey key = context.ApiKey.Where(a => a.Key == apikey).First();
                key.renovateExpireDate(10);
                context.ApiKey.Update(key);
                context.SaveChanges();
            }


            return result;
        }

        public static void GarbageCollector(AppDbContext context)
        {
            context.ApiKey.RemoveRange(context.ApiKey.Where(a => a.expiredate <= DateTime.UtcNow));
            context.SaveChanges() ;
        }
    }
}

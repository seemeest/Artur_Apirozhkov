using Microsoft.EntityFrameworkCore;

namespace Artur_Apirozhkov.Data
{
    public class Artur_ApirozhkovContext : DbContext
    {
        public Artur_ApirozhkovContext(DbContextOptions options) : base(options)
        {
        }

        
    }
}

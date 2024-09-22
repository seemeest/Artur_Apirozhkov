using Microsoft.EntityFrameworkCore;

namespace Artur_Apirozhkov.BdModels
{
    public class Artur_ApirozhkovContext : DbContext
    {
        public Artur_ApirozhkovContext(DbContextOptions options) : base(options)
        {
        }

        
    }
}

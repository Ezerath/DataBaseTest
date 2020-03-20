using System.Data.Entity;

namespace DataBaseTest
{
    class MaterialContext : DbContext
    {
        public MaterialContext() : base("DefaultConnection")
        {

        }
        public DbSet<Material> Materials { get; set; }
        //public DbSet<Material> Egger { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace PARCIAL1A.Models
{
    public class Parcial1AContext
    {
        public class parcial1AContext : DbContext
        {
            public parcial1AContext(DbContextOptions<parcial1AContext> options) : base(options)
            {
            }
        }
    }
}

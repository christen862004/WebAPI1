using Microsoft.EntityFrameworkCore;

namespace WebAPI1.Models
{
    public class ITIContext:DbContext
    {
        public DbSet<Department>  Department { get; set; }
        public DbSet<Employee>  Employee { get; set; }

        public ITIContext(DbContextOptions<ITIContext> options):base(options)
        {
            
        }
    }
}

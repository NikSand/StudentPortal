using Microsoft.EntityFrameworkCore;
using StudentPortal.Web.Models.Entities;
namespace StudentPortal.Web.Data

{
    public class ApplicationDbCopntext : DbContext
    {
        public ApplicationDbCopntext(DbContextOptions<ApplicationDbCopntext> options) : base(options) 
        {
            
        }

        public DbSet<Student> Students { get; set; }
    }
}

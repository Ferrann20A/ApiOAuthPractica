using ApiOAuthPractica.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiOAuthPractica.Data
{
    public class DoctoresContext: DbContext
    {
        public DoctoresContext(DbContextOptions<DoctoresContext> options)
            :base(options) { }

        public DbSet<Doctor> Doctores { get; set; }
    }
}

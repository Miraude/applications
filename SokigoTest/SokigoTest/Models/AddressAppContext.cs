using Microsoft.EntityFrameworkCore;

namespace SokigoTest.Models
{
    public class AddressAppContext : DbContext
    {
        public AddressAppContext()
        {

        }
        public AddressAppContext(DbContextOptions<AddressAppContext> options)
            : base(options)
        {

        }

        public virtual DbSet<PersonsModel> Persons { get; set; } = null!;
    }
}

using SokigoTest.Models;

namespace SokigoTest.Services
{
    public class PersonService : IPersonService
    {
        private readonly AddressAppContext _context;
        public PersonService(AddressAppContext context)
        {
            _context = context;
        }

        public List<PersonsModel> GetAll()
        {
            return _context.Persons?.ToList();
        }
    }
}

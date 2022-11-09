using SokigoTest.Models;

namespace SokigoTest.Services
{
    public interface IPersonService
    {
        public List<PersonsModel> GetAll();
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SokigoTest.Models;

namespace SokigoTest.Controllers
{
    public class PersonController : Controller
    {
        private readonly  AddressAppContext _context;

        public PersonController(AddressAppContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<PersonsModel> objList = _context.Persons;

            return View(objList);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PersonsModel obj)
        {

            if (ModelState.IsValid)
            {

                _context.Persons.Add(obj);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();

        }

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SokigoTest.Models;
using SokigoTest.Services;

namespace SokigoTest.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonService personService;
        public List<PersonsModel> Persons { get; set; }
        public ActionResult Index()
        {
            Persons = personService.GetAll().ToList();

            return View(Persons);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                PersonsModel p = new();

                //p.FirstName = firstname
                //p.LastName = lastname
                //p.Address = address
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}

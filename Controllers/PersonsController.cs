using BazaOsobZaginionych.Models;
using BazaOsobZaginionych.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BazaOsobZaginionych.Controllers
{
    public class PersonsController : Controller
    {
        private readonly DatabaseContext dbContext;

        public PersonsController(DatabaseContext DbContext)
        {
            dbContext = DbContext;
        }

        [HttpGet]

        public async Task<IActionResult> Index(string searching)
        {
            var persons = from p in dbContext.Persons select p;

            if (!String.IsNullOrEmpty(searching))
            {
                persons = persons.Where(p => p.Gender.Contains(searching));
            }

            return View(persons.ToList());

        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Add(AddPersonViewModel addPersonRequest)
        {
            var person = new Person()
            {
                Id = Guid.NewGuid(),
                FirstName = addPersonRequest.FirstName,
                LastName = addPersonRequest.LastName,
                Age = addPersonRequest.Age,
                Gender = addPersonRequest.Gender,
                City = addPersonRequest.City,
                State = addPersonRequest.State,
            };

            await dbContext.Persons.AddAsync(person);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        [HttpGet]

        public async Task<IActionResult> View(Guid id)
        {
            var person = await dbContext.Persons.FirstOrDefaultAsync(x => x.Id == id);

            if (person != null)
            {
                var viewModel = new UpdatePersonViewModel()
                {
                    Id = person.Id,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Gender = person.Gender,
                    Age = person.Age,
                    City = person.City,
                    State = person.State,
                };
                return await Task.Run(()=> View("View",viewModel));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]

        public async Task<IActionResult> View(UpdatePersonViewModel model)
        {
            var person = await dbContext.Persons.FindAsync(model.Id);

            if(person != null)
            {
                person.FirstName = model.FirstName;
                person.LastName = model.LastName;
                person.Gender = model.Gender;
                person.Age = model.Age;
                person.City = model.City;
                person.State = model.State;

                await dbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]

        public async Task<IActionResult> Delete(UpdatePersonViewModel model)
        {
            var person = await dbContext.Persons.FindAsync(model.Id);

            if(person != null)
            {
                dbContext.Persons.Remove(person);
                await dbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]

        public async Task<IActionResult> Select(Guid id)
        {
            var person = await dbContext.Persons.FirstOrDefaultAsync(x => x.Id == id);

            if (person != null)
            {
                var viewModel = new UpdatePersonViewModel()
                {
                    Id = person.Id,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Gender = person.Gender,
                    Age = person.Age,
                    City = person.City,
                    State = person.State,
                };
                return await Task.Run(() => View("Select", viewModel));
            }

            return RedirectToAction("Index");
        }


    }
}

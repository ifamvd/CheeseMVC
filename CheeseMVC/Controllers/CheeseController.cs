using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using CheeseMVC.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {
        // additional database stuff
        private CheeseDbContext context;
        public CheeseController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Cheese> cheeses = context.Cheeses.ToList();
            return View(cheeses);
        }

        public IActionResult Add()
        {
            AddCheeseViewModel addCheeseViewModel = new AddCheeseViewModel();
            return View(addCheeseViewModel);
        }

        public IActionResult Delete()
        {
            ViewBag.cheeses = context.Cheeses.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddCheeseViewModel addCheeseViewModel)
        {
            if(ModelState.IsValid)
            {
                Cheese newCheese = new Cheese
                {
                    Name = addCheeseViewModel.Name,
                    Description = addCheeseViewModel.Description,
                    Type = addCheeseViewModel.Type
                };
                // database stuff
                context.Cheeses.Add(newCheese);
                context.SaveChanges();

                return Redirect("/Cheese");
            }
            return View(addCheeseViewModel);
        }

        [HttpPost]
        [Route("/Cheese/Delete")]
        public IActionResult RemoveCheese(int name)
        {
            Cheese toRemove = context.Cheeses.Single(c => c.ID == name);
            context.Cheeses.Remove(toRemove);
            context.SaveChanges();
            return Redirect("/Cheese");
        }

        [HttpPost]
        public IActionResult RemoveCheeseMultiple(int[] names)
        {
            foreach(int name in names)
            {
                // database stuff
                Cheese theCheese = context.Cheeses.Single(c => c.ID == name);
                context.Cheeses.Remove(theCheese);
            }

            // database stuff
            context.SaveChanges();

            return Redirect("/Cheese");
        }

        public IActionResult Edit(int cheeseId)
        {
            ViewBag.cheese = context.Cheeses.Single(c => c.ID == cheeseId);
            context.SaveChanges();
            return View();
        }

        [HttpPost]
        public IActionResult Edit(int cheeseId, string name, string description)
        {
            // query CheeseData for the cheese with the given id, and then update its name and description. Redirect the user to the home page.
            Cheese cheese = context.Cheeses.Single(c => c.ID == cheeseId);
            cheese.Name = name;
            cheese.Description = description;
            context.Cheeses.Update(cheese);
            context.SaveChanges();
            return Redirect("/Cheese");
        }

    }
}

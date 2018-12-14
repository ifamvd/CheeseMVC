using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {
        

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Cheese> cheeses = CheeseData.GetAll();
            return View(cheeses);
        }

        public IActionResult Add()
        {
            AddCheeseViewModel addCheeseViewModel = new AddCheeseViewModel();
            return View(addCheeseViewModel);
        }

        public IActionResult Delete()
        {
            ViewBag.cheeses = CheeseData.GetAll();
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
                CheeseData.Add(newCheese);
                return Redirect("/Cheese");
            }
            return View(addCheeseViewModel);
        }

        [HttpPost]
        [Route("/Cheese/Delete")]
        public IActionResult RemoveCheese(int name)
        {
            CheeseData.Remove(name);
            return Redirect("/Cheese");
        }

        [HttpPost]
        public IActionResult RemoveCheeseMultiple(int[] names)
        {
            foreach(int name in names)
            {
                CheeseData.Remove(name);
            }
            return Redirect("/Cheese");
        }

        public IActionResult Edit(int cheeseId)
        {
            ViewBag.cheese = CheeseData.GetById(cheeseId);
            return View();
        }

        [HttpPost]
        public IActionResult Edit(int cheeseId, string name, string description)
        {
            // query CheeseData for the cheese with the given id, and then update its name and description. Redirect the user to the home page.
            Cheese cheese = CheeseData.GetById(cheeseId);
            cheese.Name = name;
            cheese.Description = description;
            return Redirect("/Cheese");
        }

    }
}

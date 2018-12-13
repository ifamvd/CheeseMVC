using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {
        static private List<Cheese> Cheeses = new List<Cheese>();

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.cheeses = Cheeses;
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Delete()
        {
            ViewBag.cheeses = Cheeses;
            return View();
        }

        [HttpPost]
        [Route("/Cheese/Add")]
        public IActionResult NewCheese(string name, string desc)
        {
            // Add new cheese to existing cheese list
            Cheeses.Add(new Cheese(name, desc));
            return Redirect("/Cheese");
        }

        [HttpPost]
        [Route("/Cheese/Delete")]
        public IActionResult RemoveCheese(string name)
        {
            var item = Cheeses.SingleOrDefault(x => x.Name == name);
            if (item != null) Cheeses.Remove(item);
            return Redirect("/Cheese");
        }

        [HttpPost]
        public IActionResult RemoveCheeseMultiple(string[] names)
        {
            foreach(string name in names)
            {
                var item = Cheeses.SingleOrDefault(x => x.Name == name);
                if (item != null) Cheeses.Remove(item);
            }
            return Redirect("/Cheese");
        }

    }

    public class Cheese
    {
        public string Name { get; }
        public string Description { get; }

        public Cheese(string name, string desc)
        {
            Name = name;
            if (string.IsNullOrEmpty(desc))
            {
                desc = "N/A";
            }
            Description = desc;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {
        

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.cheeses = CheeseData.GetAll();
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Delete()
        {
            ViewBag.cheeses = CheeseData.GetAll();
            return View();
        }

        [HttpPost]
        [Route("/Cheese/Add")]
        public IActionResult NewCheese(Cheese newCheese)
        {
            CheeseData.Add(newCheese);
            return Redirect("/Cheese");
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

    }
}

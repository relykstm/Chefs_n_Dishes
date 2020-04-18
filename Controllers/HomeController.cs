using System;
using Microsoft.AspNetCore.Mvc;
using Chefs_n_Dishes.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Chefs_n_Dishes.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;

        public HomeController(MyContext context)
        {
            dbContext = context;
        }
        
        [HttpGet("")]
        public ViewResult Index()
        {
            List<Chef> AllChefs = dbContext.Chefs.Include(c=> c.Dishes).ToList();
            return View(AllChefs);
        }

        [HttpGet("dishes")]
        public ViewResult Dishes()
        {
            List<Dish> AllDishes = dbContext.Dishes.Include(d=> d.Chef).ToList();
            return View(AllDishes);
        }

        [HttpGet("new")]
        public ViewResult AddaChef()
        {
            return View();
        }
        [HttpGet("dishes/new")]
        public ViewResult  AddaDish()
        {
            ViewBag.Chefs = dbContext.Chefs;
            return View();
        }

        [HttpPost("newchef")]
        public IActionResult NewChef(Chef fromForm)
        {
            if(ModelState.IsValid)
            {
                dbContext.Add(fromForm);
                dbContext.SaveChanges();
            } 
            else
            {
                return View("AddaChef");
            }
            List<Chef> AllChefs = dbContext.Chefs.Include(c=> c.Dishes).ToList();
            return View("Index",AllChefs);
        }

        [HttpGet("delete/{ChefId}")]
        public RedirectToActionResult DeleteChef(int ChefId)
        {
            Chef ToDelete = dbContext.Chefs.FirstOrDefault(c => c.ChefId == ChefId);
            dbContext.Chefs.Remove(ToDelete);
            dbContext.SaveChanges();
            return RedirectToAction("Index"); 
        }

        [HttpPost("newdish")]

        public IActionResult NewDish(Dish fromForm)
        {
            if(ModelState.IsValid)
            {
                if(dbContext.Dishes.Any(d=> d.Name == fromForm.Name))
                {
                    
                    ModelState.AddModelError("Name", "That Dish Name is already taken!");
                    ViewBag.Chefs = dbContext.Chefs;
                    return View("AddaDish");
                }
                dbContext.Add(fromForm);
                dbContext.SaveChanges();
                return RedirectToAction("Dishes");
            } 
            else
            {
                    ModelState.AddModelError("Name", "That Dish Name is already taken!");
                    ViewBag.Chefs = dbContext.Chefs;
                    return View("AddaDish");
            }
        }
    }
}
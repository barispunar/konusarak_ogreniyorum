using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using konusarak_ogreniyorum.Models;

namespace konusarak_ogreniyorum.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(IndexModel model)
        {
            bool check = IndexModel.check_user(model.username, model.password);
            if (check)
            {
                return View("Generate_Exam");
            }
            return View(model);
        }



        public IActionResult Generate_Exam()
        {
            return View("Contact");
        }

        [HttpPost]
        public IActionResult Generate_Exam(Generate_ExamModel model)
        {
            Generate_ExamModel.write_to_database1();
            return View("Contact");
        } 

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

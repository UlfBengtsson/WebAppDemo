using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppDemo.Models;

namespace WebAppDemo.Controllers
{
    public class DemoController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(double multNumber)
        {
            ViewBag.UserMulti = multNumber;
            return View("CustomMultiTabel");
        }

        public IActionResult ToDoSession()
        {
            string toDoSessionText = HttpContext.Session.GetString("ToDo");

            if (!string.IsNullOrWhiteSpace(toDoSessionText))
            {
                ViewBag.SessionText = toDoSessionText;
            }

            return View();
        }

        [HttpPost]
        public IActionResult AddToDo(string toDo)
        {
            if (!string.IsNullOrWhiteSpace(toDo))
            {
                HttpContext.Session.SetString("ToDo", toDo);
            }

            //return RedirectToAction("ToDoSession");//same as line below
            return RedirectToAction(nameof(ToDoSession));
        }

        public IActionResult PreferdLanguageCookie()
        {
            string lang = Request.Cookies["PrefLang"]; ;//read cookie
            if (!string.IsNullOrWhiteSpace(lang))
            {
                ViewBag.Lang = lang;
            }
            return View();
        } 
        
        public IActionResult SetLanguageCookie(string lang)
        {
            
            if (!string.IsNullOrWhiteSpace(lang))
            {
                //Add cookie to Response object
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddMinutes(1);
                Response.Cookies.Append("PrefLang", lang, option);
            }
            return RedirectToAction(nameof(PreferdLanguageCookie));
        }


    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_FIA35_Cookies.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            // Cookies empfangen
            if (HttpContext.Request.Cookies["KeksHG"] != null && HttpContext.Request.Cookies["KeksVG"] != null)
            {
                ViewBag.BackgroundColor = HttpContext.Request.Cookies["KeksHG"];
                ViewBag.FrontColor = HttpContext.Request.Cookies["KeksVG"];
            }
            else
            {
                ViewBag.BackgroundColor = "black";
                ViewBag.FrontColor = "white";
            }
            if (HttpContext.Request.Cookies["KeksUniversal"] != null)
            {
                ViewBag.CookieInfos = HttpContext.Request.Cookies["KeksUniversal"];
            }

            return View();
        }

        [HttpPost]
        public IActionResult Index(string Vordergrund, string Hintergrund)
        {

            ViewBag.FrontColor = Vordergrund;
            ViewBag.BackgroundColor = Hintergrund;

            // Cookie setzen
            HttpContext.Response.Cookies.Append("KeksHG", Hintergrund, new CookieOptions { Expires = new DateTime(2021, 11, 11) }); // für Vordergrundfarbe
            HttpContext.Response.Cookies.Append("KeksVG", Vordergrund, new CookieOptions { Expires = new DateTime(2021, 11, 11) }); // für Hintergrundfarbe
            
            // Alternative: 3 Informationen in einem Cookie
            HttpContext.Response.Cookies.Append("KeksUniversal", $"HG_Farbe:{Hintergrund};VG_Farbe:{Vordergrund};LetzteEditierung:{DateTime.Now.ToShortDateString()}",
                new CookieOptions { Expires = new DateTime(2021, 11, 11) });
            ViewBag.CookieInfos = HttpContext.Request.Cookies["KeksUniversal"];

            return View();
        }
    }
}

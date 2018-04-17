using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dsASPCCaballos.Models;
using Microsoft.Extensions.Configuration;
using dsASPCCaballos.DataAccess;
using dsASPCCaballos.Entidades;

namespace dsASPCCaballos.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration _configuration;
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;


        }
        public IActionResult Index()
        {
            var ad = new AdaptadorCaballos(_configuration);
            var res = ad.ParticipanteRegistrar();
            ViewData["Res"] = res;
            return View();
        }
        [HttpPost]
        public IActionResult Registrar([FromForm] FormularioRegistro form)
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        public IActionResult Participantes()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

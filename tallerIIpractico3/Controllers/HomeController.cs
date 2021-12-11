using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using tallerIIpractico3.entities;
using tallerIIpractico3.Models;
using tallerIIpractico3.Models.Db;
using tallerIIpractico3.Models.Entities;
using tallerIIpractico3.ViewModel;

namespace tallerIIpractico3.Controllers
{
    public class HomeController : Controller
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly Db db;
        private readonly IMapper mapper;

        public HomeController(Db Db, IMapper mapper)
        {
            db = Db;
            this.mapper = mapper;
        }


        public IActionResult Index()
        {
            try
            {
                Usuario userDb = db.UsuarioDb.UsuarioByUserPass(
                HttpContext.Session.GetString("user"), HttpContext.Session.GetString("pass"));

                if (userDb != null)
                {
                    UsuarioViewModel userVM = mapper.Map<UsuarioViewModel>(userDb);
                    return View(userVM);
                }
                else
                    return RedirectToAction("IndexUsuario", "Usuario");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return RedirectToAction("IndexUsuario", "Usuario");
            }  
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

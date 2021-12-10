using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using tallerIIpractico3.entities;
using tallerIIpractico3.Models.Db;
using NLog;
using Microsoft.AspNetCore.Http;
using tallerIIpractico3.Models.Entities;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using tallerIIpractico3.ViewModel;

namespace tallerIIpractico3.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly Db db;
        private readonly IMapper mapper;
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        public UsuarioController(Db Db, IMapper mapper)
        {
            db = Db;
            this.mapper = mapper;
        }

        public IActionResult IndexUsuario()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string user, string pass)
        {
            Usuario usu = db.UsuarioDb.UsuarioByUserPass(user, pass);

            if (usu != null)
            {
                HttpContext.Session.SetString("user", usu.User);
                HttpContext.Session.SetString("rol", usu.Rol);
                return RedirectToAction("Index", "Home");
            }    
            return View("IndexUsuario");
        }

        public IActionResult CreateUsuarioView()
        {
            return View(new UsuarioViewModel());
        }

        [HttpPost]
        public IActionResult CreateUsuario(UsuarioViewModel usuVm)
        {
            if (ModelState.IsValid)
            {
                var usu = mapper.Map<Usuario>(usuVm);
                db.UsuarioDb.SaveUsuario(usu);
                return RedirectToAction("IndexUsuario");

            }
            else
            {
                return RedirectToAction("CreateUsuarioView");
            }
            
        }
    }
}

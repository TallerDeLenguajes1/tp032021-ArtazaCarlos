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
            try
            {
                Usuario usu = db.UsuarioDb.UsuarioByUserPass(user, pass);

                if (usu != null)
                {
                    HttpContext.Session.SetString("user", usu.User);
                    HttpContext.Session.SetString("pass", usu.Pass);
                    HttpContext.Session.SetString("rol", usu.Rol);
                    return RedirectToAction("Index", "Home");
                }
                return View("IndexUsuario");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return View("IndexUsuario");
            }
            
        }

        public IActionResult CreateView()
        {
            try
            {
                Usuario userDb = db.UsuarioDb.UsuarioByUserPass(
                HttpContext.Session.GetString("user"), HttpContext.Session.GetString("pass"));
                if (userDb != null)
                {
                    UsuarioViewModel userVM = mapper.Map<UsuarioViewModel>(userDb);
                    UsuarioABMViewModel usuarioCreateVM = new UsuarioABMViewModel();
                    usuarioCreateVM.Usuario = new UsuarioViewModel();
                    usuarioCreateVM.UserLog = userVM;
                    return View(usuarioCreateVM);
                }
                return RedirectToAction("IndexUsuario", "Usuario");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return RedirectToAction("IndexUsuario", "Usuario");
            }
            
        }

        [HttpPost]
        public IActionResult SaveUsuario(UsuarioABMViewModel usuarioCreateVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Usuario usuarioDB = mapper.Map<Usuario>(usuarioCreateVM.Usuario);
                    usuarioDB.Rol = RolUsuario.user.ToString();//aunque no se guarda el rol, se crea como admin, error que no pude depurar de momento
                    db.UsuarioDb.SaveUsuario(usuarioDB);
                    return RedirectToAction("ListaDeUsuarios");
                }
                return RedirectToAction("ErrorCreateUsuario", "Logger");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return RedirectToAction("ErrorCreateUsuario", "Logger");
            }
            

        }

        public IActionResult ListaDeUsuarios()
        {
            try
            {
                Usuario userDb = db.UsuarioDb.UsuarioByUserPass(
                HttpContext.Session.GetString("user"), HttpContext.Session.GetString("pass"));
                if (userDb != null)
                {
                    UsuarioViewModel userVM = mapper.Map<UsuarioViewModel>(userDb);
                    var usuarioVM = mapper.Map<List<UsuarioViewModel>>(db.UsuarioDb.ReadUsuarios());
                    UsuarioListaiewModel usuarioListaVM = new UsuarioListaiewModel();
                    usuarioListaVM.UserLog = userVM;
                    usuarioListaVM.Usuarios = usuarioVM;
                    return View(usuarioListaVM);
                }
                return RedirectToAction("IndexUsuario", "Usuario");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return RedirectToAction("IndexUsuario", "Usuario");
            }
            
        }

        [HttpGet]
        public IActionResult EditView(UsuarioViewModel usuarioVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Usuario userDb = db.UsuarioDb.UsuarioByUserPass(
                                    HttpContext.Session.GetString("user"), HttpContext.Session.GetString("pass"));

                    if (userDb != null)
                    {
                        UsuarioViewModel userVM = mapper.Map<UsuarioViewModel>(userDb);
                        UsuarioABMViewModel usuarioUpdateVM = new UsuarioABMViewModel();
                        usuarioUpdateVM.UserLog = userVM;
                        usuarioUpdateVM.Usuario = usuarioVM;
                        return View(usuarioUpdateVM);
                    }
                    return RedirectToAction("IndexUsuario", "Usuario");
                }
                return RedirectToAction("ErrorUpdateCliente", "Logger");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return RedirectToAction("ErrorUpdateCliente", "Logger");
            }
            
        }

        [HttpPost]
        public IActionResult UpdateUsuario(UsuarioABMViewModel usuarioUpdateVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Usuario usuarioDb = mapper.Map<Usuario>(usuarioUpdateVM.Usuario);
                    db.UsuarioDb.UpdateUsuario(usuarioDb);
                    return RedirectToAction("ListaDeUsuarios");
                }
                return RedirectToAction("ErrorUpdateUsuario", "Logger");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return RedirectToAction("ErrorUpdateUsuario", "Logger");
            }
            
        }


        public IActionResult DeleteView(UsuarioViewModel usuarioVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Usuario userDb = db.UsuarioDb.UsuarioByUserPass(
                                    HttpContext.Session.GetString("user"), HttpContext.Session.GetString("pass"));

                    if (userDb != null)
                    {
                        UsuarioViewModel userVM = mapper.Map<UsuarioViewModel>(userDb);
                        UsuarioABMViewModel usuarioDeleteVM = new UsuarioABMViewModel();
                        usuarioDeleteVM.UserLog = userVM;
                        usuarioDeleteVM.Usuario = usuarioVM;
                        return View(usuarioDeleteVM);
                    }
                    return RedirectToAction("IndexUsuario", "Usuario");
                }
                return RedirectToAction("ErrorDeleteCliente", "Logger");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return RedirectToAction("ErrorDeleteCliente", "Logger");
            }
            
        }


        public IActionResult DeteleUsuario(int usuarioId)
        {
            try
            {
                if (db.UsuarioDb.DeleteUsuario(usuarioId))
                {
                    return RedirectToAction("ListaDeUsuarios");
                }
                return RedirectToAction("ErrorDeleteCliente", "Logger");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return RedirectToAction("ErrorDeleteCliente", "Logger");
            }
            
        }

        public IActionResult Logout()
        {
            try
            {
                if (HttpContext.Session.GetString("user") != null)
                {
                    HttpContext.Session.Clear();
                    return RedirectToAction("IndexUsuario", "Usuario");
                }
                return RedirectToAction("IndexUsuario", "Usuario");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                HttpContext.Session.Clear();
                return RedirectToAction("IndexUsuario", "Usuario");
            }
            
        }

    }
}

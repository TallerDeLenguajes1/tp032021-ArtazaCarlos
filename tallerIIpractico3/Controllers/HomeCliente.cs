using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tallerIIpractico3.Controllers
{
    public class HomeCliente : Controller
    {
        // GET: HomeCliente
        public ActionResult Index()
        {
            return View();
        }

        // GET: HomeCliente/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeCliente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeCliente/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeCliente/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeCliente/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeCliente/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeCliente/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

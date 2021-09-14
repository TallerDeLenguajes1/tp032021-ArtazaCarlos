using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tallerIIpractico3.Controllers
{
    public class HomePedido : Controller
    {
        // GET: HomePedido
        public ActionResult Index()
        {
            return View();
        }

        // GET: HomePedido/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomePedido/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomePedido/Create
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

        // GET: HomePedido/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomePedido/Edit/5
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

        // GET: HomePedido/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomePedido/Delete/5
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

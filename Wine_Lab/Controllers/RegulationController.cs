using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wine_Lab.Controllers
{
    public class RegulationController : Controller
    {
        // GET: RegulationController
        public ActionResult Index()
        {
            return View();
        }

        // GET: RegulationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RegulationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RegulationController/Create
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

        // GET: RegulationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RegulationController/Edit/5
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

        // GET: RegulationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RegulationController/Delete/5
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DXNewsAPI.Model.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace DXNewsAPI.Controllers.Editing
{
    public class NewsEditorController : Controller
    {
        // GET: NewsEditor
        public ActionResult Index()
        {
            return View(new List<NewsItem>());
        }

        // GET: NewsEditor/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NewsEditor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NewsEditor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: NewsEditor/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NewsEditor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: NewsEditor/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NewsEditor/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
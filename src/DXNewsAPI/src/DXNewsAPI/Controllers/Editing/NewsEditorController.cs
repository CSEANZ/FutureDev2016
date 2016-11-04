using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DXNewsAPI.Model.Contract;
using DXNewsAPI.Model.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace DXNewsAPI.Controllers.Editing
{
    public class NewsEditorController : Controller
    {
        private readonly ITableStorageRepo _tableStorageRepo;

        public NewsEditorController(ITableStorageRepo tableStorageRepo)
        {
            _tableStorageRepo = tableStorageRepo;
        }
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
        public async Task <ActionResult> Create(NewsItem item)
        {
            try
            {
                // TODO: Add insert logic here

                var result = await _tableStorageRepo.InsertNewsItem(item);

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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DXNewsAPI.Model.Contract;
using DXNewsAPI.Model.Entity;
using DXNewsAPI.Model.Entity.News;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult> Index()
        {
            return View(await _tableStorageRepo.GetNewsItems());
        }

        // GET: NewsEditor/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        [Authorize]
        public async Task<ActionResult> Edit(string id)
        {
            return View(await _tableStorageRepo.GetNewsItemById(id));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(NewsItem item)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    var result = await _tableStorageRepo.UpdateNewsItem(item);
                }
                

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: NewsEditor/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: NewsEditor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task <ActionResult> Create(NewsItem item)
        {
            try
            {
                // TODO: Add insert logic here

                if (ModelState.IsValid)
                {
                    var result = await _tableStorageRepo.InsertNewsItem(item);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        // GET: NewsEditor/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(string id)
        {
            return View(await _tableStorageRepo.GetNewsItemById(id));
        }

        // POST: NewsEditor/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Delete(NewsItem newsItem)
        {
            try
            {
                await _tableStorageRepo.DeleteNewsItem(newsItem.Id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
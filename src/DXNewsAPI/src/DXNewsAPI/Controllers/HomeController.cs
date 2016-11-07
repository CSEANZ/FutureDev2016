using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DXNewsAPI.Model.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DXNewsAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITableStorageRepo _tableStorageRepo;

        public HomeController(ITableStorageRepo tableStorageRepo)
        {
            _tableStorageRepo = tableStorageRepo;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _tableStorageRepo.GetNewsItems(20));
        }

        public async Task<IActionResult> Detail(string id)
        {
            return View(await _tableStorageRepo.GetNewsItemById(id));
        }

        [Authorize]
        public IActionResult About()
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
            return View();
        }
    }
}

using GETASIS_Muhasebe.BL;
using GETASIS_Muhasebe.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GETASIS_Muhasebe.MVC.Controllers
{
    public class CariController : Controller
    {
        CariManager Cari = new CariManager();
        public IActionResult Index()
        {

            return View(Cari.GetAll());
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Cari cari)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = true;
                return View();
            }
            else if (ModelState.IsValid)
            {
                Cari.Add(cari);
                ViewBag.Error = false;
                ModelState.Clear();
                return View();
            }
            return View();
        }
        public IActionResult Update(int id)
        {
            return View(Cari.GetT(id));
        }
        [HttpPost]
        public IActionResult Update(Cari cari)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.Error = true;
                return View();
            }
            else if (ModelState.IsValid)
            {
                Cari.Update(cari);
                ViewBag.Error = false;
                return View();
            }
            return View();
        }

        public IActionResult Delete(int id)
        {
            TempData["DeleteError"] = "0";
            if (id > 0)
                if (Cari.Delete(id) == -1)
                {
                    TempData["DeleteError"] = "1";
                }

            return RedirectToAction("Index");
        }
    }
}

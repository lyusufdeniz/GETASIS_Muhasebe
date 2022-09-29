using GETASIS_Muhasebe.BL;
using GETASIS_Muhasebe.Entities;
using GETASIS_Muhasebe.MVC.Filters;
using GETASIS_Muhasebe.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GETASIS_Muhasebe.MVC.Controllers
{
    public class HesapController : Controller
    {
        HesapManager Hesap = new HesapManager();
        DovizManager Doviz = new DovizManager();
        HesapViewModel HesapViewModel = new HesapViewModel();
        HesapFilter HesapFilter = new HesapFilter();
        [NonAction]
        public void loadList()
        {

            List<SelectListItem> dovizler = (from x in Doviz.GetAll().ToList()
                                             select new SelectListItem
                                             { Text = x.Ad, Value = x.Id.ToString() }).ToList();



            ViewBag.Doviz = dovizler;

        }
        public IActionResult Index()
        {
            HesapViewModel.Hesap = Hesap.GetAllData();
            HesapViewModel.HesapFilter = HesapFilter;
            loadList();
            return View(HesapViewModel);
        }
        [HttpPost]
        public IActionResult Index(string DovizId)
        {
            var hesap = Hesap.GetAllData();
            loadList();
            if (!String.IsNullOrEmpty(DovizId) && DovizId != "-")
            {
                hesap = hesap.FindAll(x => x.DovizId == int.Parse(DovizId));
                HesapFilter.DovizId = int.Parse(DovizId);

            }
            HesapViewModel.Hesap = hesap;
            HesapViewModel.HesapFilter = HesapFilter;


            return View(HesapViewModel);
        }
        public IActionResult Add()
        {
            loadList();
            return View();
        }
        [HttpPost]
        public IActionResult Add(Hesap h)
        {
            if (!ModelState.IsValid)
            {
                loadList();
                ViewBag.Error = true;
                return View();
            }
            else if (ModelState.IsValid)
            {
                Hesap.Add(h);
                ViewBag.Error = false;
                ModelState.Clear();
                loadList();
                return View();
            }

            return View();

        }
        public IActionResult Delete(int id)
        {
            TempData["DeleteError"] = "0";
            if (id > 0)
                if (Hesap.Delete(id) == -1)
                {
                    TempData["DeleteError"] = "1";
                }

            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            loadList();
            return View(Hesap.GetT(id));
        }
        [HttpPost]
        public IActionResult Update(Hesap h)
        {

            if (!ModelState.IsValid)
            {
                loadList();
                ViewBag.Error = true;
                return View();
            }
            else if (ModelState.IsValid)
            {
                loadList();
                Hesap.Update(h);
                ViewBag.Error = false;
                return View();
            }
            return View();
        }
    }
}

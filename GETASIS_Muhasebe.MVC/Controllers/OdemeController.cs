using GETASIS_Muhasebe.BL;
using GETASIS_Muhasebe.Entities;
using GETASIS_Muhasebe.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GETASIS_Muhasebe.MVC.Controllers
{
    public class OdemeController : Controller
    {
        OdemeManager Odeme = new OdemeManager();
        CariManager Cari = new CariManager();
        OdemeTipManager OdemeTip = new OdemeTipManager();
        HesapManager Hesap = new HesapManager();

        [NonAction]
        public void loadList()
        {

            List<SelectListItem> cariler = (from x in Cari.GetAll().ToList()
                                            select new SelectListItem
                                            { Text = x.Ad, Value = x.Id.ToString() }).ToList();
            List<SelectListItem> turler = (from x in OdemeTip.GetAll().ToList()
                                           select new SelectListItem
                                           { Text = x.Ad, Value = x.Id.ToString() }).ToList();
            List<SelectListItem> hesaplar = (from x in Hesap.GetAll().ToList()
                                             select new SelectListItem
                                             { Text = x.Ad, Value = x.Id.ToString() }).ToList();

            ViewBag.Cari = cariler;
            ViewBag.Tur = turler;
            ViewBag.Hesap = hesaplar;
        }
        public IActionResult Index()
        {
            ViewBag.StartDate = DateTime.Now.AddMonths(-1).Date.ToString("yyyy-MM-dd");
            ViewBag.EndDate = DateTime.Now.ToString("yyyy-MM-dd");
            OdemeFilter odemeFilter = new OdemeFilter();
            odemeFilter.StartDate = (DateTime.Parse(DateTime.Now.AddMonths(-1).Date.ToString("dd/MM/yyyy")));
            odemeFilter.EndDate = DateTime.Now;
            loadList();
            OdemeViewModel OdemeViewModel = new OdemeViewModel()
            {
                Odeme = Odeme.GetAllData().FindAll(p => DateTime.Compare(DateTime.Parse(p.Tarih.ToString()), DateTime.Parse(DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy"))) >= 0 && DateTime.Compare(DateTime.Parse(p.Tarih.ToString()), DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy"))) <= 0),
                OdemeFilter = odemeFilter

            };



            return View(OdemeViewModel);

        }
        [HttpPost]
        public IActionResult Index(string StartDate, string EndDate, string HesapId, string OdemeTipId)
        {

            OdemeFilter odemeFilter = new OdemeFilter();
            loadList();
            var odeme = Odeme.GetAllData();
            if (!String.IsNullOrEmpty(HesapId) && HesapId != "-")
            {
                odeme = odeme.FindAll(x => x.HesapId == int.Parse(HesapId));
                odemeFilter.HesapId = int.Parse(HesapId);
            }
            if (!String.IsNullOrEmpty(OdemeTipId) && OdemeTipId != "-")
            {
                odeme = odeme.FindAll(x => x.OdemeTipId == int.Parse(OdemeTipId));
                odemeFilter.OdemeTipId = int.Parse(OdemeTipId);
            }
            if (!String.IsNullOrEmpty(StartDate))
            {
                ViewBag.StartDate = StartDate;
                odeme = odeme.FindAll(x => DateTime.Compare(DateTime.Parse(x.Tarih.ToString()), DateTime.Parse(StartDate)) >= 0);
                odemeFilter.EndDate = DateTime.Parse(StartDate);
            }
            if (!String.IsNullOrEmpty(EndDate))

            {
                ViewBag.EndDate = EndDate;
                odeme = odeme.FindAll(x => DateTime.Compare(DateTime.Parse(x.Tarih.ToString()), DateTime.Parse(EndDate)) <= 0);
                odemeFilter.EndDate = DateTime.Parse(EndDate);
            }
            OdemeViewModel OdemeViewModel = new OdemeViewModel() { Odeme = odeme, OdemeFilter = odemeFilter };

            return View(OdemeViewModel);
        }
        public IActionResult Add()
        {
            ViewBag.Alacak = 0.00;
            ViewBag.Borc = 0.00;
            ViewBag.CurrentDate = DateTime.Now.ToString("yyyy-MM-dd");
            loadList();
            return View();
        }
        [HttpPost]
        public IActionResult Add(Odeme o)
        {
            ViewBag.Alacak = 0.00;
            ViewBag.Borc = 0.00;
            if (!ModelState.IsValid)
            {
                loadList();
                ViewBag.Error = true;
                ViewBag.CurrentDate = o.Tarih.Value.ToString("yyyy-MM-dd");
                ViewBag.Alacak = (Double?)Double.Parse(o.Alacak.ToString());
                ViewBag.Borc = (Double?)Double.Parse(o.Borc.ToString());

                return View(o);
            }
            else if (ModelState.IsValid)
            {
                ViewBag.CurrentDate = o.Tarih.Value.ToString("yyyy-MM-dd");
                Odeme.Add(o);
                ViewBag.Error = false;

                loadList();
                return View();
            }

            return View(o);

        }
        public IActionResult Delete(int id)
        {
            if (id > 0)
                Odeme.InsteadDelete(id);
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            Odeme o = Odeme.GetT(id);
            loadList();
            double alacak = 0.00;
            double borc = 0.00;
            Double.TryParse(o.Alacak.ToString(), out alacak);
            Double.TryParse(o.Borc.ToString(), out borc);
            ViewBag.Alacak = alacak;
            ViewBag.Borc = borc;
            return View(o);
        }
        [HttpPost]
        public IActionResult Update(Odeme o)
        {

            if (!ModelState.IsValid)
            {
                loadList();
                ViewBag.Alacak = (Double?)Double.Parse(o.Alacak.ToString());
                ViewBag.Borc = (Double?)Double.Parse(o.Borc.ToString());
                ViewBag.Error = true;
                return View();
            }
            else if (ModelState.IsValid)
            {
                loadList();
                Odeme.Update(o);
                ViewBag.Error = false;
                return View();
            }
            return View();
        }


    }
}

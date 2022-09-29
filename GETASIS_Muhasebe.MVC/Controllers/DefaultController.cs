using ClosedXML.Excel;
using GETASIS_Muhasebe.BL;
using GETASIS_Muhasebe.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;
namespace GETASIS_Muhasebe.MVC.Controllers
{
    public class DefaultController : Controller
    {
        OdemeManager OdemeManager = new OdemeManager();
        CariManager CariManager = new CariManager();
        HesapManager HesapManager = new HesapManager();

        public IActionResult Index()
        {
            var _years = OdemeManager.GetAllData().OrderByDescending(p => p.Tarih.Value.Year).Select(p => p.Tarih.Value.Year).Distinct().ToList();
            List<SelectListItem> yillar = (from x in _years
                                           select new SelectListItem
                                           { Text = x.ToString(), Value = x.ToString(), }).ToList();

            ViewBag.Yillar = yillar;

            var son5 = OdemeManager.GetAllData().OrderByDescending(u => u.Tarih).Take(5);
            DashViewModel DashViewModel = new DashViewModel()
            {
                Odeme = son5.ToList(),
                Hesap = HesapManager.GetAllData(),
                year = DateTime.Now.Year

            };
            return View(DashViewModel);
        }


        public IActionResult AylikRapor(string id)
        {
            if (!String.IsNullOrEmpty(id) && int.Parse(id) > 0 && int.Parse(id) <= 12)
            {
                return Export(int.Parse(id));
            }
            int MonthId = DateTime.Now.Month;
            var months = Enumerable.Range(1, 12).Select(i => new { I = i, M = DateTimeFormatInfo.CurrentInfo.GetMonthName(i) });
            List<SelectListItem> aylar = (from x in months
                                          select new SelectListItem
                                          { Text = x.M, Value = x.I.ToString(), }).ToList();
            ViewBag.Aylar = aylar;
            RaporViewModel rapor = new RaporViewModel();

            DateTime startdate = new DateTime(DateTime.Now.Year, MonthId, 01);
            DateTime enddate = new DateTime(DateTime.Now.Year, MonthId, DateTime.DaysInMonth(DateTime.Now.Year, MonthId));
            decimal? totalborc = 0, totalalacak = 0, total = 0;
            foreach (var item in CariManager.GetAll())
            {
                decimal? alacak = OdemeManager.GetAllData().FindAll(p => p.Tarih >= startdate && p.Tarih <= enddate).Where(p => p.CariId == item.Id).Sum(p => p.Alacak);
                decimal? borc = OdemeManager.GetAllData().FindAll(p => p.Tarih >= startdate && p.Tarih <= enddate).Where(p => p.CariId == item.Id).Sum(p => p.Borc);
                totalborc = totalborc + borc;
                totalalacak = totalalacak + alacak;
            }
            total = totalalacak - totalborc;
            ViewBag.Total = total;
            ViewBag.TotalBorc = totalborc;
            ViewBag.TotalAlacak = totalalacak;

            rapor.Cari = CariManager.GetAll();
            rapor.Odeme = OdemeManager.GetAllData().FindAll(p => p.Tarih >= startdate && p.Tarih <= enddate);
            rapor.ay = MonthId;

            ViewBag.Month = startdate.ToString("MMMM");
            ViewBag.MonthId = MonthId;
            return View(rapor);





        }
        [HttpPost]
        public IActionResult AylikRapor(int MonthId)
        {
            var months = Enumerable.Range(1, 12).Select(i => new { I = i, M = DateTimeFormatInfo.CurrentInfo.GetMonthName(i) });
            List<SelectListItem> aylar = (from x in months
                                          select new SelectListItem
                                          { Text = x.M, Value = x.I.ToString(), }).ToList();
            ViewBag.Aylar = aylar;
            RaporViewModel rapor = new RaporViewModel();

            DateTime startdate = new DateTime(DateTime.Now.Year, MonthId, 01);
            DateTime enddate = new DateTime(DateTime.Now.Year, MonthId, DateTime.DaysInMonth(DateTime.Now.Year, MonthId));
            decimal? totalborc = 0, totalalacak = 0, total = 0;
            foreach (var item in CariManager.GetAll())
            {
                decimal? alacak = OdemeManager.GetAllData().FindAll(p => p.Tarih >= startdate && p.Tarih <= enddate).Where(p => p.CariId == item.Id).Sum(p => p.Alacak);
                decimal? borc = OdemeManager.GetAllData().FindAll(p => p.Tarih >= startdate && p.Tarih <= enddate).Where(p => p.CariId == item.Id).Sum(p => p.Borc);
                totalborc = totalborc + borc;
                totalalacak = totalalacak + alacak;
            }
            total = totalalacak - totalborc;
            ViewBag.Total = total;
            ViewBag.TotalBorc = totalborc;
            ViewBag.TotalAlacak = totalalacak;

            rapor.Cari = CariManager.GetAll();
            rapor.Odeme = OdemeManager.GetAllData().FindAll(p => p.Tarih >= startdate && p.Tarih <= enddate);
            rapor.ay = MonthId;

            ViewBag.Month = startdate.ToString("MMMM");
            ViewBag.MonthId = MonthId;
            return View(rapor);
        }
        [NonAction]
        public FileResult Export(int MonthId)
        {
            DateTime startdate = new DateTime(DateTime.Now.Year, MonthId, 01);
            DateTime enddate = new DateTime(DateTime.Now.Year, MonthId, DateTime.DaysInMonth(DateTime.Now.Year, MonthId));
            var cari = CariManager.GetAll();
            var data = OdemeManager.GetAllData().FindAll(p => p.Tarih >= startdate && p.Tarih <= enddate);
            using (var workbook = new XLWorkbook())
            {



                var worksheet = workbook.Worksheets.Add("Aylık Rapor");
                worksheet.Row(1).Style.Fill.BackgroundColor = XLColor.Tomato;
                worksheet.Row(1).Style.Font.Bold = true;
                worksheet.Row(1).Style.Font.FontSize = 14;



                worksheet.Cell(1, 1).Value = "Cari Adı";
                worksheet.Cell(1, 2).Value = "Cari Ad Soyad";
                worksheet.Cell(1, 3).Value = "Alacak";
                worksheet.Cell(1, 4).Value = "Borç";
                worksheet.Cell(1, 5).Value = "Toplam";
                int rowCount = 3;
                foreach (var Item in cari)
                {

                    var alacaktoplam = data.Where(p => p.CariId == Item.Id).Sum(p => p.Alacak);
                    var borctoplam = data.Where(p => p.CariId == Item.Id).Sum(p => p.Borc);
                    worksheet.Cell(rowCount, 1).Value = Item.Ad;
                    worksheet.Cell(rowCount, 2).Value = Item.AdSoyad;
                    worksheet.Cell(rowCount, 3).Value = alacaktoplam;
                    worksheet.Cell(rowCount, 4).Value = borctoplam;
                    worksheet.Cell(rowCount, 5).Value = alacaktoplam - borctoplam;

                    worksheet.Row(rowCount).Style.Font.FontSize = 12;
                    worksheet.Row(rowCount).Style.Fill.BackgroundColor = XLColor.WhiteSmoke;
                    worksheet.Row(rowCount).Style.Font.FontColor = XLColor.Black;



                    rowCount++;
                }

                worksheet.Row(rowCount + 1).Style.Font.Bold = true;
                worksheet.Row(rowCount + 1).Style.Fill.BackgroundColor = XLColor.Tomato;
                worksheet.Row(rowCount + 1).Style.Font.FontColor = XLColor.Black;
                worksheet.Row(rowCount + 1).Style.Font.FontSize = 14;
                worksheet.Row(rowCount + 3).Style.Font.FontSize = 12;
                worksheet.Row(rowCount + 3).Style.Font.Bold = true;
                worksheet.Row(rowCount + 3).Style.Fill.BackgroundColor = XLColor.AirForceBlue;
                worksheet.Row(rowCount + 3).Style.Font.FontColor = XLColor.Black;

                worksheet.Cell(rowCount + 1, 1).Value = "GENEL TOPLAM";
                worksheet.Cell(rowCount + 1, 3).FormulaA1 = "=Sum(" + worksheet.Cell(3, 3).Address + ":" + worksheet.Cell(cari.Count + 2, 3).Address + ")";
                worksheet.Cell(rowCount + 1, 4).FormulaA1 = "=Sum(" + worksheet.Cell(3, 4).Address + ":" + worksheet.Cell(cari.Count + 2, 4).Address + ")";
                worksheet.Cell(rowCount + 1, 5).FormulaA1 = "=Sum(" + worksheet.Cell(3, 5).Address + ":" + worksheet.Cell(cari.Count + 2, 5).Address + ")";

                worksheet.Cell(rowCount + 3, 1).Value = "Rapor Aralığı";
                worksheet.Cell(rowCount + 3, 2).Value = startdate.ToString("dd/MMM/yyyy");
                worksheet.Cell(rowCount + 3, 3).Value = enddate.ToString("dd/MMM/yyyy");



                {

                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Report.xlsx");
                }

            }
        }
        public JsonResult DataJson(int year)
        {
            int _yil = DateTime.Now.Year;
            if (year != 0 && year > 0)
            {
                DateTime baslangic = new DateTime(year, 1, 1);
                DateTime bitis = new DateTime(year, 12, 31);
                var query = OdemeManager.GetAllData().Where(x => x.Tarih >= baslangic && x.Tarih <= bitis).OrderBy(p => p.Tarih.Value.Month)
                       .GroupBy(p => new { p.Tarih.Value.Month, p.OdemeTip.Ad })
                       .Select(g => new { month = g.Key.Month, income = g.Sum(p => p.Alacak), outgoing = g.Sum(p => p.Borc), tip = g.Key.Ad });


                JsonResult json = new JsonResult(query);
                return json;
            }

            else
            {
                DateTime baslangic = new DateTime(_yil, 1, 1);
                DateTime bitis = new DateTime(_yil, 12, 31);
                var query = OdemeManager.GetAllData().Where(x => x.Tarih >= baslangic && x.Tarih <= bitis).OrderBy(p => p.Tarih.Value.Month)
                       .GroupBy(p => new { p.Tarih.Value.Month, p.OdemeTip.Ad })
                       .Select(g => new { month = g.Key.Month, income = g.Sum(p => p.Alacak), outgoing = g.Sum(p => p.Borc), tip = g.Key.Ad });


                JsonResult json = new JsonResult(query);
                return json;
            }


        }
    }
}

using MVCVooraadBeheer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace MVCVooraadBeheer.Controllers
{
    [Authorize]
    public class VoorraadController : Controller
    {
        class VoorraadItem
        {
            public string date;
            public int totaalTijdschriften;
        }

        private VooraadModelsContainer db = new VooraadModelsContainer();

        // GET: Voorraad
        public ActionResult Index(int? id)
        {
            ViewBag.id = id ?? 0;

            var l = db.LocationSet.ToList();
            l.Insert(0, new Location { Id = -1, Name = "Alle locaties" });
            ViewBag.location_Id = new SelectList(l, "Id", "Name");

            return View();
        }

        public ActionResult GetTotaleVoorraad()
        {
            List<VoorraadItem> rtw = new List<VoorraadItem>();
            int totaal = 0;
            foreach (var t in db.MagazineTransactionSet.OrderBy(x => x.DateTime).ToList())
            {
                if (t.TransactionType == TransactionType.FromLeverancier)
                    totaal += t.Value;
                else if (t.TransactionType == TransactionType.ShopToCustomer)
                    totaal -= t.Value;
                else continue;

                rtw.Add(new VoorraadItem { date = t.DateTime.ToString("MM-dd-yyyy"), totaalTijdschriften = totaal });
            }

            rtw = rtw
                .GroupBy(o => o.date)
                .Select(l =>
                    new VoorraadItem
                    {
                        date = l.Key,
                        totaalTijdschriften = (int)l.ToList().Average(item => (double)item.totaalTijdschriften)
                    })
                .ToList();

            return Json(rtw, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTotaleHistoricalVoorraadPerLocation(int? id)
        {
            if (id == null || db.LocationSet.Find(id) == null)
                return Json("not found", JsonRequestBehavior.AllowGet);

            //betreffende locatie
            var location = db.LocationSet.Find(id);
            List<VoorraadItem> rtw = new List<VoorraadItem>();
            int totaal = 0;
            foreach (var tr in location.MagazineTransactionFrom.Union(location.MagazineTransactionTo).OrderBy(t => t.DateTime))
            {
                totaal += (tr.LocationToId == id) ? tr.Value : -tr.Value;

                rtw.Add(new VoorraadItem { date = tr.DateTime.ToString("MM-dd-yyyy"), totaalTijdschriften = totaal });
            }

            rtw = rtw
                .GroupBy(o => o.date)
                .Select(l =>
                    new VoorraadItem
                    {
                        date = l.Key,
                        totaalTijdschriften = (int)l.ToList().Average(item => (double)item.totaalTijdschriften)
                    })
                .ToList();

            return Json(rtw, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCurrentVoorraad(int? locationId)
        {
            /*if (db.LocationSet.Find(locationId) == null)
                return Json(null, JsonRequestBehavior.AllowGet);*/

            int id = locationId ?? 0;

            var data =
                from set2 in
                    (from mSet in
                         (from tr in db.MagazineTransactionSet
                          where tr.LocationFromId == id || tr.LocationToId == id
                          group tr by tr.MagazineId into PerMagazine
                          select PerMagazine)
                     select
                    from m in mSet
                    group m by m.LocationToId == id into to
                    select to)
                select new {name = set2.FirstOrDefault().FirstOrDefault().Magazine.Name, value = set2.Select(x => x.Sum(x2 => x.Key ? x2.Value : -x2.Value)).Sum() };

            data = data.Where(row => row.value > 0);

            return Json(data.ToList(), JsonRequestBehavior.AllowGet);
        }

        public string BekijkWaarschuwingen(int? locationId)
        {
            var titles = db.MagazineTitleSet.ToList();
            var warnings = db.LocationMagazineTitleWarningSet.Where(w => w.Location.Id == locationId).Include(x=> x.MagazineTitle).ToList();

            var result = titles.Select(t => {
                return new { title = t, warning = warnings.Exists(lmw => lmw.MagazineTitle.Id == t.Id) ? warnings.First(lmw => lmw.Id == t.Id).value : 0 };
            });

            return string.Empty;
        }
    }
}
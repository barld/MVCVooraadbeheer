using MVCVooraadBeheer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCVooraadBeheer.Controllers
{
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
            var l = db.LocationSet.ToList();
            l.Insert(0, new Location { Id = -1, Name = "Alle locaties" });
            ViewBag.location_Id = new SelectList(l, "Id", "Name");

            return View();
        }

        public ActionResult GetTotaleVoorraad()
        {
            List<VoorraadItem> rtw = new List<VoorraadItem>();
            int totaal=0;
            foreach(var t in db.MagazineTransactionSet.ToList().OrderBy(x => x.DateTime))
            {
                if (t.TransactionType == TransactionType.FromLeverancier)
                    totaal += t.Value;
                else if (t.TransactionType == TransactionType.ShopToCustomer)
                    totaal -= t.Value;

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

        public JsonResult GetTotaleVooraadPerLocation(int? id)
        {
            if (id == null || db.LocationSet.Find(id) == null)
                return Json("not found", JsonRequestBehavior.AllowGet);

            //betreffende locatie
            var location = db.LocationSet.Find(id);
            List<VoorraadItem> rtw = new List<VoorraadItem>();
            int totaal = 0;
            foreach(var tr in location.MagazineTransactionFrom.Union(location.MagazineTransactionTo).OrderBy(t => t.DateTime))
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
    }
}
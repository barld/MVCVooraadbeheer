using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCVooraadBeheer.Models;

namespace MVCVooraadBeheer.Controllers
{
    [Authorize]
    [RoutePrefix("warnings")]
    public class LocationMagazineTitleWarningsController : Controller
    {
        private VooraadModelsContainer db = new VooraadModelsContainer();

        // GET: LocationMagazineTitleWarnings
        [Route]
        public ActionResult Index()
        {
            var locationMagazineTitleWarningSet = db.LocationMagazineTitleWarningSet.Include(l => l.Location).Include(l => l.MagazineTitle);
            return View(locationMagazineTitleWarningSet.ToList());
        }

        // GET: LocationMagazineTitleWarnings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocationMagazineTitleWarning locationMagazineTitleWarning = db.LocationMagazineTitleWarningSet.Find(id);
            if (locationMagazineTitleWarning == null)
            {
                return HttpNotFound();
            }
            return View(locationMagazineTitleWarning);
        }

        // GET: LocationMagazineTitleWarnings/Create
        public ActionResult Create()
        {
            ViewBag.LocationId = new SelectList(db.LocationSet, "Id", "Name");
            ViewBag.MagazineTitleId = new SelectList(db.MagazineTitleSet, "Id", "Name");
            return View();
        }

        // POST: LocationMagazineTitleWarnings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ActiveTo,value,LocationId,MagazineTitleId")] LocationMagazineTitleWarning locationMagazineTitleWarning)
        {
            if (ModelState.IsValid)
            {
                locationMagazineTitleWarning.Active = true;
                db.LocationMagazineTitleWarningSet.Add(locationMagazineTitleWarning);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LocationId = new SelectList(db.LocationSet, "Id", "Name", locationMagazineTitleWarning.LocationId);
            ViewBag.MagazineTitleId = new SelectList(db.MagazineTitleSet, "Id", "Name", locationMagazineTitleWarning.MagazineTitleId);
            return View(locationMagazineTitleWarning);
        }

        // GET: LocationMagazineTitleWarnings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocationMagazineTitleWarning locationMagazineTitleWarning = db.LocationMagazineTitleWarningSet.Find(id);
            if (locationMagazineTitleWarning == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationId = new SelectList(db.LocationSet, "Id", "Name", locationMagazineTitleWarning.LocationId);
            ViewBag.MagazineTitleId = new SelectList(db.MagazineTitleSet, "Id", "Name", locationMagazineTitleWarning.MagazineTitleId);
            return View(locationMagazineTitleWarning);
        }

        // POST: LocationMagazineTitleWarnings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Active,ActiveTo,value,LocationId,MagazineTitleId")] LocationMagazineTitleWarning locationMagazineTitleWarning)
        {
            if (ModelState.IsValid)
            {
                db.Entry(locationMagazineTitleWarning).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LocationId = new SelectList(db.LocationSet, "Id", "Name", locationMagazineTitleWarning.LocationId);
            ViewBag.MagazineTitleId = new SelectList(db.MagazineTitleSet, "Id", "Name", locationMagazineTitleWarning.MagazineTitleId);
            return View(locationMagazineTitleWarning);
        }

        // GET: LocationMagazineTitleWarnings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocationMagazineTitleWarning locationMagazineTitleWarning = db.LocationMagazineTitleWarningSet.Find(id);
            if (locationMagazineTitleWarning == null)
            {
                return HttpNotFound();
            }
            return View(locationMagazineTitleWarning);
        }

        // POST: LocationMagazineTitleWarnings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LocationMagazineTitleWarning locationMagazineTitleWarning = db.LocationMagazineTitleWarningSet.Find(id);
            db.LocationMagazineTitleWarningSet.Remove(locationMagazineTitleWarning);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult OverzichtWaarschuwingen(int? locationid)
        {
            var location = db.LocationSet.Find(locationid);
            if (location == null)
                return RedirectToAction("Index");

            int id = locationid ?? 0;

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
                select new LocationWarningModelView 
                {
                    ID = set2.FirstOrDefault().FirstOrDefault().Magazine.MagazineTitle.Id,
                    Name = set2.FirstOrDefault().FirstOrDefault().Magazine.MagazineTitle.Name, 
                    totaalTijdschriften = set2.Select(x => x.Sum(x2 => x.Key ? x2.Value : -x2.Value)).Sum(),
                    //minimaaleWaarde = db.LocationSet.Find(id) == null ? 0 : db.LocationSet.Find(id).LocationMagazineTitleWarning.FirstOrDefault(w => w.MagazineTitleId == set2.FirstOrDefault().FirstOrDefault().Magazine.MagazineTitle.Id).value //set2.FirstOrDefault().FirstOrDefault().Magazine.MagazineTitle.LocationMagazineTitleWarning.Count(x => x.LocationId > id) > 0 ? set2.FirstOrDefault().FirstOrDefault().Magazine.MagazineTitle.LocationMagazineTitleWarning.FirstOrDefault(x => x.LocationId > id).value : 0
                };

            data = data.GroupBy(x => x.Name).Select(g => new LocationWarningModelView { ID=g.FirstOrDefault().ID, Name = g.Key, totaalTijdschriften = g.Sum(x => x.totaalTijdschriften) });

            //data = data.Where(row => row.totaalTijdschriften > 0);
            var ldata = data.ToList();
            for (int i = 0; i < ldata.Count;i++ )
            {
                int mid = ldata[i].ID;
                ldata[i].minimaaleWaarde = db.LocationMagazineTitleWarningSet.Count(x => x.MagazineTitleId == mid && x.LocationId == id) > 0 ? db.LocationMagazineTitleWarningSet.FirstOrDefault(x => x.MagazineTitleId == mid && x.LocationId == id).value : 0;
            }

            return View(ldata);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    public class LocationWarningModelView
    {
        /// <summary>
        /// magazine titleId
        /// </summary>
        public int ID { get; set; }

        public int totaalTijdschriften { get; set; }

        public int minimaaleWaarde { get; set; }

        public string Name { get; set; }
    }
}

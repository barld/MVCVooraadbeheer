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
    public class LocationMagazineTitleWarningsController : Controller
    {
        private VooraadModelsContainer db = new VooraadModelsContainer();

        // GET: LocationMagazineTitleWarnings
        public ActionResult Index()
        {
            return View(db.LocationMagazineTitleWarningSet.ToList());
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
            return View();
        }

        // POST: LocationMagazineTitleWarnings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Active,ActiveTo,value")] LocationMagazineTitleWarning locationMagazineTitleWarning)
        {
            if (ModelState.IsValid)
            {
                db.LocationMagazineTitleWarningSet.Add(locationMagazineTitleWarning);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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
            return View(locationMagazineTitleWarning);
        }

        // POST: LocationMagazineTitleWarnings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Active,ActiveTo,value")] LocationMagazineTitleWarning locationMagazineTitleWarning)
        {
            if (ModelState.IsValid)
            {
                db.Entry(locationMagazineTitleWarning).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

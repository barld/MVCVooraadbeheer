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
    public class LocationsController : Controller
    {
        private VooraadModelsContainer db = new VooraadModelsContainer();

        // GET: Locations
        public ActionResult Index()
        {
            var locationSet = db.LocationSet.Include(l => l.LocationMagazineWarning);
            return View(locationSet.ToList());
        }

        // GET: Locations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.LocationSet.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // GET: Locations/Create
        public ActionResult Create()
        {
            return View(new Location { Active = true });
        }

        // POST: Locations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Adresse,PostCode,City,Active")] Location location)
        {
            if (ModelState.IsValid)
            {
                db.LocationSet.Add(location);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.LocationMagazineTitleWarningSet, "Id", "value", location.Id);
            return View(location);
        }

        // GET: Locations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.LocationSet.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.LocationMagazineTitleWarningSet, "Id", "value", location.Id);
            return View(location);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Adresse,PostCode,City,Active")] Location location)
        {
            if (ModelState.IsValid)
            {
                db.Entry(location).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.LocationMagazineTitleWarningSet, "Id", "value", location.Id);
            return View(location);
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

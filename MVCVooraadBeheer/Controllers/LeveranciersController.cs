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
    public class LeveranciersController : Controller
    {
        private VooraadModelsContainer db = new VooraadModelsContainer();

        // GET: Leveranciers
        public ActionResult Index()
        {
            return View(db.LeverancierSet.ToList());
        }

        // GET: Leveranciers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leverancier leverancier = db.LeverancierSet.Find(id);
            if (leverancier == null)
            {
                return HttpNotFound();
            }
            return View(leverancier);
        }

        // GET: Leveranciers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Leveranciers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Adresse,PostCode,City,Active")] Leverancier leverancier)
        {
            if (ModelState.IsValid)
            {
                db.LeverancierSet.Add(leverancier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(leverancier);
        }

        // GET: Leveranciers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leverancier leverancier = db.LeverancierSet.Find(id);
            if (leverancier == null)
            {
                return HttpNotFound();
            }
            return View(leverancier);
        }

        // POST: Leveranciers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Adresse,PostCode,City,Active")] Leverancier leverancier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(leverancier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(leverancier);
        }

        // GET: Leveranciers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leverancier leverancier = db.LeverancierSet.Find(id);
            if (leverancier == null)
            {
                return HttpNotFound();
            }
            return View(leverancier);
        }

        // POST: Leveranciers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Leverancier leverancier = db.LeverancierSet.Find(id);
            db.LeverancierSet.Remove(leverancier);
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

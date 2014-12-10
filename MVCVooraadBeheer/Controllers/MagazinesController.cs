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
    public class MagazinesController : Controller
    {
        private VooraadModelsContainer db = new VooraadModelsContainer();

        // GET: Magazines
        public ActionResult Index(string SearchString)
        {
            var magazineSet = db.MagazineSet.Where(m => m.Active).Include(m => m.MagazineTitle);

            if (!string.IsNullOrWhiteSpace(SearchString))
                magazineSet = magazineSet.Where(m => m.Name.Contains(SearchString));

            return View(magazineSet.ToList());
        }

        // GET: Magazines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Magazine magazine = db.MagazineSet.Find(id);
            if (magazine == null)
            {
                return HttpNotFound();
            }
            return View(magazine);
        }

        // GET: Magazines/Create
        public ActionResult Create()
        {
            ViewBag.MagazineTitleId = new SelectList(db.MagazineTitleSet, "Id", "Name");
            return View();
        }

        // POST: Magazines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Active,Jaargang,nummer,Verschijning,UitSchappen,BarCode,Price,MagazineTitleId")] Magazine magazine)
        {
            if (ModelState.IsValid)
            {
                db.MagazineSet.Add(magazine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MagazineTitleId = new SelectList(db.MagazineTitleSet, "Id", "Name", magazine.MagazineTitleId);
            return View(magazine);
        }

        // GET: Magazines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Magazine magazine = db.MagazineSet.Find(id);
            if (magazine == null)
            {
                return HttpNotFound();
            }
            ViewBag.MagazineTitleId = new SelectList(db.MagazineTitleSet, "Id", "Name", magazine.MagazineTitleId);
            return View(magazine);
        }

        // POST: Magazines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Active,Jaargang,nummer,Verschijning,UitSchappen,BarCode,Price,MagazineTitleId")] Magazine magazine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(magazine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MagazineTitleId = new SelectList(db.MagazineTitleSet, "Id", "Name", magazine.MagazineTitleId);
            return View(magazine);
        }

        // GET: Magazines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Magazine magazine = db.MagazineSet.Find(id);
            if (magazine == null)
            {
                return HttpNotFound();
            }
            return View(magazine);
        }

        // POST: Magazines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Magazine magazine = db.MagazineSet.Find(id);
            db.MagazineSet.Remove(magazine);
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

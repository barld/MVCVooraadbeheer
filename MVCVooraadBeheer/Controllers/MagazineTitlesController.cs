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
    public class MagazineTitlesController : Controller
    {
        private VooraadModelsContainer db = new VooraadModelsContainer();

        // GET: MagazineTitles
        public ActionResult Index(string SearchString)
        {
            var magazineTitleSet = db.MagazineTitleSet.Include(m => m.LocationMagazineTitleWarning).Include(m => m.LanguageSet);
            if (!string.IsNullOrWhiteSpace(SearchString))
                magazineTitleSet = magazineTitleSet.Where(mt => mt.Name.Contains(SearchString));

            return View(magazineTitleSet.ToList());
        }

        // GET: MagazineTitles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MagazineTitle magazineTitle = db.MagazineTitleSet.Find(id);
            if (magazineTitle == null)
            {
                return HttpNotFound();
            }
            return View(magazineTitle);
        }

        // GET: MagazineTitles/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.LocationMagazineTitleWarningSet, "Id", "value");
            ViewBag.Language_Id = new SelectList(db.LanguageSet, "Id", "Name");
            return View();
        }

        // POST: MagazineTitles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Language_Id")] MagazineTitle magazineTitle)
        {
            if (ModelState.IsValid)
            {
                db.MagazineTitleSet.Add(magazineTitle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.LocationMagazineTitleWarningSet, "Id", "value", magazineTitle.Id);
            ViewBag.Language_Id = new SelectList(db.LanguageSet, "Id", "Name", magazineTitle.Language_Id);
            return View(magazineTitle);
        }

        // GET: MagazineTitles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MagazineTitle magazineTitle = db.MagazineTitleSet.Find(id);
            if (magazineTitle == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.LocationMagazineTitleWarningSet, "Id", "value", magazineTitle.Id);
            ViewBag.Language_Id = new SelectList(db.LanguageSet, "Id", "Name", magazineTitle.Language_Id);
            return View(magazineTitle);
        }

        // POST: MagazineTitles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Language_Id")] MagazineTitle magazineTitle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(magazineTitle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.LocationMagazineTitleWarningSet, "Id", "value", magazineTitle.Id);
            ViewBag.Language_Id = new SelectList(db.LanguageSet, "Id", "Name", magazineTitle.Language_Id);
            return View(magazineTitle);
        }

        // GET: MagazineTitles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MagazineTitle magazineTitle = db.MagazineTitleSet.Find(id);
            if (magazineTitle == null)
            {
                return HttpNotFound();
            }
            return View(magazineTitle);
        }

        // POST: MagazineTitles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MagazineTitle magazineTitle = db.MagazineTitleSet.Find(id);
            db.MagazineTitleSet.Remove(magazineTitle);
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

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
    public class TransactionsController : Controller
    {
        private VooraadModelsContainer db = new VooraadModelsContainer();

        // GET: Transactions
        public ActionResult Index()
        {
            var magazineTransactionSet = db.MagazineTransactionSet.Include(m => m.LocationFrom).Include(m => m.LocationTo).Include(m => m.Magazine).Include(m => m.Leverancier);
            return View(magazineTransactionSet.ToList());
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MagazineTransaction magazineTransaction = db.MagazineTransactionSet.Find(id);
            if (magazineTransaction == null)
            {
                return HttpNotFound();
            }
            return View(magazineTransaction);
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            ViewBag.LocationFromId = new SelectList(db.LocationSet, "Id", "Name");
            ViewBag.LocationToId = new SelectList(db.LocationSet, "Id", "Name");
            ViewBag.MagazineId = new SelectList(db.MagazineSet, "Id", "Name");
            ViewBag.LeverancierId = new SelectList(db.LeverancierSet, "Id", "Name");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DateTime,Value,LocationFromId,LocationToId,TransactionType,MagazineId,LeverancierId")] MagazineTransaction magazineTransaction)
        {
            if (ModelState.IsValid)
            {
                db.MagazineTransactionSet.Add(magazineTransaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LocationFromId = new SelectList(db.LocationSet, "Id", "Name", magazineTransaction.LocationFromId);
            ViewBag.LocationToId = new SelectList(db.LocationSet, "Id", "Name", magazineTransaction.LocationToId);
            ViewBag.MagazineId = new SelectList(db.MagazineSet, "Id", "Name", magazineTransaction.MagazineId);
            ViewBag.LeverancierId = new SelectList(db.LeverancierSet, "Id", "Name", magazineTransaction.LeverancierId);
            return View(magazineTransaction);
        }

        public ActionResult CreateFromLeverancier(int? id)
        {
            ViewBag.LocationToId = new SelectList(db.LocationSet, "Id", "Name");
            ViewBag.MagazineId = new SelectList(db.MagazineSet, "Id", "Name");
            ViewBag.LeverancierId = db.LeverancierSet.Find(id) != null ? new SelectList(db.LeverancierSet, "Id", "Name", id) : new SelectList(db.LeverancierSet, "Id", "Name");

            return View(new MagazineTransaction() { DateTime = DateTime.Now, TransactionType = TransactionType.FromLeverancier });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFromLeverancier(MagazineTransaction m)
        {
            if(ModelState.IsValid)
            {
                m.TransactionType = TransactionType.FromLeverancier;
                db.MagazineTransactionSet.Add(m);
                db.SaveChanges();
                return RedirectToAction("index");
            }
            return View();
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MagazineTransaction magazineTransaction = db.MagazineTransactionSet.Find(id);
            if (magazineTransaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationFromId = new SelectList(db.LocationSet, "Id", "Name", magazineTransaction.LocationFromId);
            ViewBag.LocationToId = new SelectList(db.LocationSet, "Id", "Name", magazineTransaction.LocationToId);
            ViewBag.MagazineId = new SelectList(db.MagazineSet, "Id", "Name", magazineTransaction.MagazineId);
            ViewBag.LeverancierId = new SelectList(db.LeverancierSet, "Id", "Name", magazineTransaction.LeverancierId);
            return View(magazineTransaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DateTime,Value,LocationFromId,LocationToId,TransactionType,MagazineId,LeverancierId")] MagazineTransaction magazineTransaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(magazineTransaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LocationFromId = new SelectList(db.LocationSet, "Id", "Name", magazineTransaction.LocationFromId);
            ViewBag.LocationToId = new SelectList(db.LocationSet, "Id", "Name", magazineTransaction.LocationToId);
            ViewBag.MagazineId = new SelectList(db.MagazineSet, "Id", "Name", magazineTransaction.MagazineId);
            ViewBag.LeverancierId = new SelectList(db.LeverancierSet, "Id", "Name", magazineTransaction.LeverancierId);
            return View(magazineTransaction);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MagazineTransaction magazineTransaction = db.MagazineTransactionSet.Find(id);
            if (magazineTransaction == null)
            {
                return HttpNotFound();
            }
            return View(magazineTransaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MagazineTransaction magazineTransaction = db.MagazineTransactionSet.Find(id);
            db.MagazineTransactionSet.Remove(magazineTransaction);
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCVooraadBeheer.Models;
using PagedList;

namespace MVCVooraadBeheer.Controllers
{
    [Authorize]
    public class TransactionsController : Controller
    {
        private VooraadModelsContainer db = new VooraadModelsContainer();

        // GET: Transactions
        public ActionResult Index(int? page)
        {
            var magazineTransactionSet = db.MagazineTransactionSet.Include(m => m.LocationFrom).Include(m => m.LocationTo).Include(m => m.Magazine).Include(m => m.Leverancier);

            magazineTransactionSet = magazineTransactionSet.OrderBy(t => t.DateTime);

            int pageNumber = page ?? 1;

            return View(magazineTransactionSet.ToPagedList(pageNumber, 10));
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
            return View(m);
        }

        public ActionResult CreateCustomerTransaction(int? locationId)
        {
            ViewBag.LocationFromId = new SelectList(db.LocationSet, "Id", "Name", locationId);
            ViewBag.MagazineId = new SelectList(db.MagazineSet, "Id", "Name");

            return View(new MagazineTransaction() { DateTime = DateTime.Now, TransactionType = TransactionType.ShopToCustomer });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCustomerTransaction(MagazineTransaction m)
        {
            if (ModelState.IsValid)
            {
                m.TransactionType = TransactionType.ShopToCustomer;
                db.MagazineTransactionSet.Add(m);
                db.SaveChanges();
                return RedirectToAction("index");
            }
            return View(m);
        }

        public ActionResult CreateInternTransaction(int? from, int? to)
        {
            ViewBag.LocationToId = new SelectList(db.LocationSet, "Id", "Name");
            ViewBag.LocationFromId = ViewBag.LocationToId;
            ViewBag.MagazineId = new SelectList(db.MagazineSet, "Id", "Name");

            return View(new MagazineTransaction() { DateTime = DateTime.Now, TransactionType = TransactionType.Intern });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateInternTransaction(MagazineTransaction m)
        {
            if (ModelState.IsValid)
            {
                m.TransactionType = TransactionType.Intern;
                db.MagazineTransactionSet.Add(m);
                db.SaveChanges();
                return RedirectToAction("index");
            }
            return View(m);
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

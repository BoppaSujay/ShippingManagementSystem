using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShippingManagementSystem.Models;

namespace ShippingManagementSystem.Controllers
{
    public class ShipAccountsController : Controller
    {
        private ShipManagementSystemEntities db = new ShipManagementSystemEntities();

        // GET: ShipAccounts
        public ActionResult Index()
        {
            return View(db.ShipAccounts.ToList());
        }

        // GET: ShipAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShipAccount shipAccount = db.ShipAccounts.Find(id);
            if (shipAccount == null)
            {
                return HttpNotFound();
            }
            return View(shipAccount);
        }

        // GET: ShipAccounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShipAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShipAccountId,AccountNumber,AccountBalance")] ShipAccount shipAccount)
        {
            if (ModelState.IsValid)
            {
                db.ShipAccounts.Add(shipAccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(shipAccount);
        }

        // GET: ShipAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShipAccount shipAccount = db.ShipAccounts.Find(id);
            if (shipAccount == null)
            {
                return HttpNotFound();
            }
            return View(shipAccount);
        }

        // POST: ShipAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShipAccountId,AccountNumber,AccountBalance")] ShipAccount shipAccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shipAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shipAccount);
        }

        // GET: ShipAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShipAccount shipAccount = db.ShipAccounts.Find(id);
            if (shipAccount == null)
            {
                return HttpNotFound();
            }
            return View(shipAccount);
        }

        // POST: ShipAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ShipAccount shipAccount = db.ShipAccounts.Find(id);
            db.ShipAccounts.Remove(shipAccount);
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

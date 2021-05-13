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
    public class CargoBookingsController : Controller
    {
        private ShipManagementSystemEntities db = new ShipManagementSystemEntities();

        // GET: CargoBookings
        public ActionResult Index()
        {
            var cargoBooking = db.CargoBookings.Include(c => c.Ship).Include(c => c.User);
            return View(db.Ships.ToList());
        }

        // GET: CargoBookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CargoBooking cargoBooking = db.CargoBookings.Find(id);
            if (cargoBooking == null)
            {
                return HttpNotFound();
            }
            return View(cargoBooking);
        }

        #region Booking
        // GET: CargoBookings/Create
        public ActionResult Booking()
        {
            ViewBag.ShipId = new SelectList(db.Ships, "ShipId", "ShipModel");
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName");
            return View();
        }

        // POST: CargoBookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Booking([Bind(Include = "CargoBookingId,UserId,ShipId,Product,Quantity,ShippingCost,BookingDate")] CargoBooking cargoBooking)
        {

                db.CargoBookings.Add(cargoBooking);
                //int id = 1;
                //ShipAccount sa = db.ShipAccounts.Find(id);
                //sa.AccountBalance += (decimal)cargoBooking.ShippingCost;
                //db.Entry(sa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Users");

            ViewBag.ShipId = new SelectList(db.Ships, "ShipId", "ShipModel", cargoBooking.ShipId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", cargoBooking.UserId);
            return View(cargoBooking);
        }
        #endregion
        // GET: CargoBookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CargoBooking cargoBooking = db.CargoBookings.Find(id);
            if (cargoBooking == null)
            {
                return HttpNotFound();
            }
            ViewBag.ShipId = new SelectList(db.Ships, "ShipId", "ShipModel", cargoBooking.ShipId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", cargoBooking.UserId);
            return View(cargoBooking);
        }

        // POST: CargoBookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CargoBookingId,UserId,ShipId,Product,Quantity,ShippingCost,BookingDate")] CargoBooking cargoBooking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cargoBooking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ShipId = new SelectList(db.Ships, "ShipId", "ShipModel", cargoBooking.ShipId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", cargoBooking.UserId);
            return View(cargoBooking);
        }

        // GET: CargoBookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CargoBooking cargoBooking = db.CargoBookings.Find(id);
            if (cargoBooking == null)
            {
                return HttpNotFound();
            }
            return View(cargoBooking);
        }

        // POST: CargoBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CargoBooking cargoBooking = db.CargoBookings.Find(id);
            db.CargoBookings.Remove(cargoBooking);
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

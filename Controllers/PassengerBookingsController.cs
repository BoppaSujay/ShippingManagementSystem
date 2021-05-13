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
    public class PassengerBookingsController : Controller
    {
        private ShipManagementSystemEntities db = new ShipManagementSystemEntities();

        // GET: PassengerBookings
        public ActionResult Index()
        {
            var passengerBooking = db.PassengerBookings.Include(c => c.Ship).Include(c => c.User);
            return View(db.Ships.ToList());
        }

        // GET: PassengerBookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PassengerBooking passengerBooking = db.PassengerBookings.Find(id);
            if (passengerBooking == null)
            {
                return HttpNotFound();
            }
            return View(passengerBooking);
        }


        #region Booking
        // GET: PassengerBookings/Create
        public ActionResult Booking()
        {
            ViewBag.ShipId = new SelectList(db.Ships, "ShipId", "ShipModel");
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName");
            return View();
        }

        // POST: PassengerBookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Booking([Bind(Include = "PassengerBookingId,UserId,ShipId,Tickets_Available,TicketCost,NoOfTickets,TotalPrice,BookingDate")] PassengerBooking passengerBooking)
        {

                db.PassengerBookings.Add(passengerBooking);
                //int id = 1;
                //ShipAccount sa = db.ShipAccounts.Find(id);
                //sa.AccountBalance += (decimal)passengerBooking.TotalPrice;
                //db.Entry(sa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Users");
           

            ViewBag.ShipId = new SelectList(db.Ships, "ShipId", "ShipModel", passengerBooking.ShipId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", passengerBooking.UserId);
            return View(passengerBooking);
        }

        #endregion


        // GET: PassengerBookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PassengerBooking passengerBooking = db.PassengerBookings.Find(id);
            if (passengerBooking == null)
            {
                return HttpNotFound();
            }
            ViewBag.ShipId = new SelectList(db.Ships, "ShipId", "ShipModel", passengerBooking.ShipId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", passengerBooking.UserId);
            return View(passengerBooking);
        }

        // POST: PassengerBookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PassengerBookingId,UserId,ShipId,Tickets_Available,TicketCost,NoOfTickets,TotalPrice,BookingDate")] PassengerBooking passengerBooking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(passengerBooking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ShipId = new SelectList(db.Ships, "ShipId", "ShipModel", passengerBooking.ShipId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", passengerBooking.UserId);
            return View(passengerBooking);
        }

        // GET: PassengerBookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PassengerBooking passengerBooking = db.PassengerBookings.Find(id);
            if (passengerBooking == null)
            {
                return HttpNotFound();
            }
            return View(passengerBooking);
        }

        // POST: PassengerBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PassengerBooking passengerBooking = db.PassengerBookings.Find(id);
            db.PassengerBookings.Remove(passengerBooking);
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

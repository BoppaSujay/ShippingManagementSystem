using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;
using ShippingManagementSystem.Models;

namespace ShippingManagementSystem.Controllers
{
    public class UsersController : Controller
    {
        private ShipManagementSystemEntities db = new ShipManagementSystemEntities();

        // GET: Users
        #region Index
        public ActionResult Index()
        {
            return View();
        }
        #endregion


        #region Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login([Bind(Include = "UserId,Password")] User user)
        {
                User use = db.Users.Find(user.UserId);
                if (user.Password == use.Password)
                {

                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error");
                }
            return View(user);
        }
        #endregion



        #region Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "UserId,FirstName,LastName,DoB,Gender,ContactNumber,Category,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }
        #endregion

        #region CargoShip
        public ActionResult CargoShip()
        {
            return View(db.Ships.ToList());
        }
        #endregion


        #region CruiseShip
        public ActionResult CruiseShip()
        {
            return View(db.Ships.ToList());
        }
        #endregion



        #region Report
        public void Report(int id, string type)
        {
            ExcelPackage Ep = new ExcelPackage();
            if (type == "cargo")
            {
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("CargoReport");
                Sheet.Cells["A1"].Value = "CargoId";
                Sheet.Cells["B1"].Value = "Product";
                Sheet.Cells["C1"].Value = "Quantity";
                Sheet.Cells["D1"].Value = "BookingDate";
                Sheet.Cells["E1"].Value = "Shipping Cost";
                int row = 2;
                foreach (var item in db.CargoBookings)
                {
                    if (item.UserId == id)
                    {
                        Sheet.Cells[string.Format("A{0}", row)].Value = item.CargoBookingId;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.Product;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.Quantity;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.BookingDate;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.ShippingCost;
                    }
                    row++;
                }
                Sheet.Cells["A:AZ"].AutoFitColumns();
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment: filename=" + $"CargoReport{DateTime.Now.Ticks.ToString()}.xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
            else
            {
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("PassengerReport");
                Sheet.Cells["A1"].Value = "BookingId";
                Sheet.Cells["B1"].Value = "TicketCost";
                Sheet.Cells["C1"].Value = "No Of Tickets";
                Sheet.Cells["D1"].Value = "Total Price";
                Sheet.Cells["E1"].Value = "Booking Date";
                int row = 2;
                foreach (var item in db.PassengerBookings)
                {
                    if (item.UserId == id)
                    {
                        Sheet.Cells[string.Format("A{0}", row)].Value = item.PassengerBookingId;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.TicketCost;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.NoOfTickets;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.TotalPrice;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.BookingDate;
                    }
                    row++;
                }
                Sheet.Cells["A:AZ"].AutoFitColumns();
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment: filename=" + $"PassengerReport{DateTime.Now.Ticks.ToString()}.xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
        }
        #endregion

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,FirstName,LastName,DoB,Gender,ContactNumber,Category,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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

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
    public class AdminsController : Controller
    {
        private ShipManagementSystemEntities db = new ShipManagementSystemEntities();

        // GET: Admins
        public ActionResult Index()
        {
            return View(db.Ships.ToList());
        }

        #region Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "AdminId,Password")] Admin admin)
        {
            Admin use = db.Admins.Find(admin.AdminId);
            if (use.Approval == true)
            {
                if (admin.Password == use.Password)
                {

                    return View("Index");
                }
                else
                {
                    return View("Error");
                }
            }
            else
            {
                return View("LoginError");
            }
            return View(admin);
        }
        #endregion

        #region Register
        // GET: Admins/Create
        public ActionResult Register()
        {
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "AdminId,FirstName,LastName,DoB,Gender,ContactNumber,Category,Password")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Admins.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin);
        }

        #endregion


        #region Reports

        public void UserReport()
        {
            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("UserReport");
            Sheet.Cells["A1"].Value = "FirstName";
            Sheet.Cells["B1"].Value = "LastName";
            Sheet.Cells["C1"].Value = "DoB";
            Sheet.Cells["D1"].Value = "Gender";
            Sheet.Cells["E1"].Value = "ContactNumber";
            Sheet.Cells["F1"].Value = "UserId";
            int row = 2;
            foreach (var item in db.Users)
            {
                Sheet.Cells[string.Format("A{0}", row)].Value = item.FirstName;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.LastName;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.DoB;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.Gender;
                Sheet.Cells[string.Format("E{0}", row)].Value = item.ContactNumber;
                Sheet.Cells[string.Format("F{0}", row)].Value = item.UserId;
                row++;
            }
            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + $"UserReport_{DateTime.Now.Ticks.ToString()}.xlsx");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
        }
        public void EmployeeReport()
        {
            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("EmployeeReport");
            Sheet.Cells["A1"].Value = "EmployeeId";
            Sheet.Cells["B1"].Value = "EmployeeName";
            Sheet.Cells["C1"].Value = "Designation";
            Sheet.Cells["D1"].Value = "Address";
            Sheet.Cells["E1"].Value = "Salary";
            int row = 2;
            foreach (var item in db.Employees)
            {
                Sheet.Cells[string.Format("A{0}", row)].Value = item.EmployeeId;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.EmployeeName;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.Designation;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.Address;
                Sheet.Cells[string.Format("E{0}", row)].Value = item.Salary;
                row++;
            }
            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + $"EmployeeReport_{DateTime.Now.Ticks.ToString()}.xlsx");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
        }
        public void CargoReport()
        {
            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("CargoReport");
            Sheet.Cells["A1"].Value = "ShipId";
            Sheet.Cells["B1"].Value = "ShipNumber";
            Sheet.Cells["C1"].Value = "ShipModel";
            Sheet.Cells["D1"].Value = "Source";
            Sheet.Cells["E1"].Value = "Destination";
            Sheet.Cells["F1"].Value = "DepartureDate";
            Sheet.Cells["G1"].Value = "DepartureTime";
            int row = 2;
            foreach (var item in db.Ships)
            {
                if (item.ShipType == "Cargo")
                {
                    Sheet.Cells[string.Format("A{0}", row)].Value = item.ShipId;
                    Sheet.Cells[string.Format("B{0}", row)].Value = item.ShipNumber;
                    Sheet.Cells[string.Format("C{0}", row)].Value = item.ShipModel;
                    Sheet.Cells[string.Format("D{0}", row)].Value = item.Source;
                    Sheet.Cells[string.Format("E{0}", row)].Value = item.Destination;
                    Sheet.Cells[string.Format("E{0}", row)].Value = item.DepartureDate;
                    Sheet.Cells[string.Format("E{0}", row)].Value = item.DepartureTime;
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
        public void PassengerReport()
        {
            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("PassengerReport");
            Sheet.Cells["A1"].Value = "ShipId";
            Sheet.Cells["B1"].Value = "ShipNumber";
            Sheet.Cells["C1"].Value = "ShipModel";
            Sheet.Cells["D1"].Value = "Source";
            Sheet.Cells["E1"].Value = "FacilityType";
            Sheet.Cells["F1"].Value = "Destination";
            Sheet.Cells["G1"].Value = "DepartureDate";
            Sheet.Cells["H1"].Value = "DepartureTime";
            int row = 2;
            foreach (var item in db.Ships)
            {
                if (item.ShipType == "Passenger")
                {
                    Sheet.Cells[string.Format("A{0}", row)].Value = item.ShipId;
                    Sheet.Cells[string.Format("B{0}", row)].Value = item.ShipNumber;
                    Sheet.Cells[string.Format("C{0}", row)].Value = item.ShipModel;
                    Sheet.Cells[string.Format("D{0}", row)].Value = item.Source;
                    Sheet.Cells[string.Format("D{0}", row)].Value = item.FacilityType;
                    Sheet.Cells[string.Format("E{0}", row)].Value = item.Destination;
                    Sheet.Cells[string.Format("E{0}", row)].Value = item.DepartureDate;
                    Sheet.Cells[string.Format("E{0}", row)].Value = item.DepartureTime;
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
        public void FeedbackReport()
        {
            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("FeedbackReport");
            Sheet.Cells["A1"].Value = "FeedbackId";
            Sheet.Cells["B1"].Value = "UserId";
            Sheet.Cells["C1"].Value = "Correctness";
            Sheet.Cells["D1"].Value = "Safety";
            Sheet.Cells["E1"].Value = "RateOfService";
            int row = 2;
            foreach (var item in db.Feedbacks)
            {
                Sheet.Cells[string.Format("A{0}", row)].Value = item.FeedbackId;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.UserId;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.Correctness;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.Safety;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.Rate_of_Service;
                row++;
            }
            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + $"FeedbackReport{DateTime.Now.Ticks.ToString()}.xlsx");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
        }

        #endregion



        #region Acccounts Section

        #region Purchasing Ships
        public ActionResult PurchaseShip(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShipAccount ship = db.ShipAccounts.Find(id);
            if (ship == null)
            {
                return HttpNotFound();
            }
            return View(ship);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PurchaseShip(int ShipAccountId, int AccountBalance)
        {
            if (ModelState.IsValid)
            {
                ShipAccount ship = db.ShipAccounts.Find(ShipAccountId);
                ship.AccountBalance -= AccountBalance;
                db.Entry(ship).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(AccountBalance);
        }


        #endregion

        #region Profits
        public ActionResult ProfitShip(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShipAccount ship = db.ShipAccounts.Find(id);
            if (ship == null)
            {
                return HttpNotFound();
            }
            return View(ship);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProfitShip(int ShipAccountId, int AccountBalance)
        {
            if (ModelState.IsValid)
            {
                ShipAccount ship = db.ShipAccounts.Find(ShipAccountId);
                ship.AccountBalance += AccountBalance;
                db.Entry(ship).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(AccountBalance);
        }


        #endregion



        #region Employee salary
        public ActionResult EmployeeSalary(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShipAccount ship = db.ShipAccounts.Find(id);
            if (ship == null)
            {
                return HttpNotFound();
            }
            return View(ship);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmployeeSalary(int ShipAccountId, int AccountBalance)
        {
            if (ModelState.IsValid)
            {
                ShipAccount ship = db.ShipAccounts.Find(ShipAccountId);
                ship.AccountBalance -= AccountBalance;
                db.Entry(ship).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(AccountBalance);
        }

        #endregion


        #region Ticket Price Commented
        public ActionResult TicketPrice(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShipAccount ship = db.ShipAccounts.Find(id);
            if (ship == null)
            {
                return HttpNotFound();
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TicketPrice(int ShipAccountId, int AccountBalance)
        {
            if (ModelState.IsValid)
            {
                ShipAccount ship = db.ShipAccounts.Find(ShipAccountId);
                CargoBooking cg = db.CargoBookings.Find(AccountBalance);
                ship.AccountBalance += (decimal)cg.ShippingCost;
                db.Entry(ship).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(AccountBalance);
        }

        #endregion


        #endregion

        // GET: Admins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }




        // GET: Admins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdminId,FirstName,LastName,DoB,Gender,ContactNumber,Category,Password,Approval")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        // GET: Admins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin admin = db.Admins.Find(id);
            db.Admins.Remove(admin);
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

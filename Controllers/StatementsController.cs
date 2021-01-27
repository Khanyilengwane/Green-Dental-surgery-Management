using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Green.Models;

namespace Green.Controllers
{
    public class StatementsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Statements
        public ActionResult Index()
        {
            return View(db.Statements.ToList());
        }

        // GET: Statements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Statement statement = db.Statements.Find(id);
            if (statement == null)
            {
                return HttpNotFound();
            }
            return View(statement);
        }

        // GET: Statements/Create
        public ActionResult Create(int id)
        {


            var t = db.Appointments.Find(id).UserID;

            var model = new Statement
            {
                UserID = db.Appointments.Find(id).UserID,
                Username = db.Users.Find(db.Appointments.Find(id).UserID).Name.ToString(),
                DoctorID = db.Appointments.Find(id).DoctorID.ToString(),
                Date = db.Appointments.Find(id).Date,
                Time = db.Appointments.Find(id).Time,
                ProcedureName = db.Appointments.Find(id).ProcedureName,
                DueAmount = db.Appointments.Where(x => x.UserID ==t ).ToList().Sum(x => x.BookingPrice),


        }; 

            return View(model);
        }

        public ActionResult appointmentIndexYo()
        {
            return View(db.Appointments.ToList());

        }

        
        // POST: Statements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StatementID,DoctorID,UserID,Date,Time,Username,ProcedureName,refNumber,Total,DueAmount,paymAmoount")] Statement statement, int id)
        {
            var y = db.Appointments.Find(id).UserID;

            statement.UserID = db.Users.Find(y).Name;
            statement.DoctorID = db.Appointments.Find(id).DoctorID.ToString();
            statement.Date = db.Appointments.Find(id).Date;
            statement.Time = db.Appointments.Find(id).Time;
            statement.ProcedureName = db.Appointments.Find(id).ProcedureName;
           

                
           
            statement.Total = statement.DueAmount-statement.paymAmoount;

            statement.DueAmount = statement.Total;


            if (ModelState.IsValid)
            {

                Statement kc = new Statement();
                statement.refNumber = kc.GeneratePassword();
                db.Statements.Add(statement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(statement);
        }

        // GET: Statements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Statement statement = db.Statements.Find(id);
            if (statement == null)
            {
                return HttpNotFound();
            }
            return View(statement);
        }

        // POST: Statements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StatementID,DoctorID,refNumber,UserID,Date,Time,ProcedureName,Total,Username,DueAmount,paymAmoount")] Statement statement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(statement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(statement);
        }

        // GET: Statements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Statement statement = db.Statements.Find(id);
            if (statement == null)
            {
                return HttpNotFound();
            }
            return View(statement);
        }

        // POST: Statements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Statement statement = db.Statements.Find(id);
            db.Statements.Remove(statement);
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

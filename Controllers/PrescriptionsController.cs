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
    public class PrescriptionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Prescriptions
        public ActionResult Index()
        {
            
            return View(db.Prescriptions.ToList());
        }

        // GET: Prescriptions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prescription prescription = db.Prescriptions.Find(id);
            if (prescription == null)
            {
                return HttpNotFound();
            }
            return View(prescription);
        }

        // GET: Prescriptions/Create
        public ActionResult Create(int id)
        {

            var t = db.Appointments.Find(id).DoctorID;
            var y = db.Appointments.Find(id).UserID;

            var model = new Prescription
            {
                AppointmentID = id,
                DoctorID = db.Doctors.Find(t).Name,
                UserID = db.Users.Find(y).Name,
                Date = db.Appointments.Find(id).Date,
                
            };
            return View(model);
        }

        // POST: Prescriptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "prescriptionId,AppointmentID,timesPerDay,DoctorID,Date,UserID,unitPerTime,MedicineName")] Prescription prescription, string id)
        {
            var t = db.Appointments.Find(Convert.ToInt32(id)).DoctorID;
            var y = db.Appointments.Find(Convert.ToInt32(id)).UserID;



            prescription.AppointmentID = Convert.ToInt32(id);
            prescription.DoctorID = db.Doctors.Find(t).Name;
            prescription.UserID = db.Users.Find(y).Name;
            //recnotes.ProcedureName = db.Appointments.Find(Convert.ToInt32(id)).ProcedureName;
            prescription.Date = db.Appointments.Find(Convert.ToInt32(id)).Date;
            ///recnotes.Time = db.Appointments.Find(Convert.ToInt32(id)).Time;




            if (ModelState.IsValid)
            {
                db.Prescriptions.Add(prescription);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(prescription);
        }

        // GET: Prescriptions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prescription prescription = db.Prescriptions.Find(id);
            if (prescription == null)
            {
                return HttpNotFound();
            }
            return View(prescription);
        }

        // POST: Prescriptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "prescriptionId,AppointmentID,timesPerDay,DoctorID,Date,UserID,unitPerTime,MedicineName")] Prescription prescription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prescription).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(prescription);
        }

        // GET: Prescriptions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prescription prescription = db.Prescriptions.Find(id);
            if (prescription == null)
            {
                return HttpNotFound();
            }
            return View(prescription);
        }

        // POST: Prescriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prescription prescription = db.Prescriptions.Find(id);
            db.Prescriptions.Remove(prescription);
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

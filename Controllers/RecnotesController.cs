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
    public class RecnotesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Recnotes
        public ActionResult Index()
        {
            var recnotes = db.Recnotes.Include(r => r.AppointmentModel);
            return View(recnotes.ToList());
        }


        public ActionResult LabIndex()
        {
            var recnotes = db.Recnotes.Include(r => r.AppointmentModel);
            return View(recnotes.Where(x=>x.Upload==true));
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LabComment([Bind(Include = "RecID,AppointmentID,DoctorID,UserID,SideNotes,ProcedureName,Date,Time,Upload")] Recnotes recnotes, string id)
        {
            var t = db.Appointments.Find(Convert.ToInt32(id)).DoctorID;
            var y = db.Appointments.Find(Convert.ToInt32(id)).UserID;



            recnotes.AppointmentID = Convert.ToInt32(id);
            recnotes.DoctorID = db.Doctors.Find(t).Name;
            recnotes.UserID = db.Users.Find(y).Name;
            recnotes.ProcedureName = db.Appointments.Find(Convert.ToInt32(id)).ProcedureName;
            recnotes.Date = db.Appointments.Find(Convert.ToInt32(id)).Date;
            recnotes.Time = db.Appointments.Find(Convert.ToInt32(id)).Time;

            if (ModelState.IsValid)
            {
                db.Recnotes.Add(recnotes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //  ViewBag.AppointmentID = new SelectList(db.Appointments, "AppointmentID", "UserID", recnotes.AppointmentID);
            return View(recnotes);
        }


        // GET: Recnotes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recnotes recnotes = db.Recnotes.Find(id);
            if (recnotes == null)
            {
                return HttpNotFound();
            }
            return View(recnotes);
        }

        // GET: Recnotes/Create
        public ActionResult Create(int id)
        {
            var t = db.Appointments.Find(id).DoctorID;
            var y = db.Appointments.Find(id).UserID;

            var model = new Recnotes
            {
                AppointmentID = id,
                DoctorID = db.Doctors.Find(t).Name,
                UserID = db.Users.Find(y).Name,
                ProcedureName = db.Appointments.Find(id).ProcedureName,
                Date = db.Appointments.Find(id).Date,
                Time = db.Appointments.Find(id).Time,
               
            };
            
           // ViewBag.AppointmentID = new SelectList(db.Appointments, "AppointmentID", "UserID");
            return View(model);
        }

        // POST: Recnotes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecID,AppointmentID,DoctorID,UserID,SideNotes,ProcedureName,Date,Time,Upload,Tooth,Picture")] Recnotes recnotes, string id/*, HttpPostedFileBase img_upload*/)
        {
            //byte[] data = null;
            //data = new byte[img_upload.ContentLength];
            //img_upload.InputStream.Read(data, 0, img_upload.ContentLength);
            //recnotes.Picture = data;


            var t = db.Appointments.Find(Convert.ToInt32(id)).DoctorID;
            var y = db.Appointments.Find(Convert.ToInt32(id)).UserID;



            recnotes.AppointmentID = Convert.ToInt32(id);
            recnotes.DoctorID = db.Doctors.Find(t).Name;
            recnotes.UserID = db.Users.Find(y).Name;
            recnotes.ProcedureName = db.Appointments.Find(Convert.ToInt32(id)).ProcedureName;
            recnotes.Date = db.Appointments.Find(Convert.ToInt32(id)).Date;
            recnotes.Time = db.Appointments.Find(Convert.ToInt32(id)).Time;
            

            if (ModelState.IsValid)
            {
                db.Recnotes.Add(recnotes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

          //  ViewBag.AppointmentID = new SelectList(db.Appointments, "AppointmentID", "UserID", recnotes.AppointmentID);
            return View(recnotes);
        }

        // GET: Recnotes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recnotes recnotes = db.Recnotes.Find(id);
            if (recnotes == null)
            {
                return HttpNotFound();
            }
         //   ViewBag.AppointmentID = new SelectList(db.Appointments, "AppointmentID", "UserID", recnotes.AppointmentID);
            return View(recnotes);
        }

        // POST: Recnotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecID,AppointmentID,DoctorID,UserID,SideNotes,ProcedureName,Date,Time,Upload,Tooth")] Recnotes recnotes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recnotes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           // ViewBag.AppointmentID = new SelectList(db.Appointments, "AppointmentID", "UserID", recnotes.AppointmentID);
            return View(recnotes);
        }

        // GET: Recnotes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recnotes recnotes = db.Recnotes.Find(id);
            if (recnotes == null)
            {
                return HttpNotFound();
            }
            return View(recnotes);
        }

        // POST: Recnotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recnotes recnotes = db.Recnotes.Find(id);
            db.Recnotes.Remove(recnotes);
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

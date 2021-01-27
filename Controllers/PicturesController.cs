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
    public class PicturesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Pictures
        public ActionResult Index()
        {
            return View(db.Pictures.ToList());
        }


        // GET: Pictures/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pictur pictures = db.Pictures.Find(id);
            if (pictures == null)
            {
                return HttpNotFound();
            }
            return View(pictures);
        }

        // GET: Pictures/Create
        public ActionResult Create(int id)
        {

            var t = db.Recnotes.Find(id).DoctorID;
            var y = db.Recnotes.Find(id).UserID;

            var model = new Pictur
            {
                AppointmentID = id,
                DoctorID = t,
                UserID = y,
                ProcedureName = db.Recnotes.Find(id).ProcedureName,
                Date = db.Recnotes.Find(id).Date,
                Time = db.Recnotes.Find(id).Time,
                SideNotes= db.Recnotes.Find(id).SideNotes,

            };

            // ViewBag.AppointmentID = new SelectList(db.Appointments, "AppointmentID", "UserID");
            return View(model);
        }

        // POST: Pictures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PicID,Description,ProcedureName,DoctorID,DoctorID,SideNotes,Date,Time,ProcedureName")] Pictur pictures, string id)
        {
            //byte[] data = null;
            //data = new byte[img_upload.ContentLength];
            //img_upload.InputStream.Read(data, 0, img_upload.ContentLength);
            //pictures.Picture = data;


            var t = db.Recnotes.Find(Convert.ToInt32(id)).DoctorID;
            var y = db.Recnotes.Find(Convert.ToInt32(id)).UserID;



            pictures.AppointmentID = Convert.ToInt32(id);
            pictures.DoctorID = t;
            pictures.UserID = y;
            pictures.ProcedureName = db.Recnotes.Find(Convert.ToInt32(id)).ProcedureName;
            pictures.Date = db.Recnotes.Find(Convert.ToInt32(id)).Date;
            pictures.Time = db.Recnotes.Find(Convert.ToInt32(id)).Time;
            pictures.SideNotes = db.Recnotes.Find(Convert.ToInt32(id)).SideNotes;

            if (ModelState.IsValid)
            {
                db.Pictures.Add(pictures);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pictures);
        }

        // GET: Pictures/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pictur pictures = db.Pictures.Find(id);
            if (pictures == null)
            {
                return HttpNotFound();
            }
            return View(pictures);
        }

        // POST: Pictures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PicID,Description,ProcedureName,DoctorID,DoctorID,SideNotes,Date,Time,ProcedureName")] Pictur pictures)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pictures).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pictures);
        }

        // GET: Pictures/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pictur pictures = db.Pictures.Find(id);
            if (pictures == null)
            {
                return HttpNotFound();
            }
            return View(pictures);
        }

        // POST: Pictures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pictur pictures = db.Pictures.Find(id);
            db.Pictures.Remove(pictures);
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

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
    public class LaboratiriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Laboratiries
        public ActionResult Index()
        {
            return View(db.Laboratiries.ToList());
        }

        // GET: Laboratiries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laboratiries laboratiries = db.Laboratiries.Find(id);
            if (laboratiries == null)
            {
                return HttpNotFound();
            }
            return View(laboratiries);
        }

        // GET: Laboratiries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Laboratiries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LabID,LabName")] Laboratiries laboratiries/*, HttpPostedFileBase img_upload*/)
        {
            //    byte[] data = null;
            //    data = new byte[img_upload.ContentLength];
            //    img_upload.InputStream.Read(data, 0, img_upload.ContentLength);
            //    laboratiries.Picture = data;

            if (ModelState.IsValid)
            {
                db.Laboratiries.Add(laboratiries);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(laboratiries);
        }

        // GET: Laboratiries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laboratiries laboratiries = db.Laboratiries.Find(id);
            if (laboratiries == null)
            {
                return HttpNotFound();
            }
            return View(laboratiries);
        }

        // POST: Laboratiries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LabID,LabName")] Laboratiries laboratiries)
        {
            if (ModelState.IsValid)
            {
                db.Entry(laboratiries).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(laboratiries);
        }

        // GET: Laboratiries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laboratiries laboratiries = db.Laboratiries.Find(id);
            if (laboratiries == null)
            {
                return HttpNotFound();
            }
            return View(laboratiries);
        }

        // POST: Laboratiries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Laboratiries laboratiries = db.Laboratiries.Find(id);
            db.Laboratiries.Remove(laboratiries);
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

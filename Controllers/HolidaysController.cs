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
    public class HolidaysController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Holidays
        public ActionResult Index()
           
        {
            return View(db.Holidays.ToList());
        }

        // GET: Holidays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Holidays holidays = db.Holidays.Find(id);
            if (holidays == null)
            {
                return HttpNotFound();
            }
            return View(holidays);
        }

        // GET: Holidays/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Holidays/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HolidayId,day")] Holidays holidays)
        {
            if (ModelState.IsValid)
            {
                db.Holidays.Add(holidays);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(holidays);
        }

        // GET: Holidays/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Holidays holidays = db.Holidays.Find(id);
            if (holidays == null)
            {
                return HttpNotFound();
            }
            return View(holidays);
        }

        // POST: Holidays/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HolidayId,day")] Holidays holidays)
        {
            if (ModelState.IsValid)
            {
                db.Entry(holidays).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(holidays);
        }

        // GET: Holidays/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Holidays holidays = db.Holidays.Find(id);
            if (holidays == null)
            {
                return HttpNotFound();
            }
            return View(holidays);
        }

        // POST: Holidays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Holidays holidays = db.Holidays.Find(id);
            db.Holidays.Remove(holidays);
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

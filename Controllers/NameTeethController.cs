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
    public class NameTeethController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: NameTeeth
        public ActionResult Index()
        {
            return View(db.NameTeeth.ToList());
        }

        // GET: NameTeeth/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NameTooth nameTooth = db.NameTeeth.Find(id);
            if (nameTooth == null)
            {
                return HttpNotFound();
            }
            return View(nameTooth);
        }

        // GET: NameTeeth/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NameTeeth/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "toothID,toothName")] NameTooth nameTooth)
        {
            if (ModelState.IsValid)
            {
                db.NameTeeth.Add(nameTooth);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nameTooth);
        }

        // GET: NameTeeth/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NameTooth nameTooth = db.NameTeeth.Find(id);
            if (nameTooth == null)
            {
                return HttpNotFound();
            }
            return View(nameTooth);
        }

        // POST: NameTeeth/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "toothID,toothName")] NameTooth nameTooth)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nameTooth).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nameTooth);
        }

        // GET: NameTeeth/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NameTooth nameTooth = db.NameTeeth.Find(id);
            if (nameTooth == null)
            {
                return HttpNotFound();
            }
            return View(nameTooth);
        }

        // POST: NameTeeth/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NameTooth nameTooth = db.NameTeeth.Find(id);
            db.NameTeeth.Remove(nameTooth);
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

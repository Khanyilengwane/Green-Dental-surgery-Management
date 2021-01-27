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
    public class TeethController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Teeth
        public ActionResult Index()
        {
            var teeth = db.Teeth.Include(t => t.Procedure);
            return View(teeth.ToList());
        }

        // GET: Teeth/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teeth teeth = db.Teeth.Find(id);
            if (teeth == null)
            {
                return HttpNotFound();
            }
            return View(teeth);
        }

        // GET: Teeth/Create
        public ActionResult Create()
        {
            ViewBag.ProcedurID = new SelectList(db.Procedures, "ProcedurID", "ProcedureName");
            return View();
        }

        // POST: Teeth/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeethID,TeethName,ProcedurID")] Teeth teeth)
        {
            if (ModelState.IsValid)
            {
                db.Teeth.Add(teeth);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProcedurID = new SelectList(db.Procedures, "ProcedurID", "ProcedureName", teeth.ProcedurID);
            return View(teeth);
        }

        // GET: Teeth/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                
            }
            Teeth teeth = db.Teeth.Find(id);
            if (teeth == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProcedurID = new SelectList(db.Procedures, "ProcedurID", "ProcedureName", teeth.ProcedurID);
            return View(teeth);
        }

        // POST: Teeth/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeethID,TeethName,ProcedurID")] Teeth teeth)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teeth).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProcedurID = new SelectList(db.Procedures, "ProcedurID", "ProcedureName", teeth.ProcedurID);
            return View(teeth);
        }

        // GET: Teeth/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teeth teeth = db.Teeth.Find(id);
            if (teeth == null)
            {
                return HttpNotFound();
            }
            return View(teeth);
        }

        // POST: Teeth/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Teeth teeth = db.Teeth.Find(id);
            db.Teeth.Remove(teeth);
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
        #region clicks
        public void SetClicks(string id)
        {

        }
        #endregion
    }
}

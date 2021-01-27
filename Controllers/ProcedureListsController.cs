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
    public class ProcedureListsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProcedureLists
        public ActionResult Index()
        {
            var procedureLists = db.ProcedureLists.Include(p => p.Procedure);
            return View(procedureLists.ToList());
        }

        // GET: ProcedureLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcedureList procedureList = db.ProcedureLists.Find(id);
            if (procedureList == null)
            {
                return HttpNotFound();
            }
            return View(procedureList);
        }

        // GET: ProcedureLists/Create
        public ActionResult Create(int id)
        {
            var mode = new ProcedureList
            {

                ToothName = db.NameTeeth.Find(id).toothName,


            };
            ViewBag.ProcedurID = new SelectList(db.Procedures, "ProcedurID", "ProcedureName");
            return View(mode);
        }

        // POST: ProcedureLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProID,ProcedurID,ToothName")] ProcedureList procedureList)
        {
            if (ModelState.IsValid)
            {
                db.ProcedureLists.Add(procedureList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProcedurID = new SelectList(db.Procedures, "ProcedurID", "ProcedureName", procedureList.ProcedurID);
            return View(procedureList);
        }

        // GET: ProcedureLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcedureList procedureList = db.ProcedureLists.Find(id);
            if (procedureList == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProcedurID = new SelectList(db.Procedures, "ProcedurID", "ProcedureName", procedureList.ProcedurID);
            return View(procedureList);
        }

        // POST: ProcedureLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProID,ProcedurID,ToothName")] ProcedureList procedureList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(procedureList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProcedurID = new SelectList(db.Procedures, "ProcedurID", "ProcedureName", procedureList.ProcedurID);
            return View(procedureList);
        }

        // GET: ProcedureLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcedureList procedureList = db.ProcedureLists.Find(id);
            if (procedureList == null)
            {
                return HttpNotFound();
            }
            return View(procedureList);
        }

        // POST: ProcedureLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProcedureList procedureList = db.ProcedureLists.Find(id);
            db.ProcedureLists.Remove(procedureList);
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

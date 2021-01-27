using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Green.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using SendGrid.Helpers.Mail;

namespace Green.Controllers
{
    public class LabAssistancesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public LabAssistancesController()
           : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        public LabAssistancesController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }



        // GET: LabAssistances
        public ActionResult Index()
        {
            var labAssistances = db.LabAssistances.Include(l => l.Laboratiries);
            return View(labAssistances.ToList());
        }

        // GET: LabAssistances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LabAssistance labAssistance = db.LabAssistances.Find(id);
            if (labAssistance == null)
            {
                return HttpNotFound();
            }
            return View(labAssistance);
        }

        // GET: LabAssistances/Create
        public ActionResult Create()
        {
            ViewBag.LabID = new SelectList(db.Laboratiries, "LabID", "LabName");
            return View();
        }

        // POST: LabAssistances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AssID,Name,Email,BirthDate,Sex,Picture,LabID")] LabAssistance labAssistance)
        {
            if (ModelState.IsValid)
            {
                var user = labAssistance.GetUser();
                LabAssistance dm = new LabAssistance();

                string password = dm.GeneratePassword();

                var result = await UserManager.CreateAsync(user, password);

                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

                if (result.Succeeded)
                {

                    if (!roleManager.RoleExists("LabAssistance"))
                    {
                        roleManager.Create(new IdentityRole("LabAssistance"));
                    }
                    await UserManager.AddToRoleAsync(user.Id, "LabAssistance");

                    var myMessage = new SendGridMessage
                    {
                        From = new EmailAddress("no-reply@Code-Hub.com", "No-Reply")
                    };
                    myMessage.AddTo(user.Email);
                    string subject = "Registration Received";
                    string body = ("Hi " + user.Name + " " + "\n" + "Your password is  " + "<b>" + password + "</b>" + "  ." +
                    "\n" + " Ensure not to share your password with anyone...  Have a great day." + "\n");
                    myMessage.Subject = subject;
                    myMessage.HtmlContent = body;
                    var transportWeb = new SendGrid.SendGridClient("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");
                    await transportWeb.SendEmailAsync(myMessage);

                    db.LabAssistances.Add(labAssistance);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.LabID = new SelectList(db.Laboratiries, "LabID", "LabName", labAssistance.LabID);
            return View(labAssistance);
        }

        // GET: LabAssistances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LabAssistance labAssistance = db.LabAssistances.Find(id);
            if (labAssistance == null)
            {
                return HttpNotFound();
            }
            ViewBag.LabID = new SelectList(db.Laboratiries, "LabID", "LabName", labAssistance.LabID);
            return View(labAssistance);
        }

        // POST: LabAssistances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssID,Name,Email,BirthDate,Sex,Picture,LabID")] LabAssistance labAssistance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(labAssistance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LabID = new SelectList(db.Laboratiries, "LabID", "LabName", labAssistance.LabID);
            return View(labAssistance);
        }

        // GET: LabAssistances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LabAssistance labAssistance = db.LabAssistances.Find(id);
            if (labAssistance == null)
            {
                return HttpNotFound();
            }
            return View(labAssistance);
        }

        // POST: LabAssistances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LabAssistance labAssistance = db.LabAssistances.Find(id);
            db.LabAssistances.Remove(labAssistance);
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

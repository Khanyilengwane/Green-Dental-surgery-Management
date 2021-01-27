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
    public class ReceptionistsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ReceptionistsController()
           : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        public ReceptionistsController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        // GET: Receptionists
        public ActionResult Index()
        {
            return View(db.Receptionists.ToList());
        }

        // GET: Receptionists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receptionist receptionist = db.Receptionists.Find(id);
            if (receptionist == null)
            {
                return HttpNotFound();
            }
            return View(receptionist);
        }

        // GET: Receptionists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Receptionists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ReceptionID,Name,Surname,Email,BirthDate,Sex")] Receptionist receptionist)
        {
            if (ModelState.IsValid)
            {
                var user = receptionist.GetUser();
                Receptionist dm = new Receptionist();

                string password = dm.GeneratePassword();

                var result = await UserManager.CreateAsync(user, password);

                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

                if (result.Succeeded)
                {

                    if (!roleManager.RoleExists("Receptionist"))
                    {
                        roleManager.Create(new IdentityRole("Receptionist"));
                    }
                    await UserManager.AddToRoleAsync(user.Id, "Receptionist");

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
                    
                    db.Receptionists.Add(receptionist);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(receptionist);
        }

        // GET: Receptionists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receptionist receptionist = db.Receptionists.Find(id);
            if (receptionist == null)
            {
                return HttpNotFound();
            }
            return View(receptionist);
        }

        // POST: Receptionists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReceptionID,Name,Surname,Email,BirthDate,Sex")] Receptionist receptionist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(receptionist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(receptionist);
        }

        // GET: Receptionists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receptionist receptionist = db.Receptionists.Find(id);
            if (receptionist == null)
            {
                return HttpNotFound();
            }
            return View(receptionist);
        }

        // POST: Receptionists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Receptionist receptionist = db.Receptionists.Find(id);
            db.Receptionists.Remove(receptionist);
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

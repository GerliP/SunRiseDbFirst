using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SunRiseDbFirst.Models;

namespace SunRiseDbFirst.Controllers
{
    public class LoginController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(SunRiseDbFirst.Models.User user)
        {
            var userDetails = db.Users.Where(m => m.Email == user.Email && m.Password == user.Password).FirstOrDefault();
            if (userDetails == null)
            {
                ViewBag.LoginErrorMessage = "Incorrect email or password";
                return View("Login"); 
            }
            else
            {
                Session["Id"] = userDetails.Id;
                Session["Firstname"] = userDetails.Firstname;
                int role = userDetails.Role_id;
               
                return FindView(role);

            }
    }
         
        public ActionResult FindView(int role)
{
    if (role == 1) // Admin
    {
        return RedirectToAction("Index", "Admin");
    }
    else if(role == 2) // Patinet
    {
        return RedirectToAction("Index", "Patient");
    }
    else
    {
        return RedirectToAction("Index", "Caretaker"); //Caretaker
    }
}
        public ActionResult Logout(User user)
        {
            int userId = (int)Session["Id"];
            Session.Abandon();
            return RedirectToAction("Login", "Login");

        }
// GET: Login/Details/5
public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            ViewBag.Role_id = new SelectList(db.Roles, "Id", "Name");
            return View();
        }

        // POST: Login/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Firstname,Lastname,Email,Password,Address,ContactNumber,Role_id,Gender")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Role_id = new SelectList(db.Roles, "Id", "Name", user.Role_id);
            return View(user);
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.Role_id = new SelectList(db.Roles, "Id", "Name", user.Role_id);
            return View(user);
        }

        // POST: Login/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Firstname,Lastname,Email,Password,Address,ContactNumber,Role_id,Gender")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Role_id = new SelectList(db.Roles, "Id", "Name", user.Role_id);
            return View(user);
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Login/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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

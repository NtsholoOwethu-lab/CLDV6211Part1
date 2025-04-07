using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentRegistrationAppPart2.Models;

namespace StudentRegistrationAppPart2.Controllers
{
    public class StudentInputController : Controller
    {
        private RegistrationDBContext2 db = new RegistrationDBContext2();

        // GET: StudentInput
        public ActionResult Index()
        {
            return View(db.Students.ToList());
        }

        // GET: StudentInput/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentInput studentInput = db.Students.Find(id);
            if (studentInput == null)
            {
                return HttpNotFound();
            }
            return View(studentInput);
        }

        // GET: StudentInput/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentInput/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentID,Name,Surname,Email")] StudentInput studentInput)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(studentInput);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(studentInput);
        }

        // GET: StudentInput/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentInput studentInput = db.Students.Find(id);
            if (studentInput == null)
            {
                return HttpNotFound();
            }
            return View(studentInput);
        }

        // POST: StudentInput/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentID,Name,Surname,Email")] StudentInput studentInput)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentInput).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(studentInput);
        }

        // GET: StudentInput/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentInput studentInput = db.Students.Find(id);
            if (studentInput == null)
            {
                return HttpNotFound();
            }
            return View(studentInput);
        }

        // POST: StudentInput/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentInput studentInput = db.Students.Find(id);
            db.Students.Remove(studentInput);
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

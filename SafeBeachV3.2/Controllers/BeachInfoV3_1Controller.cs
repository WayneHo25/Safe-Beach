using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SafeBeachV3._2.Models;

namespace SafeBeachV3._2.Controllers
{
    public class BeachInfoV3_1Controller : Controller
    {
        private BeachInfoEntities db = new BeachInfoEntities();

        // GET: BeachInfoV3_1
        public ActionResult Index()
        {
            return View(db.BeachInfoV3_1.ToList());
        }

        // GET: BeachInfoV3_1/Details/5
        public ActionResult Details(double? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BeachInfoV3_1 beachInfoV3_1 = db.BeachInfoV3_1.Find(id);
            if (beachInfoV3_1 == null)
            {
                return HttpNotFound();
            }
            return View(beachInfoV3_1);
        }

        // GET: BeachInfoV3_1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BeachInfoV3_1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "beachID,beachName,beachAddress,region,beachSuburb,postcode,latitude,longitude,patrol,patrolTime,surfLevel,swimLevel,drowning,waterQuality")] BeachInfoV3_1 beachInfoV3_1)
        {
            if (ModelState.IsValid)
            {
                db.BeachInfoV3_1.Add(beachInfoV3_1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(beachInfoV3_1);
        }

        // GET: BeachInfoV3_1/Edit/5
        public ActionResult Edit(double? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BeachInfoV3_1 beachInfoV3_1 = db.BeachInfoV3_1.Find(id);
            if (beachInfoV3_1 == null)
            {
                return HttpNotFound();
            }
            return View(beachInfoV3_1);
        }

        // POST: BeachInfoV3_1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "beachID,beachName,beachAddress,region,beachSuburb,postcode,latitude,longitude,patrol,patrolTime,surfLevel,swimLevel,drowning,waterQuality")] BeachInfoV3_1 beachInfoV3_1)
        {
            if (ModelState.IsValid)
            {
                db.Entry(beachInfoV3_1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(beachInfoV3_1);
        }

        // GET: BeachInfoV3_1/Delete/5
        public ActionResult Delete(double? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BeachInfoV3_1 beachInfoV3_1 = db.BeachInfoV3_1.Find(id);
            if (beachInfoV3_1 == null)
            {
                return HttpNotFound();
            }
            return View(beachInfoV3_1);
        }

        // POST: BeachInfoV3_1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(double id)
        {
            BeachInfoV3_1 beachInfoV3_1 = db.BeachInfoV3_1.Find(id);
            db.BeachInfoV3_1.Remove(beachInfoV3_1);
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

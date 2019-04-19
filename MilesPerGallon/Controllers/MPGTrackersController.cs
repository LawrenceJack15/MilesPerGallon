using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MilesPerGallon.Models;

namespace MilesPerGallon.Controllers
{
    public class MPGTrackersController : Controller
    {
        private MilesPerGallonEntities db = new MilesPerGallonEntities();
        //private MPGTracker MPGTracker = new MPGTracker();

        // GET: MPGTrackers
        public ActionResult Index(string sortOrder, string searchString, DateTime? StartDate, DateTime? EndDate)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;

            var tabledata = from s in db.MPGTrackers
                                select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                tabledata = tabledata.Where(s => s.LastName.Contains(searchString)
                                       || s.CarModel.Contains(searchString));
            }

            if (StartDate.HasValue)
            {
                tabledata = tabledata.Where(x => x.FillUpDate >= StartDate.Value);
            }
            if (EndDate.HasValue)
            {
                tabledata = tabledata.Where(x => x.FillUpDate <= EndDate.Value);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    tabledata = tabledata.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    tabledata = tabledata.OrderBy(s => s.StartDateUseOfCar);
                    tabledata = tabledata.OrderBy(s => s.EndDateUseOfCar);
                    tabledata = tabledata.OrderBy(s => s.FillUpDate);
                    break;
                case "date_desc":
                    tabledata = tabledata.OrderByDescending(s => s.StartDateUseOfCar);
                    tabledata = tabledata.OrderByDescending(s => s.EndDateUseOfCar);
                    tabledata = tabledata.OrderByDescending(s => s.FillUpDate);
                    break;
                default:
                    tabledata = tabledata.OrderBy(s => s.LastName);
                    break;
            }
            return View(tabledata.ToList());
        }

        // GET: MPGTrackers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MPGTrackers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,CarModel,MilesDriven,GallonsFilled,FillUpDate,StartDateUseOfCar,EndDateUseOfCar")] MPGTracker mPGTracker)
        {
            if (ModelState.IsValid)
            {
                db.MPGTrackers.Add(mPGTracker);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mPGTracker);
        }

        // GET: MPGTrackers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MPGTracker mPGTracker = db.MPGTrackers.Find(id);
            if (mPGTracker == null)
            {
                return HttpNotFound();
            }
            return View(mPGTracker);
        }

        // POST: MPGTrackers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,CarModel,MilesDriven,GallonsFilled,FillUpDate,StartDateUseOfCar,EndDateUseOfCar")] MPGTracker mPGTracker)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mPGTracker).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mPGTracker);
        }

        // GET: MPGTrackers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MPGTracker mPGTracker = db.MPGTrackers.Find(id);
            if (mPGTracker == null)
            {
                return HttpNotFound();
            }
            return View(mPGTracker);
        }

        // POST: MPGTrackers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MPGTracker mPGTracker = db.MPGTrackers.Find(id);
            db.MPGTrackers.Remove(mPGTracker);
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

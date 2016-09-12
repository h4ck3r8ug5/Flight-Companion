using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FlightCompanion.Controllers
{
    public class AdminController : Controller
    {
        private FlightCompanionEntities db = new FlightCompanionEntities();

        // GET: Admin
        public ActionResult Index()
        {
            var charts = db.Charts.Include(f => f.ChartTypeMetadata);
            return View(charts.ToList());
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            var chartTypes = OperationProcessor.GetChartTypes();
            ViewData["ChartTypes"] = chartTypes;
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int chartType, HttpPostedFileBase[] files)
        {
            var result = OperationProcessor.UploadChart(chartType, files, HttpContext);
            return result ? RedirectToAction("Index") : null;
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlightPlan flightPlan = db.FlightPlans.Find(id);
            if (flightPlan == null)
            {
                return HttpNotFound();
            }
            ViewBag.Departure = new SelectList(db.IcaoCodes, "Id", "Code", flightPlan.Departure);
            ViewBag.Destination = new SelectList(db.IcaoCodes, "Id", "Code", flightPlan.Destination);
            return View(flightPlan);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Departure,Destination,Distance,Waypoints")] FlightPlan flightPlan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flightPlan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Departure = new SelectList(db.IcaoCodes, "Id", "Code", flightPlan.Departure);
            ViewBag.Destination = new SelectList(db.IcaoCodes, "Id", "Code", flightPlan.Destination);
            return View(flightPlan);
        }

        //// GET: Admin/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    FlightPlan flightPlan = db.FlightPlans.Find(id);
        //    if (flightPlan == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(flightPlan);
        //}

        //// POST: Admin/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    FlightPlan flightPlan = db.FlightPlans.Find(id);
        //    db.FlightPlans.Remove(flightPlan);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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

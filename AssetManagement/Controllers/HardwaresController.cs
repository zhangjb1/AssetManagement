using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AssetManagement.Models;

namespace AssetManagement.Controllers
{
    public class HardwaresController : Controller
    {
        private AssetManagementEntities db = new AssetManagementEntities();

        // GET: Hardwares
        public ActionResult Index()
        {
            var hardwares = db.Hardwares.Include(h => h.PurchaseOrder).Include(h => h.Status).Include(h => h.Type1);
            return View(hardwares.ToList());
        }

        // GET: Hardwares/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hardware hardware = db.Hardwares.Find(id);
            if (hardware == null)
            {
                return HttpNotFound();
            }
            return View(hardware);
        }

        // GET: Hardwares/Create
        public ActionResult Create()
        {
            ViewBag.POID = new SelectList(db.PurchaseOrders, "ID", "PO_Number");
            ViewBag.StatusID = new SelectList(db.Status, "ID", "Description");
            ViewBag.Type = new SelectList(db.Types, "ID", "Description");
            return View();
        }

        // POST: Hardwares/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Type,Model,ServiceTag,POID,LabelName,StatusID")] Hardware hardware)
        {
            if (ModelState.IsValid)
            {
                db.Hardwares.Add(hardware);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.POID = new SelectList(db.PurchaseOrders, "ID", "PO_Number", hardware.POID);
            ViewBag.StatusID = new SelectList(db.Status, "ID", "Description", hardware.StatusID);
            ViewBag.Type = new SelectList(db.Types, "ID", "Description", hardware.Type);
            return View(hardware);
        }

        // GET: Hardwares/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hardware hardware = db.Hardwares.Find(id);
            if (hardware == null)
            {
                return HttpNotFound();
            }
            ViewBag.POID = new SelectList(db.PurchaseOrders, "ID", "PO_Number", hardware.POID);
            ViewBag.StatusID = new SelectList(db.Status, "ID", "Description", hardware.StatusID);
            ViewBag.Type = new SelectList(db.Types, "ID", "Description", hardware.Type);
            return View(hardware);
        }

        // POST: Hardwares/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Type,Model,ServiceTag,POID,LabelName,StatusID")] Hardware hardware)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hardware).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.POID = new SelectList(db.PurchaseOrders, "ID", "PO_Number", hardware.POID);
            ViewBag.StatusID = new SelectList(db.Status, "ID", "Description", hardware.StatusID);
            ViewBag.Type = new SelectList(db.Types, "ID", "Description", hardware.Type);
            return View(hardware);
        }

        // GET: Hardwares/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hardware hardware = db.Hardwares.Find(id);
            if (hardware == null)
            {
                return HttpNotFound();
            }
            return View(hardware);
        }

        // POST: Hardwares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hardware hardware = db.Hardwares.Find(id);
            db.Hardwares.Remove(hardware);
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

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
    public class SoftwaresController : Controller
    {
        private AssetManagementEntities db = new AssetManagementEntities();

        // GET: Softwares
        public ActionResult Index()
        {
            var softwares = db.Softwares.Include(s => s.PurchaseOrder).Include(s => s.Status);
            return View(softwares.ToList());
        }

        // GET: Softwares/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Software software = db.Softwares.Find(id);
            if (software == null)
            {
                return HttpNotFound();
            }
            return View(software);
        }

        // GET: Softwares/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.PurchaseOrders, "ID", "PO_Number");
            ViewBag.StatusID = new SelectList(db.Status, "ID", "Description");
            return View();
        }

        // POST: Softwares/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,POID,StatusID,LicenseNo")] Software software)
        {
            if (ModelState.IsValid)
            {
                db.Softwares.Add(software);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.PurchaseOrders, "ID", "PO_Number", software.ID);
            ViewBag.StatusID = new SelectList(db.Status, "ID", "Description", software.StatusID);
            return View(software);
        }

        // GET: Softwares/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Software software = db.Softwares.Find(id);
            if (software == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.PurchaseOrders, "ID", "PO_Number", software.ID);
            ViewBag.StatusID = new SelectList(db.Status, "ID", "Description", software.StatusID);
            return View(software);
        }

        // POST: Softwares/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,POID,StatusID,LicenseNo")] Software software)
        {
            if (ModelState.IsValid)
            {
                db.Entry(software).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.PurchaseOrders, "ID", "PO_Number", software.ID);
            ViewBag.StatusID = new SelectList(db.Status, "ID", "Description", software.StatusID);
            return View(software);
        }

        // GET: Softwares/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Software software = db.Softwares.Find(id);
            if (software == null)
            {
                return HttpNotFound();
            }
            return View(software);
        }

        // POST: Softwares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Software software = db.Softwares.Find(id);
            db.Softwares.Remove(software);
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AssetManagement.Models;
using AssetManagement.ViewModels;

namespace AssetManagement.Controllers
{
    public class SoftwaresController : Controller
    {
        private AssetManagementEntities db = new AssetManagementEntities();

        // GET: Softwares
        public ActionResult Index()
        {
            var softwares = db.Softwares.Include(s => s.PurchaseOrder).Include(s => s.Status);
            List<SoftwareVM> softwareList = new List<SoftwareVM>();
            foreach (Software s in softwares)
            {
                SoftwareVM sVM = new SoftwareVM();
                sVM.ID = s.ID;
                sVM.SoftwareName = s.Name;
                sVM.TotalLicNo = s.LicenseNo;
                sVM.PONumber = s.PurchaseOrder.PO_Number;
                if (s.Name.Contains("Office"))
                {
                    sVM.UsedLicNo = db.Assignments.Where(a => a.SoftwareID == s.ID).Count();
                } else if (s.Name.Contains("Visio"))
                {
                    sVM.UsedLicNo = db.Assignments.Where(a => a.VisioID == s.ID).Count();
                }
                 softwareList.Add(sVM);
            }
            return View(softwareList);
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
      
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: Softwares/Usedlic/5
        public ActionResult Usedlic(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsedlicVM usedliclVM = new UsedlicVM();
            usedliclVM.SoftwareName = db.Softwares.Find(id).Name;
            if (db.Softwares.Find(id).Name.Contains("Office"))
            {
                usedliclVM.UsedlicList = db.Assignments.Where(a => a.SoftwareID == id).Select(x => new UsedlicObj {
                                                                                                       UserName = x.Employee.FirstName,
                                                                                                       ComputerName = x.Hardware.LabelName
                                                                                                       }).ToList();
            }
            else if (db.Softwares.Find(id).Name.Contains("Visio"))
            {
                usedliclVM.UsedlicList = db.Assignments.Where(a => a.VisioID == id).Select(x => new UsedlicObj {
                                                                                                    UserName = x.Employee.FirstName,
                                                                                                    ComputerName = x.Hardware.LabelName
                                                                                                    }).ToList();
            }
            usedliclVM.UsedlicNo = usedliclVM.UsedlicList.Count();
            if (usedliclVM.UsedlicList == null)
            {
                return HttpNotFound();
            }
            return View(usedliclVM);
        }
    }
}

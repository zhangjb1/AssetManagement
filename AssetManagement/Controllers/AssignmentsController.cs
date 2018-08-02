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
    public class AssignmentsController : Controller
    {
        private AssetManagementEntities db = new AssetManagementEntities();

        // GET: Assignments
        public ActionResult Index()
        {
            var assignments = db.Assignments.Include(a => a.Employee).Include(a => a.Hardware).Include(a => a.Software);
            return View(assignments.ToList());
        }

        // GET: Assignments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = db.Assignments.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        // GET: Assignments/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "FirstName");
            ViewBag.HardwareID = new SelectList(db.Hardwares, "ID", "Model");
            ViewBag.SoftwareID = new SelectList(db.Softwares, "ID", "Name");
            return View();
        }

        // POST: Assignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,EmployeeID,HardwareID,SoftwareID,StatusID,Comment")] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                db.Assignments.Add(assignment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "FirstName", assignment.EmployeeID);
            ViewBag.HardwareID = new SelectList(db.Hardwares, "ID", "Model", assignment.HardwareID);
            ViewBag.SoftwareID = new SelectList(db.Softwares, "ID", "Name", assignment.SoftwareID);
            return View(assignment);
        }

        // GET: Assignments/Edit/5
        public ActionResult Edit(int? id)
        {
            AssignmentVM assignmentVM = new ViewModels.AssignmentVM();
             
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            assignmentVM.assignment = db.Assignments.Find(id);
            if (assignmentVM.assignment == null)
            {
                return HttpNotFound();
            }
            List<SelectListItem> items1 = new List<SelectListItem>();
            foreach (Employee e in db.Employees)
            {
                if (assignmentVM.assignment.EmployeeID == e.ID)
                {
                    items1.Add(new SelectListItem { Value = e.ID.ToString(), Text = e.FirstName, Selected = true});
                }
                else
                {
                    items1.Add(new SelectListItem { Value = e.ID.ToString(), Text = e.FirstName});
                }                
            }
            assignmentVM.employeeList=items1;

            List<SelectListItem> items2 = new List<SelectListItem>();
            foreach (Hardware h in db.Hardwares)
            {
                if (assignmentVM.assignment.HardwareID == h.ID)
                {
                    items2.Add(new SelectListItem { Value = h.ID.ToString(), Text = h.LabelName, Selected = true });
                }
                else
                {
                    items2.Add(new SelectListItem { Value = h.ID.ToString(), Text = h.LabelName });
                }
            }
            assignmentVM.computerList = items2;

            List<SelectListItem> items3 = new List<SelectListItem>();
            foreach (Software s in db.Softwares.Where(a=>a.Name.Contains("Office")))
            {
                if (assignmentVM.assignment.SoftwareID == s.ID)
                {
                    items3.Add(new SelectListItem { Value = s.ID.ToString(), Text = s.Name, Selected = true });
                }
                else
                {
                    items3.Add(new SelectListItem { Value = s.ID.ToString(), Text = s.Name });
                }
            }
            assignmentVM.msofficeList = items3;

            List<SelectListItem> items4 = new List<SelectListItem>();
            foreach (Software s in db.Softwares.Where(a => a.Name.Contains("Visio")))
            {
                if (assignmentVM.assignment.VisioID == s.ID)
                {
                    items4.Add(new SelectListItem { Value = s.ID.ToString(), Text = s.Name, Selected = true });
                }
                else
                {
                    items4.Add(new SelectListItem { Value = s.ID.ToString(), Text = s.Name });
                }
            }
            assignmentVM.msvisioLst = items4;

            //ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "FirstName", assignment.EmployeeID);
            //ViewBag.HardwareID = new SelectList(db.Hardwares, "ID", "Model", assignment.HardwareID);
            //ViewBag.SoftwareID = new SelectList(db.Softwares, "ID", "Name", assignment.SoftwareID);
            //ViewBag.VisioID = new SelectList(db.Softwares, "ID", "Name", assignment.VisioID);
            return View(assignmentVM);
        }

        // POST: Assignments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,EmployeeID,HardwareID,SoftwareID,VisioID,StatusID,Comment")] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assignment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "FirstName", assignment.EmployeeID);
            ViewBag.HardwareID = new SelectList(db.Hardwares, "ID", "Model", assignment.HardwareID);
            ViewBag.SoftwareID = new SelectList(db.Softwares, "ID", "Name", assignment.SoftwareID);
            ViewBag.VisioID = new SelectList(db.Softwares, "ID", "Name", assignment.VisioID);
            return View(assignment);
        }

        // GET: Assignments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = db.Assignments.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        // POST: Assignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Assignment assignment = db.Assignments.Find(id);
            db.Assignments.Remove(assignment);
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

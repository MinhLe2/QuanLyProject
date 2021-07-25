using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyProject.Models;

namespace QuanLyProject.Controllers
{
    public class AssignTasksController : Controller
    {
        private Minh_Model db = new Minh_Model();

        // GET: AssignTasks
        public ActionResult Index(string Search, string Date)
        {
            var assignTasks = from a in db.AssignTasks
                              join c in db.Clients
                              on a.ClientId equals c.ClientId
                              join e in db.Employees
                              on a.EmployeeId equals e.EmployeeId
                              join p in db.Projects
                              on a.ProjectId equals p.ProjectId
                              select a;

            var DateList = new List<String>() {"Đã Hoàn Thành","Đang thực hiện"};
            ViewBag.Date = new SelectList(DateList);

            if (!String.IsNullOrEmpty(Search))
                assignTasks = assignTasks.Where(a => a.Employee.EmployeeName.Contains(Search)
                || a.Client.ClientName.Contains(Search) || a.Project.ProjectName.Contains(Search));
            
            switch (Date)
            {
                case "Đã Hoàn Thành":
                    assignTasks = assignTasks.Where(p => p.Project.ProjectEnd < DateTime.Now || p.Project.ProjectEnd != null);
                    break;
                case "Đang thực hiện":
                    assignTasks = assignTasks.Where(p => p.Project.ProjectEnd > DateTime.Now || p.Project.ProjectEnd == null);
                    break;
            }
            //var assignTasks = db.AssignTasks.Include(a => a.Client).Include(a => a.Employee).Include(a => a.Project);
            return View(assignTasks.ToList());
        }

        // GET: AssignTasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignTask assignTask = db.AssignTasks.Find(id);
            if (assignTask == null)
            {
                return HttpNotFound();
            }
            return View(assignTask);
        }

        // GET: AssignTasks/Create
        public ActionResult Create()
        {
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ClientName");
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName");
            return View();
        }

        // POST: AssignTasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AssingnTaskId,EmployeeId,ClientId,ProjectId,Task,Note")] AssignTask assignTask)
        {
            if (ModelState.IsValid)
            {
                db.AssignTasks.Add(assignTask);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ClientName", assignTask.ClientId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", assignTask.EmployeeId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName", assignTask.ProjectId);
            return View(assignTask);
        }

        // GET: AssignTasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignTask assignTask = db.AssignTasks.Find(id);
            if (assignTask == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ClientName", assignTask.ClientId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", assignTask.EmployeeId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName", assignTask.ProjectId);
            return View(assignTask);
        }

        // POST: AssignTasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssingnTaskId,EmployeeId,ClientId,ProjectId,Task,Note")] AssignTask assignTask)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assignTask).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ClientName", assignTask.ClientId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", assignTask.EmployeeId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName", assignTask.ProjectId);
            return View(assignTask);
        }

        // GET: AssignTasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignTask assignTask = db.AssignTasks.Find(id);
            if (assignTask == null)
            {
                return HttpNotFound();
            }
            return View(assignTask);
        }

        // POST: AssignTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssignTask assignTask = db.AssignTasks.Find(id);
            db.AssignTasks.Remove(assignTask);
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

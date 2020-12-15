using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using task.Models;

namespace task.Controllers
{
    public class ProudectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Proudects
        [Authorize(Roles = "admin,client")]
        public ActionResult Index()
        {
            var proudects = db.proudects.Include(p => p.Category);
            return View(proudects.ToList());
        }

        // GET: Proudects/Details/5
        [Authorize(Roles = "admin,client")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proudect proudect = db.proudects.Find(id);
            if (proudect == null)
            {
                return HttpNotFound();
            }
            return View(proudect);
        }

        // GET: Proudects/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            return View();
        }

        // POST: Proudects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,Name,Price,Description,CategoryId,Proudctimage")] Proudect proudect, HttpPostedFileBase pic)
        {
            if (ModelState.IsValid)
            {
                if (pic == null) { return Content("null"); }
                string filename = Guid.NewGuid().ToString();

                string ext = pic.FileName.Split('.')[1];
                pic.SaveAs(Server.MapPath("~/categoriesimage/") + filename + "." + ext);
                proudect.Proudctimage = filename + "." + ext;
                db.proudects.Add(proudect);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", proudect.CategoryId);
            return View(proudect);
        }

        // GET: Proudects/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proudect proudect = db.proudects.Find(id);
            if (proudect == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", proudect.CategoryId);
            return View(proudect);
        }

        // POST: Proudects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,Name,Price,Description,CategoryId")] Proudect proudect)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proudect).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", proudect.CategoryId);
            return View(proudect);
        }

        // GET: Proudects/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proudect proudect = db.proudects.Find(id);
            if (proudect == null)
            {
                return HttpNotFound();
            }
            return View(proudect);
        }

        // POST: Proudects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Proudect proudect = db.proudects.Find(id);
            db.proudects.Remove(proudect);
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

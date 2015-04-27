using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyVideoTraining.Entities;
using MyVideoTraining.Models;

namespace MyVideoTraining.Controllers
{
    public class AssignmentTypesController : Controller
    {
        private MVTDataModel db = new MVTDataModel();

        // GET: AssignmentTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.AssignmentTypes.ToListAsync());
        }

        // GET: AssignmentTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignmentType assignmentType = await db.AssignmentTypes.FindAsync(id);
            if (assignmentType == null)
            {
                return HttpNotFound();
            }
            return View(assignmentType);
        }

        // GET: AssignmentTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AssignmentTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AssignmentTypeId,AssignmentTypeName")] AssignmentType assignmentType)
        {
            if (ModelState.IsValid)
            {
                db.AssignmentTypes.Add(assignmentType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(assignmentType);
        }

        // GET: AssignmentTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignmentType assignmentType = await db.AssignmentTypes.FindAsync(id);
            if (assignmentType == null)
            {
                return HttpNotFound();
            }
            return View(assignmentType);
        }

        // POST: AssignmentTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AssignmentTypeId,AssignmentTypeName")] AssignmentType assignmentType)
        {
            if (ModelState.IsValid)
            {
                var toEdit = db.AssignmentTypes.First(x => x.AssignmentTypeId == assignmentType.AssignmentTypeId);
                if (TryUpdateModel<AssignmentType>(toEdit))
                {
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return View(assignmentType);
        }

        // GET: AssignmentTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignmentType assignmentType = await db.AssignmentTypes.FindAsync(id);
            if (assignmentType == null)
            {
                return HttpNotFound();
            }
            return View(assignmentType);
        }

        // POST: AssignmentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AssignmentType assignmentType = await db.AssignmentTypes.FindAsync(id);
            db.AssignmentTypes.Remove(assignmentType);
            await db.SaveChangesAsync();
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

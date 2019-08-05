using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Event_Management.Context;
using Event_Management.Models;
using Microsoft.AspNet.Identity;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace Event_Management.Controllers
{
    public class BooksController : Controller
    {
        private EventManagementDbContaxt db = new EventManagementDbContaxt();

        // GET: Books
        [Authorize(Roles = "Admin,User")]
        public ActionResult Index()
        {
            var books = db.Books.Include(b => b.Hall);
            return View(books.ToList());
        }

        // GET: Books/Details/5
        [Authorize(Roles = "Admin,User")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        [Authorize(Roles = "Admin,User")]
        public ActionResult Create()
        {
            ViewBag.Email = User.Identity.GetUserName();
            ViewBag.HallId = new SelectList(db.Halls, "Id", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Email,Address,Mobile,DateTime,HallId")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HallId = new SelectList(db.Halls, "Id", "Name", book.HallId);
            return View(book);
        }

        // GET: Books/Edit/5
        [Authorize(Roles = "Admin,User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.HallId = new SelectList(db.Halls, "Id", "Name", book.HallId);
            return View(book);
        }
        [Authorize(Roles = "Admin,User")]
        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Address,Mobile,DateTime,HallId")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HallId = new SelectList(db.Halls, "Id", "Name", book.HallId);
            return View(book);
        }

        // GET: Books/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
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

	    public JsonResult IsDateExist(int? hallId, DateTime datetime)
	    {
            var model = db.Books.Where(x => (hallId.HasValue) ?
             (x.HallId == hallId && x.DateTime == datetime) :
             (x.DateTime == datetime)
         );

            return Json(!model.Any(),JsonRequestBehavior.AllowGet);
        }
        public ActionResult ExportReport()
{

    List<Book> allEverest = new List<Book>();
    using (EventManagementDbContaxt dc = new EventManagementDbContaxt())
    {
        allEverest = dc.Books.ToList();
    }
 
    ReportDocument rd = new ReportDocument();
    rd.Load(Path.Combine(Server.MapPath("~/CrystalReport"), "CrystalReport1.rpt"));
    rd.SetDataSource(allEverest);
 
    Response.Buffer = false;
    Response.ClearContent();
    Response.ClearHeaders();
    try
    {
        Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        stream.Seek(0, SeekOrigin.Begin);
        return File(stream, "application/pdf", "EverestList.pdf");
    }
    catch (Exception ex)
    {
        throw;
    }
}

    }
}

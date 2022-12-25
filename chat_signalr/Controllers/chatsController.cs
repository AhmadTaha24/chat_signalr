using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using chat_signalr.Models;

namespace chat_signalr.Controllers
{
    public class chatsController : Controller
    {
        private signalr_chatEntities db = new signalr_chatEntities();

        // GET: chats
        public ActionResult Index()
        {
            var chats = db.chats.Include(c => c.user);
            return View(chats.ToList());
        }

        // GET: chats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            chat chat = db.chats.Find(id);
            if (chat == null)
            {
                return HttpNotFound();
            }
            return View(chat);
        }

        // GET: chats/Create
        public ActionResult Create()
        {
            ViewBag.name = new SelectList(db.users, "name", "name");
            return View();
        }

        // POST: chats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,message,img_url,active_stat")] chat chat)
        {
            if (ModelState.IsValid)
            {
                db.chats.Add(chat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.name = new SelectList(db.chats, "name", chat.name);
            return View(chat);
        }

        // GET: chats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            chat chat = db.chats.Find(id);
            if (chat == null)
            {
                return HttpNotFound();
            }
            ViewBag.name = new SelectList(db.users, "name","name", chat.name);
            return View(chat);
        }

        // POST: chats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,message,img_url,active_stat")] chat chat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.name = new SelectList(db.users, "name", "name", chat.name);
            return View(chat);
        }

        // GET: chats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            chat chat = db.chats.Find(id);
            if (chat == null)
            {
                return HttpNotFound();
            }
            return View(chat);
        }

        // POST: chats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            chat chat = db.chats.Find(id);
            db.chats.Remove(chat);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using chat_signalr.Models;
using System.Dynamic;


namespace chat_signalr.Controllers
{
    public class HomeController : Controller
    {
        private signalr_chatEntities db = new signalr_chatEntities();
        

        public ActionResult Index()
        {
            dynamic mymodel = new ExpandoObject();
            mymodel.UserList = GetUser();
            mymodel.ChatList = GetChat();
            mymodel.InfoList = GetInfo();
            return View(mymodel);
        }
        public ActionResult Chat()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public List<user> GetUser()
        {
            signalr_chatEntities db = new signalr_chatEntities();
            List<user> Luser = db.users.ToList();
            return Luser;
        }
        public List<chat> GetChat()
        {
            signalr_chatEntities db = new signalr_chatEntities();
            List<chat> Lchat = db.chats.ToList();
            return Lchat;
        }
        public List<info> GetInfo()
        {
            signalr_chatEntities db = new signalr_chatEntities();
            List<info> Linfo= db.infoes.ToList();
            return Linfo;
        }
    }
}
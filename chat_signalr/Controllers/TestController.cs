using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using chat_signalr.Models;
using System.Dynamic;



namespace chat_signalr.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        private signalr_chatEntities db = new signalr_chatEntities();
        public ActionResult Index()
        {
            dynamic mymodel = new ExpandoObject();
            mymodel.UserList = GetUser();
            mymodel.ChatList = GetChat();
            mymodel.InfoList = GetInfo();
            return View(mymodel);
        }
        public ActionResult user2()
        {

            dynamic mymodel = new ExpandoObject();
            mymodel.UserList = GetUser();
            mymodel.ChatList = GetChat();
            mymodel.InfoList = GetInfo();
            return View(mymodel);
        }
        public ActionResult test(string receiver, string name)
        {
            dynamic mymodel = new ExpandoObject();
            mymodel.UserList = GetUser();
            mymodel.ChatList = GetChat();

            mymodel.CurrentChat = db.chats.Where(c => (c.name == name && c.receiver == receiver)|| (c.name == receiver && c.receiver == name));
            mymodel.InfoList = GetInfo();
            mymodel.name = receiver;
            //user name = db.users.Find(name);
            return View(mymodel);
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
            List<info> Linfo = db.infoes.ToList();
            return Linfo;
        }
    }
}
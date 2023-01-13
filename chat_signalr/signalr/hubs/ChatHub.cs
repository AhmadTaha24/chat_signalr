using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data.Entity;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using chat_signalr.Models;
using System.Security.Policy;
using System.Drawing;

namespace chat_signalr
{
    public class chathub : Hub
    {
        public async Task SendMessage(string fromUser, string message)
        {
            await Clients.All.SendAsync("RecieveMessage", fromUser, message);
        }
        public void Send(string name, string message)
        {
            signalr_chatEntities db = new signalr_chatEntities();
            chat ch = new chat();
            
            ch.name = name;
            ch.message = message;
            ch.sent_time = DateTime.Now;
            ch.img_url =  db.users.Where(c => c.name == name).Select(c => c.img_url).First();
            var user = db.users.ToList();
            //ch.id_user = 1;
            
            
            db.chats.Add(ch);
            Clients.All.addNewMessageToPage(name, message);
            
            db.SaveChanges();

        }
        public void SendChatMessage(string who, string message)
        {
            var cookie = Context.Request.Cookies["UserInfo"];
            //string name = Context.User.Identity.Name;
            string name = cookie.Value;

            Clients.Group(who).addChatMessage(name , message);
        }

        public void Ssend(string n, string who, string message)
        {
            signalr_chatEntities db = new signalr_chatEntities();
            chat ch = new chat();


            var user =db.users.Where(m => m.name == n).FirstOrDefault();
            string test = Context.ConnectionId;
            user.con_id = test;
            ch.name = n;
            ch.message = message;
            ch.receiver = who;
            ch.sent_time = DateTime.Now;
            ch.img_url = db.users.Where(c => c.name == n).Select(c => c.img_url).First();
            db.chats.Add(ch);
            db.SaveChanges();

            string who_con = db.users.Where(m => m.name == who).FirstOrDefault().con_id ;
            string url = db.users.Where(m => m.name == n).FirstOrDefault().img_url;

            Clients.Caller.myMessage(n, message, url);
            Clients.Client(who_con).addChatMessage(n ,  message, url);
         
            
            // Clients.All.addChatMessage(name, message);
        }

        public override Task OnConnected()
        {
            var cookie = Context.Request.Cookies["UserInfo"];
            //string name = Context.User.Identity.Name;
            string name = cookie.Value;
            var x = Context.ConnectionId;
            Groups.Add(Context.ConnectionId, name);

            return base.OnConnected();
        }

    }
}
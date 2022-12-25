using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    }
}
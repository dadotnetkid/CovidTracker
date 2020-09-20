using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Models;
using Models.Repository;

namespace Web.Hubs
{
    [HubName("NotifierHub")]
    public class NotifierHub : Hub
    {
        //public void NotifySmsServer(string contactNumber, string message)
        //{
        //    Clients.All.Notify(new Messages{ ContactNumber = contactNumber, Message = message });
        //}


        public void GetAllSMS()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            Clients.All.GetAll(unitOfWork.MessagesRepo.Get(m => m.IsSent == false));
        }

        public void UpdateSms(Messages messages)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            var res = unitOfWork.MessagesRepo.Find(x => x.Id == messages.Id);
            res.IsSent = true;
            unitOfWork.Save();
        }
    }
}
using BLL_IDEA_X.Helper_Classes;
using DAL_IDEA_X;
using DAL_IDEA_X.EntityFramework;
using EntityLayer_IDEA_X.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_IDEA_X.Services
{
    public class MessageRequestService
    {
        public static MessageRequestModel SendChatRequest(MessageRequestModel model)
        {

            if (model.SENDER != null && model.RECEIVER != null)
            {
                MESSAGE_REQUESTS request = new MESSAGE_REQUESTS()
                {
                    MESSAGE_TIME = IDEAX_Functions.time(),
                    SENDER = model.SENDER,
                    RECEIVER = model.RECEIVER
                };

                var val =  DataAcessFactory.MessageRequestDataAccess().Create(request);
                model.MESSAGE_TIME = request.MESSAGE_TIME;
                if (val) return model;
                return null;
            }

            return null;
        }
        public static MessageRequestModel GetChatRequest(string uname,string mname)
        {
            var data = DataAcessFactory.MessageRequestDataAccess().Get();
            var urec = data.Where(val => val.SENDER.Equals(uname) &&
            val.RECEIVER.Equals(mname)).SingleOrDefault();
            if(urec != null)
            {
                MessageRequestModel model = new MessageRequestModel()
                {
                    MESSAGE_TIME = urec.MESSAGE_TIME,
                    RECEIVER = urec.RECEIVER,
                    SENDER = urec.SENDER,
                    REQUEST_ID = urec.REQUEST_ID,
                };
                return model;
            }
            return null;

        }
        public static bool AcceptChatRequest(MessageRequestModel model)
        {
            var urec = GetChatRequest(model.RECEIVER, model.SENDER);
            ChatBoxModel chab = new ChatBoxModel();
            var res = false;
            if (urec != null)
            {

                res = DeleteChatRequest(urec);
                string session = model.SENDER + "_TO_" + model.RECEIVER;
                chab.CHAT_SESSION = session;
                chab.SENDER = model.SENDER;
                chab.RECEIVER = model.RECEIVER;
                chab.CHAT_TIME = IDEAX_Functions.time();
                res =  ChatBoxService.AddChatSession(chab);
                return res;
            }
            return res;
        }

        public static bool DeleteChatRequest(MessageRequestModel model)
        {
            var chatRequest = GetChatRequest(model.RECEIVER, model.SENDER);
            if(chatRequest != null)
            {
                MESSAGE_REQUESTS request = new MESSAGE_REQUESTS()
                {
                    MESSAGE_TIME = chatRequest.MESSAGE_TIME,
                    RECEIVER = chatRequest.RECEIVER,
                    REQUEST_ID = chatRequest.REQUEST_ID,
                    SENDER = chatRequest.SENDER

                };

                return DataAcessFactory.MessageRequestDataAccess().Delete(request);
            }
            return false;
        }

        public static bool CancelChatRequest(MessageRequestModel model)
        {
            var chatRequest = GetChatRequest(model.SENDER, model.RECEIVER);
            if (chatRequest != null)
            {
                MESSAGE_REQUESTS request = new MESSAGE_REQUESTS()
                {
                    MESSAGE_TIME = chatRequest.MESSAGE_TIME,
                    RECEIVER = chatRequest.RECEIVER,
                    REQUEST_ID = chatRequest.REQUEST_ID,
                    SENDER = chatRequest.SENDER

                };

                return DataAcessFactory.MessageRequestDataAccess().Delete(request);
            }
            return false;
        }

        public static string CheckChatRequest(string uname,string mname)
        {
            var urec = GetChatRequest(mname, uname);
            string msg = null;
            if (urec != null)
            {
                 msg = "WAITING_ACCEPT";

               
            }
            else
            {
                var usender = GetChatRequest(uname, mname);
                if (usender != null)
                {
                    msg = "ALREADY_SEND";
                }
            }
            return msg;
        }

        public static List<MessageRequestModel> ReqUserList(string uname)
        {
            var data = DataAcessFactory.MessageRequestDataAccess().Get();
            var lst = data.Where(val => val.RECEIVER.Equals(uname)).ToList();
            List<MessageRequestModel> msgReqBox = new List<MessageRequestModel>();
            foreach (var chatRequest in lst)
            {
                msgReqBox.Add(new MessageRequestModel()
                {
                    MESSAGE_TIME = chatRequest.MESSAGE_TIME,
                    RECEIVER = chatRequest.RECEIVER,
                    REQUEST_ID = chatRequest.REQUEST_ID,
                    SENDER = chatRequest.SENDER
                });
            }
            return msgReqBox;
        }

        public static List<MessageRequestModel> SendReqList(string uname)
        {
            var data = DataAcessFactory.MessageRequestDataAccess().Get();
            var lst = data.Where(val => val.SENDER.Equals(uname) && !val.RECEIVER.Equals(uname)).ToList();
            List<MessageRequestModel> msgReqBox = new List<MessageRequestModel>();
            foreach (var chatRequest in lst)
            {
                msgReqBox.Add(new MessageRequestModel()
                {
                    MESSAGE_TIME = chatRequest.MESSAGE_TIME,
                    RECEIVER = chatRequest.RECEIVER,
                    REQUEST_ID = chatRequest.REQUEST_ID,
                    SENDER = chatRequest.SENDER
                });
            }
            return msgReqBox;
        }

        public static List<MessageRequestModel> GetAllReqList()
        {
            var data = DataAcessFactory.MessageRequestDataAccess().Get();
            var lst = (from u_d in data
                       orderby u_d.REQUEST_ID descending
                       select u_d).ToList();
            List<MessageRequestModel> msgReqBox = new List<MessageRequestModel>();
            foreach (var chatRequest in lst)
            {
                msgReqBox.Add(new MessageRequestModel()
                {
                    MESSAGE_TIME = chatRequest.MESSAGE_TIME,
                    RECEIVER = chatRequest.RECEIVER,
                    REQUEST_ID = chatRequest.REQUEST_ID,
                    SENDER = chatRequest.SENDER
                });
            }
            return msgReqBox;
        }

        public static List<MessageRequestModel> SearchMessageRequests(string text)
        {
            var data = GetAllReqList();
            var lst = (from u_d in data
                       where u_d.REQUEST_ID.ToString().Contains(text)
                       || u_d.SENDER.Contains(text)
                       || u_d.RECEIVER.Contains(text)
                       || u_d.MESSAGE_TIME.Contains(text)
                       select u_d).ToList();
            List<MessageRequestModel> msgReqBox = new List<MessageRequestModel>();
            foreach (var chatRequest in lst)
            {
                msgReqBox.Add(new MessageRequestModel()
                {
                    MESSAGE_TIME = chatRequest.MESSAGE_TIME,
                    RECEIVER = chatRequest.RECEIVER,
                    REQUEST_ID = chatRequest.REQUEST_ID,
                    SENDER = chatRequest.SENDER
                });
            }
            return msgReqBox;
        }
    }
}

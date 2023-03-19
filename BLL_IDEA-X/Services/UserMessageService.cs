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
    public class UserMessageService
    {
        //Read -- niloy
        public static List<UserMessageModel> GetChatList(string sess,string uname,string mname)
        {
            if(sess != null)
            {
                var data = DataAcessFactory.UserMessageDataAccess().Get();
                var lst = data.Where(m => m.SESSION_NAME.Equals(sess)).ToList();
                var chat_new = ChatBoxService.GetChatMate(uname, mname);
                List<UserMessageModel> output = new List<UserMessageModel>();
                foreach (var item in lst)
                {
                    output.Add(new UserMessageModel()
                    {
                        MESSAGE_ID = item.MESSAGE_ID,
                        MESSAGE_TIME = item.MESSAGE_TIME,
                        RECEIVER = item.RECEIVER,
                        SENDER = item.SENDER,
                        SESSION_NAME = item.SESSION_NAME,
                        USER_MESSAGE = item.USER_MESSAGE
                    });
                }
                List<UserMessageModel> o = output.Select(val => {
                    val.USER_MESSAGE = EncryptionAndHashLogic.DecryptMsg(
                     val.USER_MESSAGE, val.SESSION_NAME, chat_new.CHAT_TIME);
                    return val;
                     }).ToList();
                return o;
            }
            return null;
        }

        //Delete --niloy
        public static bool RemoveUserMessage(UserMessageModel model)
        {
            if(model != null && model.MESSAGE_ID != 0)
            {
                var msg = DataAcessFactory.UserMessageDataAccess().Get(model.MESSAGE_ID);
                if(msg != null)
                {
                    
                    return DataAcessFactory.UserMessageDataAccess().Delete(msg);
                }
                return false;
            }
            return false;
        }

        public static bool RemoveChat(UserMessageModel model)
        {
            var chat = ChatBoxService.GetChatMate(model.SENDER, model.RECEIVER);
            if(chat != null)
            {
                var chatlist = GetChatList(chat.CHAT_SESSION, chat.SENDER, chat.RECEIVER);
                if(chatlist != null)
                {
                    var res = false;
                    foreach (var item in chatlist)
                    {
                        UserMessageModel m = new UserMessageModel()
                        {
                            MESSAGE_ID = item.MESSAGE_ID,
                            MESSAGE_TIME = item.MESSAGE_TIME,
                            RECEIVER = item.RECEIVER,
                            SENDER = item.SENDER,
                            SESSION_NAME = item.SESSION_NAME,
                            USER_MESSAGE = item.USER_MESSAGE
                        };
                        res = RemoveUserMessage(m);
                    }
                    return res;
                }
                return false;
            }

            return false;
        }
        //Create -- niloy
        public static List<UserChatMessageModel> SendChatMessage(UserMessageModel msg)
        {
            if(msg != null)
            {
                var output = false;
                var chat = ChatBoxService.GetChatMate(msg.SENDER, msg.RECEIVER);
               if(chat != null)
                {
                   
                    var user_msg = new USER_MESSAGES
                    {
                        SENDER = msg.SENDER,
                        RECEIVER = msg.RECEIVER,
                        
                        SESSION_NAME = chat.CHAT_SESSION,
                        MESSAGE_TIME = IDEAX_Functions.time(),
                    };
                    user_msg.USER_MESSAGE = EncryptionAndHashLogic.EncryptMsg(msg.USER_MESSAGE.Trim(),
                  chat.CHAT_SESSION, chat.CHAT_TIME);
                    output = DataAcessFactory.UserMessageDataAccess().Create(user_msg);
                    var chat_new = ChatBoxService.GetChatMate(msg.SENDER, msg.RECEIVER);
                    var msg_list = GetChatList(chat_new.CHAT_SESSION,msg.SENDER,msg.RECEIVER);
                    var u_details = UserDetailsService.GetUserDetails();
                    var chatlist = (from m in msg_list
                                    where m.SESSION_NAME.Equals(chat_new.CHAT_SESSION)
                                    && m.MESSAGE_TIME.Equals(user_msg.MESSAGE_TIME)
                                    join u in u_details
                                    on m.SENDER equals u.USERNAME into cl
                                    from c in cl.DefaultIfEmpty()
                                    select new UserChatMessageModel
                                    {
                                        MESSAGE_ID = m.MESSAGE_ID,
                                        MESSAGE_TIME = m.MESSAGE_TIME,
                                        PROFILE_IMG = c.PROFILE_PICTURE,
                                        RECEIVER = m.RECEIVER,
                                        SENDER = m.SENDER,
                                        SESSION_NAME = m.SESSION_NAME,
                                        USER_MESSAGE = m.USER_MESSAGE

                                    }
                                   ).ToList();
                   
                    return chatlist;

                }

                return null;
              
            }
            return null;
        }

        //Update -- niloy
        public static bool EditChatMessage(UserMessageModel model)
        {
            if(model != null && model.MESSAGE_ID != 0)
            {
                var msg_model = DataAcessFactory.UserMessageDataAccess().Get(model.MESSAGE_ID);
                var chatmate = ChatBoxService.GetChatMate(model.SENDER, model.RECEIVER);
                if (msg_model != null)
                {
                    USER_MESSAGES msg = new USER_MESSAGES()
                    {
                        MESSAGE_ID = msg_model.MESSAGE_ID,
                        MESSAGE_TIME = msg_model.MESSAGE_TIME,
                        RECEIVER = msg_model.RECEIVER,
                        SENDER = msg_model.SENDER,
                        SESSION_NAME = msg_model.SESSION_NAME,
                        USER_MESSAGE = EncryptionAndHashLogic.EncryptMsg(model.USER_MESSAGE,
                            model.SESSION_NAME, chatmate.CHAT_TIME)
                    };
                    return DataAcessFactory.UserMessageDataAccess().Update(msg);
                }
                return false;
            }

            
            return false;
            
        }
    }
}

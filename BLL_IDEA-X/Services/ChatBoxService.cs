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
    public class ChatBoxService
    {
        //Read -- niloy
        public static ChatBoxModel GetChatMate(string uname,string mname)
        {
            var data = DataAcessFactory.ChatBoxDataAccess().Get();
            var user = data.Where(val => (val.SENDER.Equals(uname) 
            && val.RECEIVER.Equals(mname)) || 
            (val.SENDER.Equals(mname) 
            && val.RECEIVER.Equals(uname))).SingleOrDefault();
            ChatBoxModel model = new ChatBoxModel();
            return MappingService.MapClass(user, model);
        }

        public static ChatBoxModel GetEmptyChatResult()
        {
            ChatBoxModel chatBoxModel = new ChatBoxModel();
            return chatBoxModel;
        }
        //Create -- niloy
        public static bool AddChatSession(ChatBoxModel chatBox)
        {
            if(chatBox != null)
            {
                CHAT_BOXS chatsess = new CHAT_BOXS()
                {
                    CHAT_SESSION = $"{chatBox.SENDER}_TO_{chatBox.RECEIVER}",
                    CHAT_TIME = IDEAX_Functions.time(),
                    SENDER = chatBox.SENDER,
                    RECEIVER = chatBox.RECEIVER
                };

                return DataAcessFactory.ChatBoxDataAccess().Create(chatsess);
            }
            return false;
        }

        //Delete -- niloy
        public static bool RemoveChatMate(ChatBoxModel chatBox)
        {
            if(chatBox != null)
            {
                var chat = GetChatMate(chatBox.SENDER, chatBox.RECEIVER);
               if(chat != null)
                {
                    var chatlist = UserMessageService.GetChatList(chatBox.CHAT_SESSION,
                        chatBox.SENDER,chatBox.RECEIVER);
                    if (chatlist != null)
                    {
                        foreach (var x in chatlist)
                        {
                            UserMessageService.RemoveUserMessage(x);

                        }
                    }
                    CHAT_BOXS chat_d = new CHAT_BOXS()
                    {
                        CHAT_SESSION = chat.CHAT_SESSION,
                        CHAT_TIME = chat.CHAT_TIME,
                        RECEIVER = chat.RECEIVER,
                        SENDER = chat.SENDER,
                    };
                    return DataAcessFactory.ChatBoxDataAccess().Delete(chat_d);
                }
            }
            return false;
        }

        //Update -- niloy
        public static bool UpdateChatSession(ChatBoxModel chat)
        {
            if(chat != null)
            {
                CHAT_BOXS chat_u = new CHAT_BOXS()
                {
                    CHAT_SESSION = chat.CHAT_SESSION,
                    CHAT_TIME = chat.CHAT_TIME,
                    RECEIVER = chat.RECEIVER,
                    SENDER = chat.SENDER,
                };
                if(chat_u.CHAT_TIME == null)
                {
                    chat_u.CHAT_TIME = IDEAX_Functions.time();
                }
                return DataAcessFactory.ChatBoxDataAccess().Update(chat_u);
            }
            return false;
        }


        public static List<ChatBoxModel> RecUserList(string uname)
        {
            var data = DataAcessFactory.ChatBoxDataAccess().Get();
            var lst = data.Where(val => val.SENDER.Equals(uname) && !val.RECEIVER.Equals(uname)).ToList();
            List<ChatBoxModel> chatBoxes = new List<ChatBoxModel>();
            foreach (var chat in lst)
            {
                chatBoxes.Add(new ChatBoxModel()
                {
                    CHAT_SESSION = chat.CHAT_SESSION,
                    CHAT_TIME = chat.CHAT_TIME,
                    RECEIVER = chat.RECEIVER,
                    SENDER = chat.SENDER,
                });
            }
            return chatBoxes;
        }

        public static List<ChatBoxModel> SendUserList(string uname)
        {
            var data = DataAcessFactory.ChatBoxDataAccess().Get();
            var lst = data.Where(val => val.RECEIVER.Equals(uname) && !val.SENDER.Equals(uname)).ToList();
            List<ChatBoxModel> chatBoxes = new List<ChatBoxModel>();
            foreach (var chat in lst)
            {
                chatBoxes.Add(new ChatBoxModel()
                {
                    CHAT_SESSION = chat.CHAT_SESSION,
                    CHAT_TIME = chat.CHAT_TIME,
                    RECEIVER = chat.RECEIVER,
                    SENDER = chat.SENDER,
                });
            }
            return chatBoxes;
        }
        public static List<ChatBoxModel> BothUsersList(string uname)
        {
            var data = DataAcessFactory.ChatBoxDataAccess().Get();
            var lst = data.Where(val => val.RECEIVER.Equals(uname) && val.SENDER.Equals(uname)).ToList();
            List<ChatBoxModel> chatBoxes = new List<ChatBoxModel>();
            foreach (var chat in lst)
            {
                chatBoxes.Add(new ChatBoxModel()
                {
                    CHAT_SESSION = chat.CHAT_SESSION,
                    CHAT_TIME = chat.CHAT_TIME,
                    RECEIVER = chat.RECEIVER,
                    SENDER = chat.SENDER,
                });
            }
            return chatBoxes;
        }

        public static List<ChatBoxModel> ChatSessions()
        {
            var data = DataAcessFactory.ChatBoxDataAccess().Get();
            var lst = (from u_d in data
                       orderby u_d.CHAT_SESSION ascending
                       select u_d).ToList();
            List<ChatBoxModel> chatBoxes = new List<ChatBoxModel>();
            foreach (var chat in lst)
            {
                chatBoxes.Add(new ChatBoxModel()
                {
                    CHAT_SESSION = chat.CHAT_SESSION,
                    CHAT_TIME = chat.CHAT_TIME,
                    RECEIVER = chat.RECEIVER,
                    SENDER = chat.SENDER,
                });
            }
            return chatBoxes;
        }

        public static List<ChatBoxModel> SearchChatSessions(string text)
        {
            var data = ChatSessions();
            var lst = (from u_d in data
                       where u_d.CHAT_SESSION.Contains(text)
                       || u_d.SENDER.Contains(text)
                       || u_d.RECEIVER.Contains(text)
                       || u_d.CHAT_TIME.Contains(text)
                       
                       select u_d).ToList();
            List<ChatBoxModel> chatBoxes = new List<ChatBoxModel>();
            foreach (var chat in lst)
            {
                chatBoxes.Add(new ChatBoxModel()
                {
                    CHAT_SESSION = chat.CHAT_SESSION,
                    CHAT_TIME = chat.CHAT_TIME,
                    RECEIVER = chat.RECEIVER,
                    SENDER = chat.SENDER,
                });
            }
            return chatBoxes;
        }


    }

}

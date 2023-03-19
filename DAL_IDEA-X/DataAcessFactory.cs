using DAL_IDEA_X.EntityFramework;
using DAL_IDEA_X.Interface;
using DAL_IDEA_X.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_IDEA_X
{
    public class DataAcessFactory
    {
        static IDEA_XEntities db = new IDEA_XEntities();
        public static IRepo<ADMIN,int,string> AdminDataAccess()
        {
            return new AdminRepo(db);
        }
        public static IRepo<ALL_USERS,int,string> AllUsersDataAccess()
        {
            return new AllUsersRepo(db);
        }
        public static IRepo<CHAT_BOXS,int,string> ChatBoxDataAccess()
        {
            return new ChatBoxRepo(db);
        }
        public static IRepo<CONTACT,int,string> ContactDataAccess()
        {
            return new ContactRepo(db);
        }
        public static IRepo<GENERAL_POSTS,int,string> GeneralPostDataAccess()
        {
            return new GeneralPostRepo(db);
        }
        public static IRepo<LOGIN,int,string> LoginDataAccess()
        {
            return new LoginRepo(db);
        }
        public static IRepo<MESSAGE_REQUESTS,int,string> MessageRequestDataAccess()
        {
            return new MessageRequestRepo(db);
        }
        public static IRepo<NOTE,int,string> NotesDataAccess()
        {
            return new NotesRepo(db);
        }
        public static IRepo<POST_ACTIONS,int,string> PostActionDataAcess()
        {
            return new PostActionsRepo(db);
        }
        public static IRepo<POST_REPORT,int,string> PostRepoDataAccess()
        {
            return new PostReportRepo(db);
        }
        public static IRepo<USER_ACCESS_CONTROLLER,int,string> UACDataAccess()
        {
            return new UserAcessControllerRepo(db);
        }
        public static IRepo<USER_DETAILS,int,string> UserDetailsDataAccess()
        {
            return new UserDetailsRepo(db);
        }
        public static IRepo<USER_MESSAGES,int,string> UserMessageDataAccess()
        {
            return new UserMessageRepo(db);
        }
        public static IAuth AuthDataAcess()
        {
            return new LoginRepo(db);
        }
        
    }
}

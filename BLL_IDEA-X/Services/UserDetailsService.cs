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
    public class UserDetailsService
    {

        public static List<UserDetailsModel> GetOldMateList(string uname)
        {
            var rec_user = ChatBoxService.RecUserList(uname);
            var send_user = ChatBoxService.SendUserList(uname);
            var both_user = ChatBoxService.BothUsersList(uname);

            var req_user = MessageRequestService.ReqUserList(uname);
            var send_req = MessageRequestService.SendReqList(uname); 
            List<UserDetailsModel> user = new List<UserDetailsModel>();

            foreach (var u in rec_user)
            {
                var u_d = GetUserDetails(u.RECEIVER);
                user.Add(u_d);
            }
            foreach (var u in send_user)
            {
                var u_d = GetUserDetails(u.SENDER);
                user.Add(u_d);
            }
            foreach (var u in both_user)
            {
                var u_d = GetUserDetails(u.SENDER);
                user.Add(u_d);
            }
            foreach (var u in req_user)
            {
                var u_d = GetUserDetails(u.SENDER);
                user.Add(u_d);
            }
            foreach (var u in send_req)
            {
                var u_d = GetUserDetails(u.RECEIVER);
                user.Add(u_d);
            }

            return user;
        }


        //Create --anindra
        public static bool AddUserDetails(UserModel details)
        {
            UserDetailsModel udetail = new UserDetailsModel()
            {
                USERNAME = details.USERNAME,
                FIRST_NAME = details.FIRST_NAME,
                LAST_NAME = details.LAST_NAME,
                HEADLINE = details.HEADLINE,
                DATE_OF_BIRTH = details.DATE_OF_BIRTH,
                GENDER = details.GENDER,
                MOBILE = details.MOBILE,
                USER_ADDRESS = "N/A",
                USER_STATE = "N/A",
                ZIP_CODE = "N/A",
                COUNTRY = details.COUNTRY,
                INDUSTRY = details.INDUSTRY,
                EDUCATIONAL_INSTITUTION = details.EDUCATIONAL_INSTITUTION,
                DEPARTMENT = details.DEPARTMENT,
                CONTACT_URL = "N/A",
                SIGNUP_TIME = IDEAX_Functions.time(),
                USER_STATUS = "ACTIVE",
                SIGNUP_IP = IDEAX_Functions.ip(),
                PROFILE_PICTURE = details.PROFILE_IMG,
            };
            USER_DETAILS dest = new USER_DETAILS();
            USER_DETAILS output = MappingService.MapClass(udetail, dest);
            return DataAcessFactory.UserDetailsDataAccess().Create(output);
        }
        //Read -- anindra
        public static UserDetailsModel GetUserDetails(string uname)
        {
            var data = DataAcessFactory.UserDetailsDataAccess().Get(uname);
            UserDetailsModel dest = new UserDetailsModel();
            var output = MappingService.MapClass(data, dest);
            return output;
        } 

        public static List<UserDetailsModel> SearchProfile(string searchText)
        {
            var data = DataAcessFactory.UserDetailsDataAccess().Get();
            var ulist = (data.Where(val => val.USERNAME.Trim().ToLower().Contains(searchText.ToLower()) 
            || val.FIRST_NAME.Trim().ToLower().Contains(searchText.ToLower()) 
            || val.LAST_NAME.Trim().ToLower().Contains(searchText.ToLower()) 
            || (val.FIRST_NAME + " " + val.LAST_NAME).Trim().ToLower().Contains(searchText.ToLower()))
            .ToList());
            List<UserDetailsModel> userDetails = new List<UserDetailsModel>();
            foreach (var item in ulist)
            {
                UserDetailsModel dest = new UserDetailsModel();
                userDetails.Add(

                MappingService.MapClass(item, dest));
                
            }
            return userDetails;
        }
        public static List<UserDetailsModel> SearchAllUserDetails(string text)
        {
            var data = GetUserDetails();
            var users = (from u_d in data
                         where u_d.USERNAME.Contains(text)
                         || (u_d.FIRST_NAME + " " + u_d.LAST_NAME).Contains(text)
                         || u_d.DATE_OF_BIRTH.Contains(text)
                         || u_d.GENDER.Contains(text)
                         || u_d.MOBILE.Contains(text)
                         || u_d.COUNTRY.Contains(text)
                         || u_d.INDUSTRY.Contains(text)
                         || u_d.EDUCATIONAL_INSTITUTION.Contains(text)
                         || u_d.DEPARTMENT.Contains(text)
                         || u_d.SIGNUP_TIME.Contains(text)
                          || u_d.USER_STATUS.Contains(text)
                           || u_d.SIGNUP_IP.Contains(text)
                         select u_d).ToList();
            List<UserDetailsModel> userDetails = new List<UserDetailsModel>();
            foreach (var item in users)
            {
                UserDetailsModel dest = new UserDetailsModel();
                userDetails.Add(

                MappingService.MapClass(item, dest));

            }
            return userDetails;
        }
        public static List<UserDetailsModel> UserSearch(string searchText)
        {
            var data = DataAcessFactory.UserDetailsDataAccess().Get();
            var ulist = (data.Where(x => x.USERNAME.Contains(searchText))
            .ToList());
            List<UserDetailsModel> userDetails = new List<UserDetailsModel>();
            foreach (var item in ulist)
            {
                UserDetailsModel dest = new UserDetailsModel();
                userDetails.Add(

                MappingService.MapClass(item, dest));

            }
            return userDetails;
        }

        //Delete -- anindra
        public static bool RemoveUserDetails(string uname)
        {
            if(uname != null) return DataAcessFactory.UserDetailsDataAccess().Delete(uname);
            return false;
        } 
        //Update -- anindra
        public static bool UpdateUserDetails(UserModel detailsModel)
        {

            USER_DETAILS details = new USER_DETAILS();
            details.USERNAME = detailsModel.USERNAME;
            if (detailsModel.FIRST_NAME != null)
            {
                details.FIRST_NAME = detailsModel.FIRST_NAME;
            }
            if (detailsModel.LAST_NAME != null)
            {
                details.LAST_NAME = detailsModel.LAST_NAME;
            }
            if (detailsModel.HEADLINE != null)
            {
                details.HEADLINE = detailsModel.HEADLINE;
            }
            if (detailsModel.DATE_OF_BIRTH != null)
            {
                details.DATE_OF_BIRTH = detailsModel.DATE_OF_BIRTH;
            }
            if (detailsModel.GENDER != null)
            {
                details.GENDER = detailsModel.GENDER;
            }
            if (detailsModel.MOBILE != null)
            {
                details.MOBILE = detailsModel.MOBILE;
            }
            if (detailsModel.COUNTRY != null)
            {
                details.COUNTRY = detailsModel.COUNTRY;
            }
            if (detailsModel.INDUSTRY != null)
            {
                details.INDUSTRY = detailsModel.INDUSTRY;
            }
            if (detailsModel.EDUCATIONAL_INSTITUTION != null)
            {
                details.EDUCATIONAL_INSTITUTION = detailsModel.EDUCATIONAL_INSTITUTION;
            }
            if (detailsModel.DEPARTMENT != null)
            {
                details.DEPARTMENT = detailsModel.DEPARTMENT;
            }
            

            if (detailsModel.PROFILE_IMG != null)
            {
                //updateUser.PROFILE_IMG =  IDEAX_Functions.imageToByte(updateUser.PROFILE_PICTURE);
                details.PROFILE_PICTURE = detailsModel.PROFILE_IMG;
            }
            return DataAcessFactory.UserDetailsDataAccess().Update(details);

        }
        //Read -- Anindra
        public static List<UserDetailsModel> GetUserDetails()
        {
            var data = DataAcessFactory.UserDetailsDataAccess().Get();
            var users = (from u_d in data
                         orderby u_d.USERNAME ascending
                         select u_d).ToList();
            List<UserDetailsModel> output = new List<UserDetailsModel>();
            foreach (var item in users)
            {
                output.Add(new UserDetailsModel()
                {
                    USERNAME = item.USERNAME,
                    FIRST_NAME = item.FIRST_NAME,
                    LAST_NAME = item.LAST_NAME,
                    HEADLINE = item.HEADLINE,
                    DATE_OF_BIRTH = item.DATE_OF_BIRTH,
                    GENDER = item.GENDER,
                    MOBILE = item.MOBILE,
                    USER_ADDRESS = item.USER_ADDRESS,
                    USER_STATE = item.USER_STATE,
                    ZIP_CODE = item.ZIP_CODE,
                    COUNTRY = item.COUNTRY,
                    INDUSTRY = item.INDUSTRY,
                    EDUCATIONAL_INSTITUTION = item.EDUCATIONAL_INSTITUTION,
                    DEPARTMENT = item.DEPARTMENT,
                    CONTACT_URL = item.CONTACT_URL,
                    SIGNUP_TIME = item.SIGNUP_TIME,
                    USER_STATUS = item.USER_STATUS,
                    SIGNUP_IP = item.SIGNUP_IP,
                    PROFILE_PICTURE = item.PROFILE_PICTURE,
                });
            }

            return output;
        }

    }
}

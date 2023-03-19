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
    public class ContactService
    {
        //Read -- kawshik
        public static List<ContactModel> GetContacts()
        {
            List<ContactModel> contacts = new List<ContactModel>();
            var data = DataAcessFactory.ContactDataAccess().Get();
            foreach (var item in data)
            {
                ContactModel model = new ContactModel();
                var output = MappingService.MapClass(item, model);
                contacts.Add(output);
            }
            return contacts;
        }


        public static List<ContactModel> PendingContact()
        {
            List<ContactModel> contacts = new List<ContactModel>();
            var data = GetContacts();
            var reports = (from u_d in data
                           where u_d.STATUS.Equals("PENDING")
                           orderby u_d.CONTACT_ID ascending
                           select u_d).ToList();
            foreach (var item in reports)
            {
                ContactModel model = new ContactModel();
                var output = MappingService.MapClass(item, model);
                contacts.Add(output);
            }
            return contacts;
        }
        public static List<ContactModel> SolvedContact()
        {
            List<ContactModel> contacts = new List<ContactModel>();
            var data = GetContacts();
            var reports = (from u_d in data
                           where u_d.STATUS.Equals("SOLVED")
                           orderby u_d.CONTACT_ID ascending
                           select u_d).ToList();
            foreach (var item in reports)
            {
                ContactModel model = new ContactModel();
                var output = MappingService.MapClass(item, model);
                contacts.Add(output);
            }
            return contacts;
        }

        public static List<ContactModel> SearchPendingContacts(String text)
        {
            List<ContactModel> contacts = new List<ContactModel>();
            var data = PendingContact();
            var reports = (from u_d in data
                           where u_d.CONTACT_ID.ToString().Contains(text) ||
                             u_d.FIRST_NAME.ToString().Contains(text) ||
                             u_d.LAST_NAME.Contains(text) ||
                             u_d.USERNAME.Contains(text) ||
                              u_d.EMAIL.Contains(text) ||
                             u_d.MESSAGE.Contains(text) ||
                             u_d.LEVEL.Contains(text) ||
                             u_d.STATUS.Contains(text) ||
                             u_d.LOGIN_TIME.Contains(text) ||
                             u_d.LOGIN_IP.Contains(text)
                           select u_d).ToList();
            foreach (var item in reports)
            {
                ContactModel model = new ContactModel();
                var output = MappingService.MapClass(item, model);
                contacts.Add(output);
            }
            return contacts;
        }

        public static List<ContactModel> SearchSolvedContacts(String text)
        {
            List<ContactModel> contacts = new List<ContactModel>();
            var data = SolvedContact();
            var reports = (from u_d in data
                           where u_d.CONTACT_ID.ToString().Contains(text) ||
                             u_d.FIRST_NAME.ToString().Contains(text) ||
                             u_d.LAST_NAME.Contains(text) ||
                             u_d.USERNAME.Contains(text) ||
                              u_d.EMAIL.Contains(text) ||
                             u_d.MESSAGE.Contains(text) ||
                             u_d.LEVEL.Contains(text) ||
                             u_d.STATUS.Contains(text) ||
                             u_d.LOGIN_TIME.Contains(text) ||
                             u_d.LOGIN_IP.Contains(text)
                           select u_d).ToList();
            foreach (var item in reports)
            {
                ContactModel model = new ContactModel();
                var output = MappingService.MapClass(item, model);
                contacts.Add(output);
            }
            return contacts;
        }
        //Read -- kawshik
        public static List<ContactModel> GetContactsNonReg(ContactRegModel regModel)
        {
            var contacts = GetContacts();
            return contacts.Where(c => c.EMAIL.Equals(regModel.EMAIL) && c.STATUS.Equals("PENDING"))
                .ToList();
        }
        //Read -- kawshik
        public static List<ContactModel> GetContactsReg(ContactRegModel regModel)
        {
            var contacts = GetContacts();
            return contacts.Where(c => c.EMAIL.Equals(regModel.EMAIL) && c.STATUS.Equals("PENDING")
            && c.USERNAME.Equals(regModel.USERNAME))
                .ToList();

        }

        //Create -- kawshik
        public static bool SendContactMessageNonReg(ContactRegModel regModel)
        {
            if (regModel != null)
            {
                var users = GetContactsNonReg(regModel);
                if (users != null)
                {
                    var count = 0;
                    foreach (var u in users)
                    {
                        count++;
                    }

                    if (count < 3)
                    {
                        CONTACT c = new CONTACT();
                        c.FIRST_NAME = regModel.FIRST_NAME;
                        c.LAST_NAME = regModel.LAST_NAME;
                        c.USERNAME = "";
                        c.EMAIL = regModel.EMAIL;
                        c.MESSAGE = regModel.MESSAGE;
                        c.LEVEL = "NONREG";
                        c.STATUS = "PENDING";
                        c.LOGIN_TIME = IDEAX_Functions.time();
                        c.LOGIN_IP = IDEAX_Functions.ip();

                        return DataAcessFactory.ContactDataAccess().Create(c);

                    }
                }
                return false;
            }
            return false;
        }
        //Create -- kawshik
        public static string SendContactReg(ContactRegModel regModel)
        {

            var users = GetContactsReg(regModel);
            var count = 0;

            if (users.Count() > 0)
            {

                foreach (var u in users)
                {
                    count++;
                }
            }
            if (count < 3)
            {
                var user = AllUserService.GetUserByLevel(regModel.USERNAME, regModel.EMAIL);

                if (user != null)
                {
                    var u_details = UserDetailsService.GetUserDetails(regModel.USERNAME);

                    CONTACT c = new CONTACT();
                    c.FIRST_NAME = u_details.FIRST_NAME;
                    c.LAST_NAME = u_details.LAST_NAME;
                    c.USERNAME = u_details.USERNAME;
                    c.EMAIL = user.EMAIL;
                    c.MESSAGE = regModel.MESSAGE;
                    c.LEVEL = user.LEVEL;
                    c.STATUS = "PENDING";
                    c.LOGIN_TIME = IDEAX_Functions.time();
                    c.LOGIN_IP = IDEAX_Functions.ip();

                    if (DataAcessFactory.ContactDataAccess().Create(c))
                        return "Save";
                    return "DoNotSave";
                }
                return "UserNotMatch";
            }
            return "DoNotSave";
        }
        //Delete -- kawshik
        public static bool DeleteContactMsg(int id)
        {
            if (id != 0)
            {
                var contact = DataAcessFactory.ContactDataAccess().Get(id);
                if (contact != null)
                {
                    return DataAcessFactory.ContactDataAccess().Delete(id);
                }
                return false;
            }
            return false;
        }

        //Update -- kawshik
        public static bool UpdateContactMsg(ContactModel model)
        {

            var data = DataAcessFactory.ContactDataAccess().Get(model.CONTACT_ID);
            if (data != null)
            {
                if (model.MESSAGE != null)
                {
                    data.MESSAGE = model.MESSAGE;
                    return DataAcessFactory.ContactDataAccess().Update(data);
                }
                return false;
            }
            return false;

        }

        public static bool UpdateContactStatus(ContactModel model)
        {

            var data = DataAcessFactory.ContactDataAccess().Get(model.CONTACT_ID);
            if (data != null)
            {
                if (model.STATUS != null)
                {
                    data.STATUS = model.STATUS;
                    return DataAcessFactory.ContactDataAccess().Update(data);
                }
                return false;
            }
            return false;

        }
    }
}

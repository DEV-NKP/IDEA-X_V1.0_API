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
    public class NotesService
    {
        //Create -- dipanwita
        public static bool AddNewNote(NoteModel model)
        {
           if(model != null)
            {
                NOTE note = new NOTE()
                {
                    USERNAME = model.USERNAME,
                    NOTE_DATE = model.NOTE_DATE,
                    NOTE_TEXT = model.NOTE_TEXT,
                    STATUS = "ACTIVE",
                    NOTE_TIME = IDEAX_Functions.time(),
                    NOTE_IP = IDEAX_Functions.ip()
                };
                return DataAcessFactory.NotesDataAccess().Create(note);
            }
            return false;
        }
        //Read -- dipanwita
        public static List<NoteModel> GetAllNotes()
        {
            var data = DataAcessFactory.NotesDataAccess().Get();
            List<NoteModel> notes = new List<NoteModel>();
            foreach (var item in data)
            {
                notes.Add(new NoteModel()
                {
                    NOTE_DATE = item.NOTE_DATE,
                    NOTE_ID = item.NOTE_ID,
                    NOTE_IP = item.NOTE_IP,
                    NOTE_TEXT = item.NOTE_TEXT,
                    NOTE_TIME = item.NOTE_TIME,
                    STATUS = item.STATUS,
                    USERNAME = item.USERNAME
                });
            }
            return notes;
        }
        public static NoteModel IdeaTracker(string uname)
        {
            string today = IDEAX_Functions.date_text();
            var note = GetNotesByUserAndDate(uname, today).SingleOrDefault();
            if(note != null)
            {
                return note;
            }
            else
            {
                NoteModel n = new NoteModel();
                n.NOTE_DATE = today;
                n.NOTE_TEXT = "";
                return n;
            }

        }

        //Read -- dipanwita
        public static List<NoteModel> GetNotesByUserAndDate(string name,string date)
        {
            var data = DataAcessFactory.NotesDataAccess().Get();
            var note = (from pa in data.Where(x => x.USERNAME.Equals(name)
                        && x.NOTE_DATE.Equals(date))
                        select pa).ToList();
            List<NoteModel> notes = new List<NoteModel>();
            foreach (var item in note)
            {
                notes.Add(new NoteModel()
                {
                    NOTE_DATE = item.NOTE_DATE,
                    NOTE_ID = item.NOTE_ID,
                    NOTE_IP = item.NOTE_IP,
                    NOTE_TEXT = item.NOTE_TEXT,
                    NOTE_TIME = item.NOTE_TIME,
                    STATUS = item.STATUS,
                    USERNAME = item.USERNAME
                });
            }
            return notes;
        }
        public static NoteModel SearchNote(string name, string date)
        {
            var note = GetNotesByUserAndDate(name, date).FirstOrDefault();
            if (note != null)
            {
                return note;
            }
            else
            {
                NoteModel n = new NoteModel();
                n.NOTE_DATE = date;
                n.NOTE_TEXT = "";
                return n;
            }
        }


        //Delete -- dipanwita
        public static bool RemoveNote(int note_id)
        {
            if(note_id != 0)
            {
                return DataAcessFactory.NotesDataAccess().Delete(note_id);
            }
            return false;
        }

        //Update -- dipanwita
        public static bool UpdateNote(NoteModel model)
        {
           if(model.NOTE_ID != 0)
            {
                NOTE note = new NOTE()
                {
                    NOTE_ID = model.NOTE_ID,
                    USERNAME = model.USERNAME,
                    NOTE_DATE = model.NOTE_DATE,
                    NOTE_TEXT = model.NOTE_TEXT,
                    STATUS = model.STATUS,
                    NOTE_TIME = IDEAX_Functions.time(),
                    NOTE_IP = IDEAX_Functions.ip()
                };
                return DataAcessFactory.NotesDataAccess().Update(note);
            }
            return false;
        }
    }
}

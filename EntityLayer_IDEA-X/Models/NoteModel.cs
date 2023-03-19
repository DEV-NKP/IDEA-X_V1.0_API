using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer_IDEA_X.Models
{
    public class NoteModel
    {
        public int NOTE_ID { get; set; }
        [Required]
        public string USERNAME { get; set; }
        [Required]
        public string NOTE_DATE { get; set; }
        [Required]
        public string NOTE_TEXT { get; set; }
        public string STATUS { get; set; }
        public string NOTE_TIME { get; set; }
        public string NOTE_IP { get; set; }
    }
}

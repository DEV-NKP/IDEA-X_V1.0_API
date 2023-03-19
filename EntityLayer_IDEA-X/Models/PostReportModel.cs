using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer_IDEA_X.Models
{
    public class PostReportModel
    {
        [Required]
        public int POST_ID { get; set; }
        public string POST_AUTHOR { get; set; }

        public string REPORT_CATEGORY { get; set; }

        public string REPORT_DETAILS { get; set; }
        public string REPORT_TIME { get; set; }
        public string REPORT_IP { get; set; }
        public string REPORT_STATUS { get; set; }
        [Required]
        public string REPORTED_BY { get; set; }

    }
}

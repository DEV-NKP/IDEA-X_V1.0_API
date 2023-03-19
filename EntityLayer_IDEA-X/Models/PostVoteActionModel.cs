using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer_IDEA_X.Models
{
    public class PostVoteActionModel : PostActionModel
    {
        public string VoteCondition { get; set; }
        public int UpvoteCount { get; set; } = 0;
        public int DownVoteCount { get; set; } = 0;
    }
}

using DAL_IDEA_X;
using EntityLayer_IDEA_X.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_IDEA_X.Services
{
    public class UserActivityService
    {
        public static UserActivityModel GetUserActivities(string uname)
        {

            var data = GeneralPostService.GetActivePostList();
            var countLike = (from i in data
                             where i.AUTHOR.Equals(uname)
                             select i.POST_LIKE).ToList();
            var counts = GeneralPostService.GetUserPostActivity(uname);
            int[] arrayLike = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] arrayDislike = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            
            int flagLike = 9;
            int flagDisLike = 9;

            foreach (int a in countLike)
            {

                if (flagLike >= 0)
                {
                    arrayLike[flagLike] = a;
                    flagLike--;
                }
            }

            var countDisLike = (from i in data
                                where i.AUTHOR.Equals(uname)
                                select i.POST_DISLIKE).ToList();

            foreach (int a in countDisLike)
            {
                if (flagDisLike >= 0)
                {
                    arrayDislike[flagDisLike] = a;
                    flagDisLike--;
                }

            }
            UserActivityModel uactivity = new UserActivityModel();
            uactivity.TotalUpvote = counts.POST_LIKE;
            uactivity.TotalDownvote = counts.POST_DISLIKE;
            uactivity.PostLike1 = arrayLike[0];
            uactivity.PostLike2 = arrayLike[1];
            uactivity.PostLike3 = arrayLike[2];
            uactivity.PostLike4 = arrayLike[3];
            uactivity.PostLike5 = arrayLike[4];
            uactivity.PostLike6 = arrayLike[5];
            uactivity.PostLike7 = arrayLike[6];
            uactivity.PostLike8 = arrayLike[7];
            uactivity.PostLike9 = arrayLike[8];
            uactivity.PostLike10 = arrayLike[9];

            uactivity.PostDisLike1 = arrayDislike[0];
            uactivity.PostDisLike2 = arrayDislike[1];
            uactivity.PostDisLike3 = arrayDislike[2];
            uactivity.PostDisLike4 = arrayDislike[3];
            uactivity.PostDisLike5 = arrayDislike[4];
            uactivity.PostDisLike6 = arrayDislike[5];
            uactivity.PostDisLike7 = arrayDislike[6];
            uactivity.PostDisLike8 = arrayDislike[7];
            uactivity.PostDisLike9 = arrayDislike[8];
            uactivity.PostDisLike10 = arrayDislike[9];
            return uactivity;
        }

        public static ActivityActionAdminModel GetUserActivitiesAdmin(string uname)
        {
            var res = true;
            var countpost = PostActionService.GetPostActionListByUserName(uname);
            int countLike = 0;
            int countDislike = 0;
            if (countpost.Count > 0)
            {
              
                foreach (var a in countpost)
                {
                    if (a.POST_STATUS.Equals("UPVOTE"))
                    {
                        countLike++;
                    }
                    if (a.POST_STATUS.Equals("DOWNVOTE"))
                    {
                        countDislike++;
                    }

                }
            }
            else
            {
                res = false;
            }

            int countLiketake = 0;
            int countDisliketake = 0;
            var data = DataAcessFactory.GeneralPostDataAccess().Get();
            var counttakepost = (from i in data
                             where i.AUTHOR.Equals(uname)
                             select i).ToList();
            if(counttakepost.Count > 0)
            {
                foreach (var a in counttakepost)
                {
                    countLiketake += a.POST_LIKE;
                    countDisliketake += a.POST_DISLIKE;
                }

            }
            else
            {
                res = false;
            }
            if(res == true)
            {
                ActivityActionAdminModel apa = new ActivityActionAdminModel();
                apa.USERNAME = uname;
                apa.LIKE = countLike;
                apa.DISLIKE = countDislike;
                apa.LIKEtake = countLiketake;
                apa.DISLIKEtake = countDisliketake;
                return apa;
            }
            return null;
        }

        public static List<ActivityPostAdminModel> SearchUserActivityProgress(string uname)
        {
            var data = DataAcessFactory.GeneralPostDataAccess().Get();
            var countpost = (from i in data
                                 where i.AUTHOR.Equals(uname)
                                 select i).ToList();
            if(countpost.Count > 0)
            {
                List<ActivityPostAdminModel> postact = new List<ActivityPostAdminModel>();
                foreach (var a in countpost)
                {
                    ActivityPostAdminModel postact1 = new ActivityPostAdminModel();
                    postact1.USERNAME = a.AUTHOR;
                    postact1.POST_ID = a.POST_ID;
                    postact1.LIKE = a.POST_LIKE;
                    postact1.DISLIKE = a.POST_DISLIKE;
                    postact.Add(postact1);
                }
                return postact;
            }
            return null;

        }

        public static ActivityPostAdminModel Monitoring()
        {
            var data = DataAcessFactory.GeneralPostDataAccess().Get();
            var countpost = (from i in data
                             orderby i.POST_ID descending
                             select i).ToList();
            if (countpost.Count > 0)
            {
                int countLike = 0;
                int countDislike = 0;
                foreach (var a in countpost)
                {
                    countLike += a.POST_LIKE;
                    countDislike += a.POST_DISLIKE;
                }
                ActivityPostAdminModel apa = new ActivityPostAdminModel();
                apa.LIKE = countLike;
                apa.DISLIKE = countDislike;



                return apa;
            }
            return null;

        }
        public static List<ActivityPostAdminModel> SearchActivityProgress()
        {
            var data = DataAcessFactory.GeneralPostDataAccess().Get();
            var countpost = (from i in data
                             orderby i.POST_ID descending
                             select i).ToList();
            if (countpost.Count > 0)
            {
                List<ActivityPostAdminModel> postact = new List<ActivityPostAdminModel>();
                foreach (var a in countpost)
                {
                    ActivityPostAdminModel postact1 = new ActivityPostAdminModel();
                    postact1.USERNAME = a.AUTHOR;
                    postact1.POST_ID = a.POST_ID;
                    postact1.LIKE = a.POST_LIKE;
                    postact1.DISLIKE = a.POST_DISLIKE;
                    postact.Add(postact1);
                }
                return postact;
            }
            return null;

        }

        public static List<ActivityPostAdminModel> UserActivityProgress()
        {
            var data = DataAcessFactory.GeneralPostDataAccess().Get();
            var countpost = (from i in data
                             orderby i.POST_ID descending
                             select i).ToList();
            if (countpost.Count > 0)
            {
                List<ActivityPostAdminModel> postact = new List<ActivityPostAdminModel>();
                foreach (var a in countpost)
                {
                    ActivityPostAdminModel postact1 = new ActivityPostAdminModel();
                    postact1.USERNAME = a.AUTHOR;
                    postact1.POST_ID = a.POST_ID;
                    postact1.LIKE = a.POST_LIKE;
                    postact1.DISLIKE = a.POST_DISLIKE;
                    postact.Add(postact1);
                }
                return postact;
            }
            return null;

        }
    }
}

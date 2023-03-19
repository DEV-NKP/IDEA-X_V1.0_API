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
    public class GeneralPostService
    {
        //Read -- niloy
        public static GeneralPostModel GetPostByID(int postid)
        {
            var data = DataAcessFactory.GeneralPostDataAccess().Get(postid);
            if (data != null)
            {
                var post = new GeneralPostModel()
                {
                    AUTHOR = data.AUTHOR,
                    POST_ID = data.POST_ID,
                    POSTING_STATUS = data.POSTING_STATUS,
                    POST_DISLIKE = data.POST_DISLIKE,
                    POST_LIKE = data.POST_LIKE,
                    POST_STATUS = data.POSTING_STATUS,
                    POST_TAG = data.POST_TAG,
                    TIMELINE_IMAGE = data.TIMELINE_IMAGE,
                    TIMELINE_TEXT = data.TIMELINE_TEXT,

                };
                return post;
            }
            return null;
        }
        //Read -- niloy
        public static GeneralPostModelWithTimeAndIP GetTimedPostByIDAndUname(int id, string uname)
        {
            var data = DataAcessFactory.GeneralPostDataAccess().Get(id, uname);
            var post = new GeneralPostModelWithTimeAndIP()
            {
                AUTHOR = data.AUTHOR,
                POST_ID = data.POST_ID,
                POSTING_STATUS = data.POSTING_STATUS,
                POST_DISLIKE = data.POST_DISLIKE,
                POST_LIKE = data.POST_LIKE,
                POST_STATUS = data.POSTING_STATUS,
                POST_TAG = data.POST_TAG,
                TIMELINE_IMAGE = data.TIMELINE_IMAGE,
                TIMELINE_TEXT = data.TIMELINE_TEXT,
                POSTING_TIME = data.POSTING_TIME,
                POST_IP = data.POST_IP,

            };
            return post;
        }

        public static List<GeneralPostModel> SearchPost(string text)
        {
            var data = GetActivePostList();
            var post_list = data.Where(p => p.AUTHOR.Equals(text) || p.POST_TAG.Equals(text) ||
            p.TIMELINE_TEXT.Equals(text)).ToList();
            return post_list;
        }

        public static List<GeneralPostModel> SearchPostAdmin(string text)
        {
            var data = DataAcessFactory.GeneralPostDataAccess().Get();
            var post_list = data.Where(p => p.AUTHOR.Equals(text) || p.POST_TAG.Equals(text) ||
            p.TIMELINE_TEXT.Equals(text)).ToList();
            var output = new List<GeneralPostModel>();
            foreach (var item in post_list)
            {
                output.Add(new GeneralPostModel()
                {

                    AUTHOR = item.AUTHOR,
                    POST_ID = item.POST_ID,
                    POSTING_STATUS = item.POSTING_STATUS,
                    POST_DISLIKE = item.POST_DISLIKE,
                    POST_LIKE = item.POST_LIKE,
                    POST_STATUS = item.POSTING_STATUS,
                    POST_TAG = item.POST_TAG,
                    TIMELINE_IMAGE = item.TIMELINE_IMAGE,
                    TIMELINE_TEXT = item.TIMELINE_TEXT,

                });
            }
            return output;


        }


        //Read -- niloy
        public static List<GeneralPostModel> GetActivePostList()
        {
            var data = DataAcessFactory.GeneralPostDataAccess().Get();
            var post_list = data.Where(p => p.POSTING_STATUS.Equals("ACTIVE")).ToList();
            var output = new List<GeneralPostModel>();
            foreach (var item in post_list)
            {
                output.Add(new GeneralPostModel()
                {

                    AUTHOR = item.AUTHOR,
                    POST_ID = item.POST_ID,
                    POSTING_STATUS = item.POSTING_STATUS,
                    POST_DISLIKE = item.POST_DISLIKE,
                    POST_LIKE = item.POST_LIKE,
                    POST_STATUS = item.POSTING_STATUS,
                    POST_TAG = item.POST_TAG,
                    TIMELINE_IMAGE = item.TIMELINE_IMAGE,
                    TIMELINE_TEXT = item.TIMELINE_TEXT,
                    
                });
            }
            return output;
        }
        public static List<GeneralPostModelWithTimeAndIP> GetPostListWithTimeAndIP()
        {
            var data = DataAcessFactory.GeneralPostDataAccess().Get();
            var parchive = (from u_d in data
                            orderby u_d.POST_ID descending
                          select u_d).ToList();
            var output = new List<GeneralPostModelWithTimeAndIP>();
            foreach (var item in parchive)
            {
                output.Add(new GeneralPostModelWithTimeAndIP()
                {

                    AUTHOR = item.AUTHOR,
                    POST_ID = item.POST_ID,
                    POSTING_STATUS = item.POSTING_STATUS,
                    POST_DISLIKE = item.POST_DISLIKE,
                    POST_LIKE = item.POST_LIKE,
                    POST_STATUS = item.POSTING_STATUS,
                    POST_TAG = item.POST_TAG,
                    TIMELINE_IMAGE = item.TIMELINE_IMAGE,
                    TIMELINE_TEXT = item.TIMELINE_TEXT,
                    POSTING_TIME = item.POSTING_TIME,
                    POST_IP = item.POST_IP
                });
            }
            return output;
        }
        public static List<GeneralPostModelWithTimeAndIP> GetPostListUserNameWithTime(string username)
        {
            var data = DataAcessFactory.GeneralPostDataAccess().Get();
            var post_list = data.Where(p => p.AUTHOR.Equals(username)).ToList();
            var output = new List<GeneralPostModelWithTimeAndIP>();
            foreach (var item in post_list)
            {
                output.Add(new GeneralPostModelWithTimeAndIP()
                {

                    AUTHOR = item.AUTHOR,
                    POST_ID = item.POST_ID,
                    POSTING_STATUS = item.POSTING_STATUS,
                    POST_DISLIKE = item.POST_DISLIKE,
                    POST_LIKE = item.POST_LIKE,
                    POST_STATUS = item.POSTING_STATUS,
                    POST_TAG = item.POST_TAG,
                    TIMELINE_IMAGE = item.TIMELINE_IMAGE,
                    TIMELINE_TEXT = item.TIMELINE_TEXT,
                    POSTING_TIME = item.POSTING_TIME

                });
            }
            return output;
        }

        public static List<GeneralPostModelWithTimeAndIP> GetActivePostWithActionsAndTime(string username)
        {
            var post_actions = PostActionService.GetPostActionListByUserName(username);
            var posts = GetPostListUserNameWithTime(username);
            List<GeneralPostModelWithTimeAndIP> post_list = null;
            if (post_actions.Count > 0)
            {
                post_list = (from p in posts
                             join p_a in post_actions
                             on p.POST_ID equals p_a.POST_ID into p_t
                             from post in p_t.DefaultIfEmpty()
                             orderby p.POST_ID descending
                             select new GeneralPostModelWithTimeAndIP()
                             {
                                 POST_ID = p.POST_ID,
                                 POST_TAG = p.POST_TAG,
                                 TIMELINE_TEXT = p.TIMELINE_TEXT,
                                 AUTHOR = p.AUTHOR,
                                 POST_LIKE = p.POST_LIKE,
                                 POST_DISLIKE = p.POST_DISLIKE,
                                 POST_STATUS = (post != null ? post.POST_STATUS : null),
                                 TIMELINE_IMAGE = p.TIMELINE_IMAGE,
                                 POSTING_TIME = p.POSTING_TIME,

                             }).ToList();
            }
            else
            {
                post_list = posts;
            }
            return post_list;
        }

        public static List<GeneralPostModelWithTimeAndIP> GetPostWithTime(string uname)
        {
            var user_data = UserDetailsService.GetUserDetails();
            var posts = GetActivePostWithActionsAndTime(uname);
            var post_list = (from p in posts
                             join u_d in user_data
                             on p.AUTHOR equals u_d.USERNAME into pi_t
                             from post in pi_t.DefaultIfEmpty()
                             orderby p.POST_ID descending
                             select new GeneralPostModelWithTimeAndIP()
                             {
                                 POST_ID = p.POST_ID,
                                 POST_TAG = p.POST_TAG,
                                 TIMELINE_TEXT = p.TIMELINE_TEXT,
                                 AUTHOR = p.AUTHOR,
                                 POST_LIKE = p.POST_LIKE,
                                 POST_DISLIKE = p.POST_DISLIKE,
                                 POST_STATUS = p.POST_STATUS,
                                 TIMELINE_IMAGE = p.TIMELINE_IMAGE,
                                 PROFILE_IMG = post.PROFILE_PICTURE,
                                 POSTING_TIME = p.POSTING_TIME

                             }).ToList();
            foreach (var prop in post_list)
            {
                var postsRep = PostReportService.GetPostReportwithIDByID(prop.POST_ID);
                if (postsRep != null)
                {
                    prop.REPORTED_POST = "REPORTED";
                }
                else
                {
                    prop.REPORTED_POST = "CLEAR";
                }
            }
            return post_list;
        }

        //Read -- niloy
        public static List<GeneralPostModel> GetPostWithProfileImage(string username)
        {
            var user_data = UserDetailsService.GetUserDetails();
            var posts = GetActivePostWithActions(username);
            var post_list = (from p in posts
                              join u_d in user_data
                              on p.AUTHOR equals u_d.USERNAME into pi_t
                              from post in pi_t.DefaultIfEmpty()
                              orderby p.POST_ID descending
                              select new GeneralPostModel()
                              {
                                  POST_ID = p.POST_ID,
                                  POST_TAG = p.POST_TAG,
                                  TIMELINE_TEXT = p.TIMELINE_TEXT,
                                  AUTHOR = p.AUTHOR,
                                  POST_LIKE = p.POST_LIKE,
                                  POST_DISLIKE = p.POST_DISLIKE,
                                  POST_STATUS = p.POST_STATUS,
                                  TIMELINE_IMAGE = p.TIMELINE_IMAGE,
                                  PROFILE_IMG = post.PROFILE_PICTURE,
                                  
                              }).ToList();
            foreach (var prop in post_list)
            {
                var postsRep = PostReportService.GetPostReportwithIDByID(prop.POST_ID);
                if (postsRep != null)
                {
                    prop.REPORTED_POST = "REPORTED";
                }
                else
                {
                    prop.REPORTED_POST = "CLEAR";
                }
            }
            return post_list;
        }


        public static List<GeneralPostModel> GetPostWithProfileImageAdmin(string username)
        {
            var user_data = UserDetailsService.GetUserDetails();
            var posts = GetActivePostWithActions(username);
            var post_list = (from p in posts
                             join u_d in user_data
                             on p.AUTHOR equals u_d.USERNAME into pi_t
                             from post in pi_t.DefaultIfEmpty()
                             orderby p.POST_ID descending
                             select new GeneralPostModel()
                             {
                                 POST_ID = p.POST_ID,
                                 POST_TAG = p.POST_TAG,
                                 TIMELINE_TEXT = p.TIMELINE_TEXT,
                                 AUTHOR = p.AUTHOR,
                                 POST_LIKE = p.POST_LIKE,
                                 POST_DISLIKE = p.POST_DISLIKE,
                                 POST_STATUS = p.POST_STATUS,
                                 TIMELINE_IMAGE = p.TIMELINE_IMAGE,
                                 PROFILE_IMG = post.PROFILE_PICTURE,

                             }).ToList();
            //foreach (var prop in post_list)
            //{
            //    var postsRep = PostReportService.GetPostReportwithIDByID(prop.POST_ID);
            //    if (postsRep != null)
            //    {
            //        prop.REPORTED_POST = "REPORTED";
            //    }
            //    else
            //    {
            //        prop.REPORTED_POST = "CLEAR";
            //    }
            //}
            return post_list;
        }

        public static GeneralPostModel SearchPostView(string uname,int id)
        {
            var postlist = GetPostWithProfileImage(uname);
            var post = postlist.Where(p => p.POST_ID == id).SingleOrDefault();
            return post;
        }

        public static GeneralPostModel SearchPostViewAdmin(string uname, int id)
        {
            var postlist = GetPostWithProfileImageAdmin(uname);
            var post = postlist.Where(p => p.POST_ID == id).SingleOrDefault();
            return post;
        }

        //Read -- niloy
        public static List<GeneralPostModel> GetActivePostWithActions(string username)
        {
            var post_actions = PostActionService.GetPostActionListByUserName(username);
            var posts = GetActivePostList();
            List<GeneralPostModel> post_list = null;
            if(post_actions.Count > 0)
            {
                    post_list = (from p in posts
                                 join p_a in post_actions
                                 on p.POST_ID equals p_a.POST_ID into p_t
                                 from post in p_t.DefaultIfEmpty()
                                 orderby p.POST_ID descending 
                                 select new GeneralPostModel()
                                 {
                                     POST_ID = p.POST_ID,
                                     POST_TAG = p.POST_TAG,
                                     TIMELINE_TEXT = p.TIMELINE_TEXT,
                                     AUTHOR = p.AUTHOR,
                                     POST_LIKE = p.POST_LIKE,
                                     POST_DISLIKE = p.POST_DISLIKE,
                                     POST_STATUS = (post != null ? post.POST_STATUS : null),
                                     TIMELINE_IMAGE = p.TIMELINE_IMAGE,

                                 }).ToList();
            }
            else
            {
                post_list = posts;
            }
            return post_list;
        }

        public static List<GeneralPostModelWithTimeAndIP> GetPostWithActions()
        {
           
            var posts = GetPostListWithTimeAndIP();
            List<PostActionModel> post_actions = new List<PostActionModel>();
            foreach (var p in posts)
            {
                var post_get = PostActionService.GetPostActionListByID(p.POST_ID);

                if (post_get != null)
                {
                    foreach (var p2 in post_get)
                    {
                        post_actions.Add(p2);
                    }
                }

            }
            List<GeneralPostModelWithTimeAndIP> post_list = null;
            if (post_actions.Count > 0)
            {
                post_list = (from p in posts
                             join p_a in post_actions
                             on p.POST_ID equals p_a.POST_ID into p_t
                             from post in p_t.DefaultIfEmpty()
                             orderby p.POST_ID descending
                             select new GeneralPostModelWithTimeAndIP()
                             {
                                 POST_ID = p.POST_ID,
                                 POST_TAG = p.POST_TAG,
                                 TIMELINE_TEXT = p.TIMELINE_TEXT,
                                 AUTHOR = p.AUTHOR,
                                 POST_LIKE = p.POST_LIKE,
                                 POST_DISLIKE = p.POST_DISLIKE,
                                 POST_STATUS = (post != null ? post.POST_STATUS : null),
                                 TIMELINE_IMAGE = p.TIMELINE_IMAGE,
                                 POST_IP =  p.POST_IP,
                                 POSTING_TIME = p.POSTING_TIME,
                                 POSTING_STATUS = p.POSTING_STATUS,
                                 USERNAME = (post != null ? post.USERNAME : null)

                             }).ToList();
            }
            else
            {
                post_list = posts;
            }
            return post_list;
        }

        public static List<GeneralPostModelWithTimeAndIP> SearchPostWithActions(string text)
        {

            var posts = GetPostWithActions();
            var posts_t = (from u_d in posts
                           where u_d.POST_ID.ToString().Contains(text)
                           || (u_d.USERNAME == null ? false : u_d.USERNAME.Contains(text))
                           || u_d.AUTHOR.Contains(text)
                           || u_d.TIMELINE_TEXT.Contains(text)
                           || u_d.POSTING_TIME.Contains(text)
                            || u_d.POST_TAG.Contains(text)
                             || u_d.POSTING_STATUS.Contains(text)
                              || (u_d.POST_STATUS == null ? false : u_d.POST_STATUS.Contains(text))
                           select u_d).ToList();

            return posts_t;
        }

        //Create --niloy
        public static bool CreatePost(GeneralPostModel createPost)
        {
            if(createPost != null)
            {
                if (!String.IsNullOrEmpty(createPost.TIMELINE_TEXT) || createPost.TIMELINE_IMAGE != null)
                {


                    var new_post = new GENERAL_POSTS()
                    {
                        AUTHOR = createPost.AUTHOR,
                        TIMELINE_TEXT = createPost.TIMELINE_TEXT,
                        POST_LIKE = 0,
                        POST_DISLIKE = 0,
                        POST_IP = IDEAX_Functions.ip(),
                        POSTING_TIME = IDEAX_Functions.time(),
                        POSTING_STATUS = "ACTIVE"
                    };

                    if (createPost.POST_TAG != null)
                    {
                        new_post.POST_TAG = $"#{createPost.POST_TAG}";
                    }
                    if (createPost.POST_TAG == null)
                    {
                        new_post.POST_TAG = $"#idea";
                    }
                    if (createPost.TIMELINE_IMAGE != null)
                    {
                        new_post.TIMELINE_IMAGE = createPost.TIMELINE_IMAGE;
                    }
                    return DataAcessFactory.GeneralPostDataAccess().Create(new_post);
                }
                return false;
            }
            return false;
        }       
        //Update -- niloy
        public static bool EditPost(
            GeneralPostEditModel editreq)
        {
            if(editreq != null)
            {
                if(editreq.POST_ID != 0 && editreq.AUTHOR != null)
                {
                    var post = GetTimedPostByIDAndUname(editreq.POST_ID, editreq.AUTHOR);
                    if(editreq.POST_TAG != null)
                    {
                        if (editreq.POST_TAG.IndexOf('#') == -1)
                        {
                            post.POST_TAG = $"#{editreq.POST_TAG}";
                        }
                        else
                        {
                            post.POST_TAG = editreq.POST_TAG;
                        }

                    }
                    

                    if(editreq.TIMELINE_TEXT != null)
                    {
                        post.TIMELINE_TEXT = editreq.TIMELINE_TEXT;
                    }

                    if (editreq.TIMELINE_IMAGE != null)
                    {
                        post.TIMELINE_IMAGE = editreq.TIMELINE_IMAGE;
                    }
                    var update_post = new GENERAL_POSTS()
                    {
                        AUTHOR = post.AUTHOR,
                        TIMELINE_TEXT = post.TIMELINE_TEXT,
                        POST_LIKE = post.POST_LIKE,
                        POST_DISLIKE = post.POST_DISLIKE,
                        POST_TAG = post.POST_TAG,
                        POSTING_STATUS = post.POSTING_STATUS,
                        POSTING_TIME = post.POSTING_TIME,
                        POST_ID = post.POST_ID,
                        POST_IP = post.POST_IP,


                    };

                    return DataAcessFactory.GeneralPostDataAccess().Update(update_post);
                }
                return false;
            }
            return false;
        }
        public static bool UpdatePostVoteCount(GeneralPostModel generalPost)
        {
            if(generalPost != null)
            {
                if (generalPost.POST_ID != 0 && generalPost.AUTHOR != null)
                {
                    var post = GetTimedPostByIDAndUname(generalPost.POST_ID, generalPost.AUTHOR);
                    if(post != null)
                    {
                        post.POST_LIKE = generalPost.POST_LIKE;
                        post.POST_DISLIKE = generalPost.POST_DISLIKE;
                        var update_post = new GENERAL_POSTS()
                        {
                            AUTHOR = post.AUTHOR,
                            TIMELINE_TEXT = post.TIMELINE_TEXT,
                            POST_LIKE = post.POST_LIKE,
                            POST_DISLIKE = post.POST_DISLIKE,
                            POST_TAG = post.POST_TAG,
                            POSTING_STATUS = post.POSTING_STATUS,
                            POSTING_TIME = post.POSTING_TIME,
                            POST_ID = post.POST_ID,
                            POST_IP = post.POST_IP,


                        };
                        return DataAcessFactory.GeneralPostDataAccess().Update(update_post);
                    }
                    return false;
                }
                return false;
            }
            return false;
        }


        public static GeneralPostModel GetUserPostActivity(string uname)
        {
           
            var data = GetActivePostList();
            var countLike = (from i in data
                             where i.AUTHOR.Equals(uname)
                             select i.POST_LIKE).ToList();
            int like = 0;
            int dislike = 0;
            foreach (int a in countLike)
            {
                like += a;
            }

            var countDisLike = (from i in data
                                where i.AUTHOR.Equals(uname)
                                select i.POST_DISLIKE).ToList();

            foreach (int a in countDisLike)
            {
                dislike += a;
            }

            GeneralPostModel postModel = new GeneralPostModel()
            {
                POST_LIKE = like,
                POST_DISLIKE = dislike
            };
            return postModel;
        }

        //Delete -- niloy
        public static bool RemovePost(int id)
        {
            var output = false;
            if(id != 0)
            {
                var post_action_list = PostActionService.GetPostActionListByID(id);
                var post_report_list = PostReportService.GetPostReportListByID(id);
                if(post_action_list != null)
                {
                    post_action_list.ForEach(pa =>
                    {
                        output = DataAcessFactory.PostActionDataAcess()
                        .Delete(pa.POST_ACTION_ID);
                    });
                   
                }
                if(post_report_list != null)
                {
                    post_report_list.ForEach(pr =>
                    {
                        output = DataAcessFactory.PostRepoDataAccess().Delete(pr.POST_ID);
                    });
                }
                output = DataAcessFactory.GeneralPostDataAccess().Delete(id);
            }
            return output;
        }

        public static bool ChangePostStatus(int id,string status)
        {
            
                
                    var post = GetPostByID(id);
                    if(post != null)
                    {
                        var update_post = new GENERAL_POSTS()
                        {
                            POST_ID = post.POST_ID,
                            AUTHOR = post.AUTHOR,
                            TIMELINE_TEXT = post.TIMELINE_TEXT,
                            POST_LIKE = post.POST_LIKE,
                            POST_DISLIKE = post.POST_DISLIKE,
                            POST_TAG = post.POST_TAG,
                            POSTING_STATUS = status,
                        };
                        return DataAcessFactory.GeneralPostDataAccess().Update(update_post);
                    }
                    return false;
              
            
        }
    }
}

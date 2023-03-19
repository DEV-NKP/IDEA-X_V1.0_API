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
    public class PostActionService
    {
        //Read -- kawshik
        public static PostActionModel GetPostActionByUserName(string uname)
        {
            var data = DataAcessFactory.PostActionDataAcess().Get(uname);
            PostActionModel model = new PostActionModel();
            var output = MappingService.MapClass(data, model);
            return output;
            
        }
        //Read -- kawshik
        public static List<PostActionModel> GetPostActionListByUserName(string uname)
        {
            var data = DataAcessFactory.PostActionDataAcess().Get();
            var list = data.Where(pa => pa.USERNAME.Equals(uname)).ToList();
            List<PostActionModel> actionModels = new List<PostActionModel>();
            PostActionModel model = new PostActionModel();
            foreach (var item in list)
            {
                var output = MappingService.MapClass(item, model);
                actionModels.Add(output);

            }
            return actionModels ;

        }


        //Read -- kawshik
        public static List<PostActionModel> GetPostActionListByID(int id)
        {
            var data = DataAcessFactory.PostActionDataAcess().Get();
            var list = data.Where(pa => pa.POST_ID == id).ToList();
            List<PostActionModel> actionModels = new List<PostActionModel>();
            PostActionModel model = new PostActionModel();
            foreach (var item in list)
            {
                var output = MappingService.MapClass(item, model);
                actionModels.Add(output);

            }
            return actionModels;

        }
        //Read -- kawshik
        public static PostActionModel GetPostActionByUnameID(string uname,int postid)
        {
            var data = DataAcessFactory.PostActionDataAcess().Get(postid, uname);
            PostActionModel model = new PostActionModel();
            var output = MappingService.MapClass(data, model);
            return output;
        }
        //Delete -- kawshik
        public static bool RemovePostAction(int postid)
        {
            if(postid != 0)
            {
                var post = DataAcessFactory.PostActionDataAcess().Get(postid);
                 if(post != null)
                {
                    return DataAcessFactory.PostActionDataAcess().Delete(postid);
                }
                return false;
            }
            return false;
        }
        //Create -- kawshik
        public static bool SendUpvotePostAction(PostActionModel model)
        {
            if(model != null)
            {
                var user = AllUserService.GetUserByName(model.USERNAME);
                if(user != null)
                {
                    POST_ACTIONS p_a = new POST_ACTIONS();
                    model.POST_STATUS = "UPVOTE";
                    var output = MappingService.MapClass(model, p_a);
                    return DataAcessFactory.PostActionDataAcess().Create(output);
                }
                return false;
            }
            return false;
        }
        //Create -- kawshik
        public static bool SendDownvotePostAction(PostActionModel model)
        {
            POST_ACTIONS p_a = new POST_ACTIONS();
            model.POST_STATUS = "DOWNVOTE";
            var output = MappingService.MapClass(model, p_a);
            return DataAcessFactory.PostActionDataAcess().Create(output);
        }
        //Update -- kawshik
        public static bool UpdatePostAction(PostActionModel model)
        {
            if(model != null)
            {
                var postaction = DataAcessFactory.PostActionDataAcess()
                    .Get(model.POST_ACTION_ID);
                if(postaction != null)
                {
                    POST_ACTIONS p_a = new POST_ACTIONS()
                    {
                        POST_ACTION_ID = postaction.POST_ACTION_ID,
                        POST_ID = postaction.POST_ID,
                        POST_STATUS = model.POST_STATUS,
                        USERNAME = postaction.USERNAME
                    };

                    return DataAcessFactory.PostActionDataAcess().Update(p_a);
                }
                return false;

            }
            return false;
        }

        public static PostVoteActionModel PostVoteActionRequest(PostVoteActionModel postVote )
        {
            PostVoteActionModel output = new PostVoteActionModel();
            if(postVote != null)
            {
                if (postVote.POST_ID != null && postVote.VoteCondition != null 
                    && postVote.USERNAME != null)
                {
                    var res = false;
                    if (postVote.VoteCondition.Equals("UPVOTE-DECLINE"))
                    {


                        var post = GeneralPostService.GetPostByID((int)postVote.POST_ID);

                        if(post != null)
                        {
                            post.POST_LIKE--;


                            var p_a = DataAcessFactory.PostActionDataAcess().Get(post.POST_ID,
                                postVote.USERNAME);
                            if (p_a != null)
                            {
                                res = GeneralPostService.UpdatePostVoteCount(post);
                                res = DataAcessFactory.PostActionDataAcess().Delete(post.POST_ID, postVote.USERNAME);

                                output.UpvoteCount = post.POST_LIKE;
                                output.DownVoteCount = post.POST_DISLIKE;
                            }

                        }
                        else
                        {
                            res = false;
                        }


                    }

                    if (postVote.VoteCondition.Equals("UPVOTE-ACCEPT"))
                    {

                        var post = GeneralPostService.GetPostByID((int)postVote.POST_ID);

                        if (post != null)
                        {
                            post.POST_LIKE++;


                            res = GeneralPostService.UpdatePostVoteCount(post);

                            var post_action = DataAcessFactory.PostActionDataAcess().Get(
                                post.POST_ID, post.AUTHOR);
                            if(post_action != null)
                            {
                                post.POST_DISLIKE--;
                                res = DataAcessFactory.PostActionDataAcess().Delete(
                                    post.POST_ID, post.AUTHOR);

                            }
                            var pa = new POST_ACTIONS();
                            pa.POST_ID = post.POST_ID;
                            pa.USERNAME = postVote.USERNAME;
                            pa.POST_STATUS = "UPVOTE";
                           
                            res = DataAcessFactory.PostActionDataAcess().Create(pa);
                            output.UpvoteCount = post.POST_LIKE;
                            output.DownVoteCount = post.POST_DISLIKE;

                        }
                        else
                        {
                            res = false;
                        }

                        
                    }

                    if (postVote.VoteCondition.Equals("DOWNVOTE-DECLINE"))
                    {


                        var post = GeneralPostService.GetPostByID((int)postVote.POST_ID);

                        if (post != null)
                        {
                            post.POST_DISLIKE--;

                            var p_a = DataAcessFactory.PostActionDataAcess().Get(post.POST_ID,
                                postVote.USERNAME);
                           if(p_a != null)
                            {
                                res = GeneralPostService.UpdatePostVoteCount(post);
                                res = DataAcessFactory.PostActionDataAcess().Delete(post.POST_ID, postVote.USERNAME);

                                output.UpvoteCount = post.POST_LIKE;
                                output.DownVoteCount = post.POST_DISLIKE;
                            }
                           

                        }
                        else
                        {
                            res = false;
                        }
                    }


                    if (postVote.VoteCondition.Equals("DOWNVOTE-ACCEPT"))
                    {


                        var post = GeneralPostService.GetPostByID((int)postVote.POST_ID);

                        if (post != null)
                        {
                            post.POST_DISLIKE++;


                            res = GeneralPostService.UpdatePostVoteCount(post);

                            var post_action = DataAcessFactory.PostActionDataAcess().Get(
                                post.POST_ID, post.AUTHOR);
                            if (post_action != null)
                            {
                                post.POST_LIKE--;
                                res = DataAcessFactory.PostActionDataAcess().Delete(
                                    post.POST_ID, post.AUTHOR);

                            }
                            var pa = new POST_ACTIONS();
                            pa.POST_ID = post.POST_ID;
                            pa.USERNAME = post.AUTHOR;
                            pa.POST_STATUS = "DOWNVOTE";
                            res = DataAcessFactory.PostActionDataAcess().Create(pa);
                            output.UpvoteCount = post.POST_LIKE;
                            output.DownVoteCount = post.POST_DISLIKE;

                        }
                        else
                        {
                            res = false;
                        }
                    }
                    if (res)
                    {
                        return output;
                    }
                    return null;
                }
                return null;
            }
            return null;

        }
    }
}

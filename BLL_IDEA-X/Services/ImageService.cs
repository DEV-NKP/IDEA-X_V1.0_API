using DAL_IDEA_X;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_IDEA_X.Services
{
    public class ImageService
    {
        public static string GetPostImage(int id)
        {
            var data = GeneralPostService.GetPostByID(id);
            if(data.TIMELINE_IMAGE != null)
            {
                //using(var ms = new MemoryStream(data.TIMELINE_IMAGE))
                //{
                //    return Image.FromStream(ms);
                //}
                return Convert.ToBase64String(data.TIMELINE_IMAGE);
            }
            return null;
        }

        public static string GetProfilePicture(string uname)
        {
            var data = UserDetailsService.GetUserDetails(uname);
            if (data.PROFILE_PICTURE != null)
            {
                //using (var ms = new MemoryStream(data.PROFILE_PICTURE))
                //{
                //    return Image.FromStream(ms);
                //}
                return Convert.ToBase64String(data.PROFILE_PICTURE);
            }
            return null;
        }
    }
}

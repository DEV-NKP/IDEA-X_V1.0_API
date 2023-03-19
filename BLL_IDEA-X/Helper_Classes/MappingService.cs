using AutoMapper;
using DAL_IDEA_X.EntityFramework;
using EntityLayer_IDEA_X;
using EntityLayer_IDEA_X.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_IDEA_X.Helper_Classes
{
    public class MappingService
    {
        static MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<ALL_USERS, AllUserModel>().ReverseMap();
            cfg.CreateMap<USER_DETAILS, UserDetailsModel>().ReverseMap();
            cfg.CreateMap<ADMIN, AdminModel>().ReverseMap();
            cfg.CreateMap<GENERAL_POSTS, GeneralPostModel>().ReverseMap();
            cfg.CreateMap<POST_ACTIONS, PostActionModel>().ReverseMap();
            cfg.CreateMap<POST_REPORT, PostReportModel>().ReverseMap();
            cfg.CreateMap<CONTACT, ContactModel>().ReverseMap();
            cfg.CreateMap<CHAT_BOXS, ChatBoxModel>().ReverseMap();
        });

        public static TDest MapClass<TSource,TDest>(TSource source,TDest dest)
        {
            var mapper = config.CreateMapper();
            TDest result = mapper.Map<TDest>(source);
            return result;
        }
    }
}

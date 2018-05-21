using AutoMapper;
using DAO.DTO;
using DAO.Models;

namespace DAO.Profiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom( x => x.Id))
                .ForMember(x => x.Content, opt => opt.MapFrom( x => x.Content))
                .ForMember(x => x.Title, opt => opt.MapFrom( x => x.Title))
                .ForMember(x => x.UserId, opt => opt.MapFrom( x => x.UserId));

            CreateMap<PostDto, Post>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Content, opt => opt.MapFrom(x => x.Content))
                .ForMember(x => x.Title, opt => opt.MapFrom(x => x.Title))
                .ForMember(x => x.User, opt => opt.Ignore())
                .ForMember(x => x.Comments, opt => opt.Ignore())
                .ForMember(x => x.Title, opt => opt.MapFrom(x => x.Title))
                .ForMember(x => x.UserId, opt => opt.MapFrom(x => x.UserId));

        }
    }
}

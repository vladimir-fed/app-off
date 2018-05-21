using AutoMapper;
using DAO.DTO;
using DAO.Models;

namespace DAO.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<SignUpDto, User>()
                .ForMember(x => x.Password, opt => opt.Ignore())
                .ForMember(x => x.PasswordText, opt => opt.MapFrom(x => x.Password))
                .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.UserName))
                .ForMember(x => x.Posts, opt => opt.Ignore())
                .ForMember(x => x.Salt, opt => opt.Ignore())
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name));

            CreateMap<User, UserDto>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.UserName))
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name));
        }
    }
}

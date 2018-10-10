using System.Linq;
using AutoMapper;
using DotNetPractice.DTOS;
using DotNetPractice.Models;

namespace DotNetPractice.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>().ForMember(dest => dest.PhotoUrl, opt =>
                {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.isProfilePhoto).PhotoUrl);
                })
                .ForMember(dest => dest.Age, opt =>{
                    opt.ResolveUsing(d => d.DateOfBirth.CalculateAge());
                });
            CreateMap<User, UserForDetailDto>().ForMember(dest => dest.PhotoUrl, opt =>
                {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.isProfilePhoto).PhotoUrl);
                })
                .ForMember(dest => dest.Age, opt =>{
                    opt.ResolveUsing(d => d.DateOfBirth.CalculateAge());
                });
            CreateMap<Photo, PhotoForDetailDto>();

            CreateMap<UserForUpdateDto, User>();
            CreateMap<UserForDetailDto, User>();
            CreateMap<Photo, PhotoFromReturnDto>();
            CreateMap<PhotoForCreationDto, Photo>();
            CreateMap<UserToRegisterDto, User>();
            CreateMap<MessageForCreationDto, Message>().ReverseMap();
            CreateMap<Message, MessageToReturnDto>()
                .ForMember(m => m.SenderPhotoUrl, opt => opt.MapFrom(u => u.Sender.Photos.FirstOrDefault(p => p.isProfilePhoto).PhotoUrl))
                .ForMember(m => m.SenderPhotoUrl, opt => opt.MapFrom(u => u.Sender.Photos.FirstOrDefault(p => p.isProfilePhoto).PhotoUrl));
        }
    }
}
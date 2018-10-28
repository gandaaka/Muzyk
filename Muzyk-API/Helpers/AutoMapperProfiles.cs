using System.Linq;
using AutoMapper;
using Muzyk_API.Models;
using Muzyk_API.DTOS;

namespace Muzyk_API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>().ForMember(dest => dest.PhotoUrl, opt =>
                {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.isProfilePhoto).MediaUrl);
                })
                .ForMember(dest => dest.Age, opt =>{
                    opt.ResolveUsing(d => d.DateOfBirth.CalculateAge());
                });
            CreateMap<User, UserForDetailDto>().ForMember(dest => dest.PhotoUrl, opt =>
                {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.isProfilePhoto).MediaUrl);
                })
                .ForMember(dest => dest.Age, opt =>{
                    opt.ResolveUsing(d => d.DateOfBirth.CalculateAge());
                });
            CreateMap<UserForUpdateDto, User>();
            CreateMap<UserForDetailDto, User>();
            CreateMap<UserToRegisterDto, User>();
            //photo DTo
            CreateMap<Photo, PhotoForDetailDto>();
            CreateMap<Photo, PhotoFromReturnDto>();
            CreateMap<PhotoForCreationDto, Photo>();
            //video DTo
            CreateMap<VideoForCreationDto, Video>();
            CreateMap<Video, VideoForReturnDto>();
            CreateMap<Video, VideoForDetailDto>();
            //MediaDto Map
            CreateMap<Media, MediaForDetailDto>();
            //messageDto
            CreateMap<MessageForCreationDto, Message>().ReverseMap();
            CreateMap<Message, MessageToReturnDto>()
                .ForMember(m => m.SenderPhotoUrl, opt => opt.MapFrom(u => u.Sender.Photos.FirstOrDefault(p => p.isProfilePhoto).MediaUrl))
                .ForMember(m => m.SenderPhotoUrl, opt => opt.MapFrom(u => u.Sender.Photos.FirstOrDefault(p => p.isProfilePhoto).MediaUrl));

            //bookingDto Map
            CreateMap<Booking, BookingsToReturnDto>();
        }
    }
}
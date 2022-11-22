using AutoMapper;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_DataBase.Entities;
using WebAppMedicalAssistantMVC.Models;

namespace WebAppMedicalAssistantMVC.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dto => dto.RoleName, opt => opt.MapFrom(entity => entity.Roles.Name))
                .ForMember(dto => dto.Id, opt => opt.MapFrom(entity => entity.Id));

            CreateMap<RegisterModel, UserDto>()
                .ForMember(dto => dto.Email, opt => opt.MapFrom(model => model.Email))
                .ForMember(dto => dto.PasswordHash, opt => opt.MapFrom(model => model.Password));

            CreateMap<UserDto, User>();

            CreateMap<UserDto, UserModel>();
        }
    }
}

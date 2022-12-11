using AutoMapper;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_DataBase.Entities;
using WebAppMedicalAssistantAPI.Model.Requests;

namespace WebAppMedicalAssistantMVC.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dto => dto.RoleName, opt => opt.MapFrom(entity => entity.Roles.Name))
                .ForMember(dto => dto.Id, opt => opt.MapFrom(entity => entity.Id));

            CreateMap<UserDto, User>();

            CreateMap<RegisterUserRequestModel, UserDto>();
        }
    }
}

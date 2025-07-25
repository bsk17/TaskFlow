using AutoMapper;

namespace TaskFlow.Api.Mappings
{
    public class UserProfile : Profile
    {
        /// <summary>
        /// UserProfile constructor
        /// Initializes a new instance of the UserProfile class.
        /// This class is used to define the mapping configurations for User-related DTOs and Models.
        /// <remarks>
        /// The UserProfile class inherits from AutoMapper.Profile and is used to configure the mappings between
        /// the User entity and its corresponding Data Transfer Objects (DTOs).
        /// It includes mappings for UserReadDto, UserCreateDto, and UserUpdateDto.
        /// The CreateMap method is used to define the source and destination types for the mappings.
        /// The mappings ensure that properties are correctly mapped between the User model and the DTOs,
        /// including any necessary transformations such as setting the CreatedAt property to the current UTC time. 
        /// </summary>
        public UserProfile()
        {
            CreateMap<Models.User, DTOs.UserReadDto>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name));
            CreateMap<DTOs.UserCreateDto, Models.User>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<DTOs.UserUpdateDto, Models.User>();
        }
    }
}
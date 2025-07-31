using AutoMapper;
using TaskFlow.Api.DTOs;
using TaskFlow.Api.Models;

namespace TaskFlow.Api.Mappings
{
    public class ActivityLogProfile : Profile
    {
        public ActivityLogProfile()
        {
            CreateMap<ActivityLog, ActivityLogReadDto>();
        }
    }

}
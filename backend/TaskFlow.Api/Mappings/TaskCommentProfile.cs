using AutoMapper;
using TaskFlow.Api.DTOs;
using TaskFlow.Api.Models;

namespace TaskFlow.Api.Mappings
{
    public class TaskCommentProfile : Profile
    {
        public TaskCommentProfile()
        {
            CreateMap<TaskComment, TaskCommentReadDto>()
                .ForMember(dest => dest.AuthorUsername, opt => opt.MapFrom(src => src.Author.Username));
        }
    }
}
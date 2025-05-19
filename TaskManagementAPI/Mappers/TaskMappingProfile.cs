using AutoMapper;
using TaskManagementAPI.DTOs;
using TaskManagementAPI.Entities;

namespace TaskManagementAPI.Mappers;

public class TaskMappingProfile : Profile
{
    public TaskMappingProfile()
    {
        CreateMap<TaskItem, TaskResponseDto>();
        CreateMap<TaskRequestDto, TaskItem>();
        CreateMap<TaskUpdateDto, TaskItem>().
            ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}

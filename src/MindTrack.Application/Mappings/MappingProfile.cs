using AutoMapper;
using MindTrack.Domain.Entities;
using MindTrack.Application.DTOs.Users;
using MindTrack.Application.DTOs.Tasks;
using MindTrack.Application.DTOs.FocusRecords;

namespace MindTrack.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Users
            CreateMap<User, UserReadDto>().ReverseMap();
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();

            // Tasks
            CreateMap<TaskItem, TaskReadDto>().ReverseMap();
            CreateMap<TaskCreateDto, TaskItem>();
            CreateMap<TaskUpdateDto, TaskItem>();

            // Focus Records
            CreateMap<FocusRecord, FocusRecordReadDto>().ReverseMap();
            CreateMap<FocusRecordCreateDto, FocusRecord>();
        }
    }
}

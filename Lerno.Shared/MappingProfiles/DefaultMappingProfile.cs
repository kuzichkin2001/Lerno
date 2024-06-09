using AutoMapper;
using Lerno.Shared.DTOs;
using Lerno.Shared.Models;

namespace Lerno.Shared.MappingProfiles
{
    public class DefaultMappingProfile : Profile
    {
        public override string ProfileName => "UsersMappingProfile";

        public DefaultMappingProfile()
        {
            // Student
            CreateMap<StudentDTO, Student>();
            CreateMap<Student, StudentDTO>();
            
            // Teacher
            CreateMap<TeacherDTO, Teacher>();
            CreateMap<Teacher, TeacherDTO>();

            // User
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();
        }
    }
}

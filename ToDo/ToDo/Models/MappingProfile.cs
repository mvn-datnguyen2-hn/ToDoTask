using AutoMapper;
using ToDo.DTOs;

namespace ToDo.Models
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Model.ToDo, ToDoRequest>().ReverseMap();
        }
    }
}

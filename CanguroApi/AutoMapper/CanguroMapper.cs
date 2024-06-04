using AutoMapper;
using CanguroApi.Domain.Entities;
using CanguroApi.DTO;

namespace CanguroApi.AutoMapper
{
    public class CanguroMapper:Profile
    {
        public CanguroMapper() { 
            CreateMap<MovCanguro, MovCanguroDTO>().ReverseMap();
        }
    }
}

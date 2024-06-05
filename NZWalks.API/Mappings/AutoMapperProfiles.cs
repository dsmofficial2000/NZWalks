using AutoMapper;
using NZWalks.API.Models.DTO;
using NZWalks.Models.Domain;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //Region Controller
            CreateMap<Region, RegionsDto>().ReverseMap();
            CreateMap<AddRegionDtos, Region>().ReverseMap();
            CreateMap<updateRegionDto, Region>().ReverseMap();

            //Walk Controller
            CreateMap<AddWalkRequestDTO, Walk>().ReverseMap();            
            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
            CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap();
        }
    }
}

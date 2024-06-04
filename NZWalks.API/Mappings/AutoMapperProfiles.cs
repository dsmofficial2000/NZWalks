﻿using AutoMapper;
using NZWalks.API.Models.DTO;
using NZWalks.Models.Domain;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionsDto>().ReverseMap();
            //CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            //CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
        }
    }
}

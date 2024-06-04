using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using NZWalks.Data;
using NZWalks.Models.Domain;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : Controller
    {
        public NzWalksDBContext DBContext;
        public IRegionRepository RegionRepository;
        private readonly IMapper mapper;

        public RegionsController(NzWalksDBContext dBContext, IRegionRepository regionRepository, IMapper mapper)
        {
            DBContext = dBContext;
            RegionRepository = regionRepository;
            this.mapper = mapper;
        }   


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get Data from Database -Domain Model
            var regionsDomain = await RegionRepository.GetAllAsync();

            //Map domain model to DTOs
            //var regionDto = new List<RegionsDto>();
            //foreach (var rDomain in regionsDomain)
            //{
            //    regionDto.Add(new RegionsDto()
            //    {
            //        Id = rDomain.Id,
            //        Name = rDomain.Name,
            //        Code = rDomain.Code,
            //        RegionImageUrl = rDomain.RegionImageUrl
            //    });
            //}

            //Map domain model to DTOs
            var regionDto = mapper.Map<List<RegionsDto>>(regionsDomain);

            //Return DTO
            return Ok(regionDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetRegionById([FromRoute]Guid id)
        {
            //using find() method
            //var region = DBContext.regions.Find(id);

            //using LINQ
            //var region = DBContext.regions.FirstOrDefault(x => x.Id == id);

            //get region domain model from database
            var regionDomain = await RegionRepository.GetRegionByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            var regionDto = new RegionsDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };
            //Map/Convert region domain model to region DTOs
            return Ok(regionDto);
        }

        //POST to create new Region
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionDtos addRegionDtos)
        {
            //Map or convert DTO to domain moel 
            var regionDomainModel = new Region
            {
                Code = addRegionDtos.Code,
                Name = addRegionDtos.Name,
                RegionImageUrl = addRegionDtos.RegionImageUrl
            };

            //Use domain model to crate region
            regionDomainModel = await RegionRepository.CreateAsync(regionDomainModel);

            //Map Domain model back to DTOs
            var regionDto = new RegionsDto
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetRegionById), new { id = regionDto.Id }, regionDto);
        }

        //PUT to update Region
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] updateRegionDto updateRegionDto)
        {
            var regiondomainModel = new Region
            {
                Code = updateRegionDto.Code,
                Name = updateRegionDto.Name,
                RegionImageUrl = updateRegionDto.RegionImageUrl
            };

            //check if resion is exist
            regiondomainModel = await RegionRepository.UpdateAsync(id, regiondomainModel);

            if (regiondomainModel == null)
            {
                return NotFound();
            }
            
            //Convert domain model into DTOs
            var regionDto = new RegionsDto
            {
                Id = regiondomainModel.Id,
                Code = regiondomainModel.Code,
                Name = regiondomainModel.Name,
                RegionImageUrl = regiondomainModel.RegionImageUrl
            };
            return Ok(regionDto);
        }

        //DELETE to delete region
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await RegionRepository.DeleteAsync(id);
                
            if(regionDomainModel == null)
            {
                return NotFound();
            }           

            //return delte region back
            //map domain Model to DTOs

            //Convert domain model into DTOs
            var regionDto = new RegionsDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return Ok(regionDto);    
        }
    }
}

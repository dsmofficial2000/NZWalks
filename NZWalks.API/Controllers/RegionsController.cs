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
            //Return DTO
            return Ok(mapper.Map<List<RegionsDto>>(regionsDomain));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetRegionById([FromRoute]Guid id)
        {            
            //get region domain model from database
            var regionDomain = await RegionRepository.GetRegionByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }            
            //Map/Convert region domain model to region DTOs
            return Ok(mapper.Map<RegionsDto>(regionDomain));
        }

        //POST to create new Region
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionDtos addRegionDtos)
        {
            //Map or convert DTO to domain moel 
            var regionDomainModel = mapper.Map<Region>(addRegionDtos);

            //Use domain model to crate region
            regionDomainModel = await RegionRepository.CreateAsync(regionDomainModel);

            var regionDto = mapper.Map<RegionsDto>(regionDomainModel);
            //Map Domain model back to DTOs            
            return CreatedAtAction(nameof(GetRegionById), new { id = regionDto.Id }, regionDto);
        }

        //PUT to update Region
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] updateRegionDto updateRegionDto)
        {
            var regiondomainModel = mapper.Map<Region>(updateRegionDto);

            //check if resion is exist
            regiondomainModel = await RegionRepository.UpdateAsync(id, regiondomainModel);

            if (regiondomainModel == null)
            {
                return NotFound();
            }
            
            //Convert domain model into DTOs
            var regionDto = mapper.Map<RegionsDto>(regiondomainModel);
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
            //Convert domain model into DTOs            
            return Ok(mapper.Map<RegionsDto>(regionDomainModel));
        }
    }
}

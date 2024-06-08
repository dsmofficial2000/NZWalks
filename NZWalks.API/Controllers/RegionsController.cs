using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            //Get Data from Database -Domain Model
            var regionsDomain = await RegionRepository.GetAllAsync();            
            //Return DTO
            return Ok(mapper.Map<List<RegionsDto>>(regionsDomain));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Reader")]
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
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionDtos addRegionDtos)
        {
            if(ModelState.IsValid)
            {
                //Map or convert DTO to domain moel 
                var regionDomainModel = mapper.Map<Region>(addRegionDtos);

                //Use domain model to crate region
                regionDomainModel = await RegionRepository.CreateAsync(regionDomainModel);

                var regionDto = mapper.Map<RegionsDto>(regionDomainModel);
                //Map Domain model back to DTOs            
                return CreatedAtAction(nameof(GetRegionById), new { id = regionDto.Id }, regionDto);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //PUT to update Region
        [HttpPut]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] updateRegionDto updateRegionDto)
        {
            if (ModelState.IsValid)
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
            else
            {
                return BadRequest(ModelState);
            }
        }

        //DELETE to delete region
        [HttpDelete]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Writer,Reader")]
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

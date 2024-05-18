using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.Data;
using NZWalks.Models.Domain;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : Controller
    {
        public NzWalksDBContext DBContext { get; }
        public RegionsController(NzWalksDBContext dBContext)
        {
            DBContext = dBContext;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            //Get Data from Database -Domain Model
            var regionsDomain = DBContext.regions.ToList();

            //Map domain model to DTOs
            var regionDto = new List<RegionsDto>();
            foreach (var rDomain in regionsDomain)
            {
                regionDto.Add(new RegionsDto()
                {
                    Id = rDomain.Id,
                    Name = rDomain.Name,
                    Code = rDomain.Code,
                    RegionImageUrl = rDomain.RegionImageUrl
                });
            }

            //Return DTO
            return Ok(regionDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetRegionById(Guid id)
        {
            //using find() method
            //var region = DBContext.regions.Find(id);

            //using LINQ
            //var region = DBContext.regions.FirstOrDefault(x => x.Id == id);

            //get region domain model from database
            var regionDomain = DBContext.regions.FirstOrDefault(x => x.Id == id);

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
        public IActionResult Create([FromBody] AddRegionDtos addRegionDtos)
        {
            //Map or convert DTO to domain moel 
            var regionDomainModel = new Region
            {
                Code = addRegionDtos.Code,
                Name = addRegionDtos.Name,
                RegionImageUrl = addRegionDtos.RegionImageUrl
            };

            //Use domain model to crate region
            DBContext.regions.Add(regionDomainModel);
            DBContext.SaveChanges();

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
        public IActionResult Update([FromRoute] Guid id, [FromBody] updateRegionDto updateRegionDto)
        {
            //check if resion is exist
            var regiondomainModel = DBContext.regions.FirstOrDefault(x => x.Id == id);

            if (regiondomainModel == null)
            {
                return NotFound();
            }

            //Map DTO to domain model
            regiondomainModel.Code = updateRegionDto.Code;
            regiondomainModel.Name = updateRegionDto.Name;
            regiondomainModel.RegionImageUrl = updateRegionDto.RegionImageUrl;

            DBContext.SaveChanges();

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
        public IActionResult Delete([FromRoute] Guid id)
        {
            var regionDomainModel = DBContext.regions.FirstOrDefault(x => x.Id == id);

            if(regionDomainModel == null)
            {
                return NotFound();
            }

            //Delete region
            DBContext.regions.Remove(regionDomainModel);
            DBContext.SaveChanges();

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

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            var region = DBContext.regions.ToList();   
            return Ok(region);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetRegionById(Guid id)
        {
            //using find() method
            //var region = DBContext.regions.Find(id);

            //using LINQ
            var region = DBContext.regions.FirstOrDefault(x => x.Id == id);

            if(region == null)
            {
                return NotFound();
            }
            return Ok(region);
        }
    }
}

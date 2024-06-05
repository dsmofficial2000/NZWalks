using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using NZWalks.Models.Domain;

namespace NZWalks.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        public IWalksRepository WalksRepository { get; set; }
        public WalksController(IMapper mapper, IWalksRepository walksRepository)
        {
            this.mapper = mapper;
            this.WalksRepository = walksRepository;
        }

        // CREATE Walk
        // POST: /api/walks
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDTO addWalkRequestDto)
        {
            // Map DTO to Domain Model
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

            await WalksRepository.CreateAsync(walkDomainModel);

            // Map Domain model to DTO
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }
                
    }
}

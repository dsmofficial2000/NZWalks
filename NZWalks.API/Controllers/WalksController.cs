using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using NZWalks.Models.Domain;
using System.Collections.Generic;

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

        // GET Walks
        // GET: /api/walks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walksDomain = await WalksRepository.GetallAsync();
            return Ok(mapper.Map<List<WalkDto>>(walksDomain));
        }

        // Get Walks By Id
        // Get: /api/walks/id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetWalksById([FromRoute] Guid id)
        {
            //get region domain model from database
            var walksDomain = await WalksRepository.GetByIdAsync(id);
            if (walksDomain == null)
            {
                return NotFound();
            }
            //Map/Convert region domain model to region DTOs
            return Ok(mapper.Map<WalkDto>(walksDomain));
        }

        // Update Walk By Id
        // PUT: /api/Walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]        
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {
            // Map DTO to Domain Model            
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

            //Check if walk is exist
            walkDomainModel = await WalksRepository.UpdateAsync(id, walkDomainModel);            

            if (walkDomainModel == null)
            {
                return NotFound();
            }
            
            // Map Domain Model to DTO
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }
    }
}

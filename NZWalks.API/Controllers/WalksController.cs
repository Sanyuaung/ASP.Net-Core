using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        #region Constructor
        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }
        #endregion

        #region CreateWalk
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreteWalk([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);
            walkDomainModel = await walkRepository.CreateAsync(walkDomainModel);
            var walkDto = mapper.Map<WalkDto>(walkDomainModel);
            return CreatedAtAction(nameof(GetWalkById), new { id = walkDto.Id }, walkDto);
        }
        #endregion

        #region GetAllWalks
        [HttpGet]
        public async Task<IActionResult> GetAllWalks([FromQuery] string? filterOn,[FromQuery] string? filterQuery)
        {
            var walksDomainModel = await walkRepository.GetWalksAsync(filterOn,filterQuery);
            var walksDto = mapper.Map<List<WalkDto>>(walksDomainModel);
            return Ok(walksDto);
        }
        #endregion

        #region GetWalkById
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetWalkById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            var walkDto = mapper.Map<WalkDto>(walkDomainModel);
            return Ok(walkDto);
        }
        #endregion

        #region UpdateWalk
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);
            walkDomainModel = await walkRepository.UpdateWalkAsync(id, walkDomainModel);
            if (walkDomainModel == null)
            {
                return NotFound(new { Message = $"Walk with Id {id} not found!." });
            }
            var regionDto = mapper.Map<WalkDto>(walkDomainModel);
            return Ok(regionDto);
        }
        #endregion

        #region DeleteWalk
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteWalk([FromRoute] Guid id)
        {
            var walkDomailModel = await walkRepository.DeleteWalkAsync(id);
            if (walkDomailModel == null)
            {
                return NotFound(new { Message = $"Walk with Id {id} not found!." });
            }
            var walkDto = mapper.Map<WalkDto>(walkDomailModel);
            return Ok(walkDto);
        }
        #endregion
    }
}

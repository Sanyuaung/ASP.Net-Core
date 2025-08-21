using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using NZWalks.API.Models.Responses;

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
            return CreatedAtAction(nameof(GetWalkById), new { id = walkDto.Id }, ApiResponse.Lists(walkDto));
        }
        #endregion

        #region GetAllWalks
        [HttpGet]
        public async Task<IActionResult> GetAllWalks([FromQuery] string? filterOn,[FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var (walksDomainModel, totalCount) = await walkRepository.GetWalksAsync(filterOn,filterQuery,sortBy,isAscending ?? true,pageNumber,pageSize);
            var walksDto = mapper.Map<List<WalkDto>>(walksDomainModel);

            var response = ApiResponse.WithPagination(walksDto, pageNumber, pageSize, totalCount);
            return Ok(response);
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
            return Ok(ApiResponse.Lists(walkDto));
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
            var walkDto = mapper.Map<WalkDto>(walkDomainModel);
            return Ok(ApiResponse.Lists(walkDto));
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
            return Ok(ApiResponse.Lists(walkDto));
        }
        #endregion
    }
}

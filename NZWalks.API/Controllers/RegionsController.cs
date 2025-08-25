using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using NZWalks.API.Models.Responses;
using Microsoft.Extensions.Logging;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        #region Constructor
        public RegionsController(NZWalksDbContext dbContext, IRegionRepository  regionRepository, IMapper mapper, ILogger<RegionsController> logger)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }
        #endregion

        #region GetAllRegions
        [HttpGet]
        public async Task<IActionResult> GetAllRegions([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            logger.LogInformation("GetAllRegions called with filterOn={FilterOn}, filterQuery={FilterQuery}, sortBy={SortBy}, isAscending={IsAscending}, pageNumber={PageNumber}, pageSize={PageSize}", filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            var (regionsDomain, totalCount) = await regionRepository.GetRegionsAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);
            var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);

            var response = ApiResponse.WithPagination(regionsDto, pageNumber, pageSize, totalCount);
            return Ok(response);
        }
        #endregion

        #region GetRegionById
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
        {
            logger.LogInformation("GetRegionById called with id={Id}", id);
            var regionDomain = await regionRepository.GetByIdAsync(id);
            if (regionDomain == null)
            {
                logger.LogWarning("Region with id {Id} not found", id);
                return NotFound();
            }
            var regionDto = mapper.Map<RegionDto>(regionDomain);
            return Ok(ApiResponse.Lists(regionDto));
        }
        #endregion

        #region CreateRegion
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            logger.LogInformation("CreateRegion called");
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            logger.LogInformation("Region created with id {Id}", regionDomainModel.Id);
            return CreatedAtAction(nameof(GetRegionById), new { id = regionDomainModel.Id }, ApiResponse.Lists(regionDto));
        }
        #endregion

        #region UpdateRegion
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            logger.LogInformation("UpdateRegion called for id {Id}", id);
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);
            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
            if(regionDomainModel == null)
            {
                logger.LogWarning("Region with id {Id} not found for update", id);
                return NotFound(new { Message = $"Region with Id {id} not found!." });
            }
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            return Ok(ApiResponse.Lists(regionDto));
        }
        #endregion

        #region DeleteRegion
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            logger.LogInformation("DeleteRegion called for id {Id}", id);
            var regionDomainModel = await regionRepository.DeleteAsync(id);
            if (regionDomainModel == null)
            {
                logger.LogWarning("Region with id {Id} not found for delete", id);
                return NotFound(new { Message = $"Region with Id {id} not found!." });
            }
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            return Ok(ApiResponse.Lists(regionDto));
        }
        #endregion
    }
}

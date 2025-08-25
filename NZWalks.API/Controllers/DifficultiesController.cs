using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using NZWalks.API.Models.Responses;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DifficultiesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IDifficultyRepository difficultyRepository;
        private readonly ILogger<DifficultiesController> logger;

        #region Constructor
        public DifficultiesController(IMapper mapper,IDifficultyRepository difficultyRepository, ILogger<DifficultiesController> logger)
        {
            this.mapper = mapper;
            this.difficultyRepository = difficultyRepository;
            this.logger = logger;
        }
        #endregion

        #region CreateDifficulty
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateDifficulty([FromBody] AddDifficultyRequestDto addDifficultyRequestDto)
        {
            logger.LogInformation("CreateDifficulty called");
            var difficultyDomainModel = mapper.Map<Difficulty>(addDifficultyRequestDto);
            difficultyDomainModel = await difficultyRepository.CreateAsync(difficultyDomainModel);
            var difficultyDto = mapper.Map<DifficultyDto>(difficultyDomainModel);
            logger.LogInformation("Difficulty created with id {Id}", difficultyDto.Id);
            return CreatedAtAction(nameof(GetDifficultyById), new { id = difficultyDto.Id }, ApiResponse.Lists(difficultyDto));
        }
        #endregion

        #region GetAllDifficulties
        [HttpGet]
        public async Task<IActionResult> GetAllDifficulties([FromQuery] string? filterOn,[FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            logger.LogInformation("GetAllDifficulties called with filterOn={FilterOn}, filterQuery={FilterQuery}, sortBy={SortBy}, isAscending={IsAscending}, pageNumber={PageNumber}, pageSize={PageSize}", filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            var (difficultiesDomainModel, totalCount) = await difficultyRepository.GetDifficultiesAsync(filterOn,filterQuery,sortBy,isAscending ?? true,pageNumber,pageSize);
            var difficultyDto = mapper.Map<List<DifficultyDto>>(difficultiesDomainModel);

            var response = ApiResponse.WithPagination(difficultyDto, pageNumber, pageSize, totalCount);
            return Ok(response);
        }
        #endregion

        #region GetDifficultyById
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetDifficultyById([FromRoute] Guid id)
        {
            logger.LogInformation("GetDifficultyById called for id {Id}", id);
            var difficultyDomainModel = await difficultyRepository.GetByIdAsync(id);
            if(difficultyDomainModel == null)
            {
                logger.LogWarning("Difficulty with id {Id} not found", id);
                return NotFound();
            }
            var difficultyDto = mapper.Map<DifficultyDto> (difficultyDomainModel);
            return Ok(ApiResponse.Lists(difficultyDto));
        }
        #endregion

        #region UpdateDifficulty
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateDifficulty([FromRoute] Guid id,[FromBody] UpdateDifficultyRequestDto updateDifficultyRequestDto)
        {
            logger.LogInformation("UpdateDifficulty called for id {Id}", id);
            var difficultyDomainModel = mapper.Map<Difficulty>(updateDifficultyRequestDto);
            difficultyDomainModel = await difficultyRepository.UpdateAsync(id, difficultyDomainModel);
            if (difficultyDomainModel == null)
            {
                logger.LogWarning("Difficulty with id {Id} not found for update", id);
                return NotFound();
            }
            var difficultyDto = mapper.Map<DifficultyDto>(difficultyDomainModel);
            return Ok(ApiResponse.Lists(difficultyDto));
        }
        #endregion

        #region DeleteDifficulty
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteDifficulty([FromRoute] Guid id)
        {
            logger.LogInformation("DeleteDifficulty called for id {Id}", id);
            var difficultyDomainModel = await difficultyRepository.DeleteAsync(id);
            difficultyDomainModel = mapper.Map<Difficulty>(difficultyDomainModel);
            if(difficultyDomainModel == null)
            {
                logger.LogWarning("Difficulty with id {Id} not found for delete", id);
                return NotFound();
            }
            var difficultyDto = mapper.Map<DifficultyDto>(difficultyDomainModel);
            return Ok(ApiResponse.Lists(difficultyDto));
        }
        #endregion
    }
}

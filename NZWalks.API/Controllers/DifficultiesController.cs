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
    public class DifficultiesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IDifficultyRepository difficultyRepository;

        #region Constructor
        public DifficultiesController(IMapper mapper,IDifficultyRepository difficultyRepository)
        {
            this.mapper = mapper;
            this.difficultyRepository = difficultyRepository;
        }
        #endregion

        #region CreateDifficulty
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateDifficulty([FromBody] AddDifficultyRequestDto addDifficultyRequestDto)
        {
            var difficultyDomainModel = mapper.Map<Difficulty>(addDifficultyRequestDto);
            difficultyDomainModel = await difficultyRepository.CreateAsync(difficultyDomainModel);
            var difficultyDto = mapper.Map<DifficultyDto>(difficultyDomainModel);
            return CreatedAtAction(nameof(GetDifficultyById), new { id = difficultyDto.Id }, ApiResponse.Lists(difficultyDto));
        }
        #endregion

        #region GetAllDifficulties
        [HttpGet]
        public async Task<IActionResult> GetAllDifficulties([FromQuery] string? filterOn,[FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
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
            var difficultyDomainModel = await difficultyRepository.GetByIdAsync(id);
            if(difficultyDomainModel == null)
            {
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
            var difficultyDomainModel = mapper.Map<Difficulty>(updateDifficultyRequestDto);
            difficultyDomainModel = await difficultyRepository.UpdateAsync(id, difficultyDomainModel);
            if (difficultyDomainModel == null)
            {
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
            var difficultyDomainModel = await difficultyRepository.DeleteAsync(id);
            difficultyDomainModel = mapper.Map<Difficulty>(difficultyDomainModel);
            if(difficultyDomainModel == null)
            {
                return NotFound();
            }
            var difficultyDto = mapper.Map<DifficultyDto>(difficultyDomainModel);
            return Ok(ApiResponse.Lists(difficultyDto));
        }
        #endregion
    }
}

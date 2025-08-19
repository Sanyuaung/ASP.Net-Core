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
            return CreatedAtAction(nameof(GetDifficultyById), new { id = difficultyDto.Id }, difficultyDto);
        }
        #endregion

        #region GetAllDifficulties
        [HttpGet]
        public async Task<IActionResult> GetAllDifficulties()
        {
            var difficultiesDomainModel = await difficultyRepository.GetAllAsync();
            var difficultyDto = mapper.Map<List<DifficultyDto>>(difficultiesDomainModel);
            return Ok(difficultyDto);
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
            return Ok(difficultyDto);
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
            return Ok(difficultyDto);
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
            return Ok(difficultyDto);
        }
        #endregion
    }
}

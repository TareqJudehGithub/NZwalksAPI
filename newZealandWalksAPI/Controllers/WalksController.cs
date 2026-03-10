using Microsoft.AspNetCore.Mvc;

using newZealandWalksAPI.Repositories;
using newZealandWalksAPI.Models.Domain;
using newZealandWalksAPI.Models.DTO;
using AutoMapper;
using newZealandWalksAPI.CustomActionFilters;
using Microsoft.AspNetCore.Authorization;

namespace newZealandWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class WalksController : ControllerBase
    {
        #region Fields
        private readonly IWalkRepository _walkRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            _walkRepository = walkRepository;
            _mapper = mapper;
        }
        #endregion

        #region Endpoints

        // GET all walks
        // GET: /api/walks?filterOn=Name&filterQuery=Track?sortBy=Name?isAscending=true?pageNumber=1?pageSize=10    
        [HttpGet]
        [Authorize(Roles = "Reader, Writer")]
        public async Task<IActionResult> GetAllWalks(
            [FromQuery] string? filterOn,
            [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy,
            [FromQuery] bool isAscending = true,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10
            )
        {
            var walksModel = await _walkRepository.GetAllWalksAsync(
                filterOn, filterQuery,
                sortBy, isAscending,
                pageNumber,
                pageSize
                );

            // Catch exception using global Exception Handler
            // throw new Exception(message: "This is a new exception");

            // Map to DTO
            var walksDTO = _mapper.Map<List<WalkDTO>>(source: walksModel);
            return Ok(walksModel);
        }
        [HttpGet]
        [Route("{id:guid}")]
        [Authorize(Roles = "Reader, Writer")]
        public async Task<IActionResult> GetWalkById([FromRoute] Guid id)
        {
            var walkModel = await _walkRepository.GetWalkByIdAsync(id);

            var walkDTO = _mapper.Map<WalkDTO>(source: walkModel);

            return Ok(walkModel);
        }

        // Create Walk
        // Post: /api/walks/{id}
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDTO addWalkRequestDTO)
        {
            // Map to Model
            var walkModel = _mapper.Map<Walk>(source: addWalkRequestDTO);

            // Invoke CreateWalkAsync repository method
            await _walkRepository.CreateWalkAsync(walkModel);

            // Map back to DTO
            var walkDTO = _mapper.Map<WalkDTO>(source: walkModel);

            // Confirm creating record by returning a 201 response
            return CreatedAtAction(
                actionName: nameof(GetWalkById),
                routeValues: new { id = walkDTO.Id },
                value: walkDTO);

            // Or just return 200 response
            // return Ok(walkDTO);          
        }

        // Update Walk
        //Put: /api/Walks/{id}
        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, [FromBody] UpdateWalkRequestDTO updateWalkRequestDTO)
        {
            // Map back to domain model and invoke the update repository method
            var walkModel = _mapper.Map<Walk>(source: updateWalkRequestDTO);
            walkModel = await _walkRepository.UpdateWalkAsync(id, walkModel);

            if (walkModel == null)
            {
                return NotFound();
            }
            // Map to DTO
            var walkDTO = _mapper.Map<WalkDTO>(source: walkModel);

            return Ok(walkDTO);
        }

        // Delete a walk
        // /api/walks/{id}
        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteWalk([FromRoute] Guid id)
        {
            var existingWalk = await _walkRepository.DeleteWalk(id);

            if (existingWalk == null)
            {
                return NotFound();
            }

            return Ok();
        }
        #endregion
    }
}

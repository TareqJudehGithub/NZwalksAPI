using Microsoft.AspNetCore.Mvc;

using newZealandWalksAPI.Repositories;
using newZealandWalksAPI.Models.Domain;
using newZealandWalksAPI.Models.DTO;
using AutoMapper;

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

        #region Fields
        // Create Walk
        // Post: /api/walks
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDTO addWalkRequestDTO)
        {
            // Map to Model
            var walkModel = _mapper.Map<Walk>(source: addWalkRequestDTO);

            // Invoke CreateWalkAsync repository method
            await _walkRepository.CreateWalkAsync(walkModel);

            // Map back to DTO
            var walkDTO = _mapper.Map<WalkDTO>(source: walkModel);

            return Ok(walkDTO);

            // Confirm creating record by returning a 201 response
            //  return CreatedAtAction(actionName: Create, value: )

            //  return CreatedAtAction(
            //actionName: nameof(GetRegionById),
            //routeValues: new { id = regionDTO.Id },
            //value: regionDTO);
        }
        #endregion
    }
}

using Microsoft.AspNetCore.Mvc;

using newZealandWalksAPI.Data;
using newZealandWalksAPI.Models.DTO;
using newZealandWalksAPI.Models.Domain;
using newZealandWalksAPI.Repositories;
using AutoMapper;

namespace newZealandWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    #region Fields
    {
        private readonly NZWalksDbContext _nZWalksDbContext;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public RegionsController(
            NZWalksDbContext nZWalksDbContext,
            IRegionRepository regionRepository,
            IMapper mapper
            )
        {
            _nZWalksDbContext = nZWalksDbContext;
            _regionRepository = regionRepository;
            _mapper = mapper;
        }
        #endregion
        #region Endpoints
        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            // Get data from the database
            var regionsModel = await _regionRepository.GetAllRegionsAsync();

            // Map domain models to DTOs
            var regionsDTO = _mapper.Map<List<RegionDTO>>(source: regionsModel);

            return Ok(regionsDTO);
        }

        // Get region by id
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
        {
            // Get data from DB
            var regionModel = await _regionRepository.GetRegionByIDAsync(id);

            if (regionModel == null)
            {
                return NotFound();
            }
            // Convert to DTOs
            var regionDTO = _mapper.Map<RegionDTO>(source: regionModel);

            return Ok(regionDTO);
        }

        // Add new region
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] AddRegionRequestDTO addRegionRequestDTO
            )
        {
            // Map DTO to domain model
            var regionModel = _mapper.Map<Region>(source: addRegionRequestDTO);

            // Add newly created record to the DB and save
            regionModel = await _regionRepository.CreateRegionAsync(regionModel);

            // Map Domain model back to DTO
            var regionDTO = _mapper.Map<RegionDTO>(source: regionModel);

            return CreatedAtAction(
                actionName: nameof(GetRegionById),
                routeValues: new { id = regionDTO.Id },
                value: regionDTO);
        }

        // Update an existing region
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            // Map back to domain model
            var regionModel = _mapper.Map<Region>(updateRegionRequestDTO);
            regionModel = await _regionRepository.UpdateRegionAsync(id, regionModel);

            // Check if region does exist
            if (regionModel == null)
            {
                return NotFound();
            }
            // Map to DTO
            var regionDTO = _mapper.Map<RegionDTO>(regionModel);

            return Ok(regionDTO);
        }

        // Delete a region
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _regionRepository.DeleteRegionAsync(id);

            return Ok();
        }
    }
    #endregion
}




// Manual mappings  code

// GetAllRegions
// Map domain models to DTOs
//var dtoModel = new List<RegionDTO>();
//foreach (var region in regionModel)
//{
//    dtoModel.Add(new RegionDTO
//    {
//        Id = region.Id,
//        Code = region.Code,
//        Name = region.Name,
//        RegionImageUrl = region.RegionImageUrl
//    });
//}

//GetRegionById
//var regionDTO = new RegionDTO
//{
//    Id = regionModel.Id,
//    Code = regionModel.Code,
//    Name = regionModel.Name,
//    RegionImageUrl = regionModel.RegionImageUrl
//};
// Return DTOs

// AddRegion

// domain model => DTO
//var regionModel = new Region
//{
//    Code = addRegionRequestDTO.Code,
//    Name = addRegionRequestDTO.Name,
//    RegionImageUrl = addRegionRequestDTO.RegionImageUrl
//};

// DTO => domain model
//var regionDTO = new RegionDTO
//{
//    Id = regionModel.Id,
//    Code = regionModel.Code,
//    Name = regionModel.Name,
//    RegionImageUrl = regionModel.RegionImageUrl
//};

// Delete
//var regionModel = new Region
//{
//    Code = updateRegionRequestDTO.Code,
//    Name = updateRegionRequestDTO.Name,
//    RegionImageUrl = updateRegionRequestDTO.RegionImageUrl
//};

//var regionDTO = new RegionDTO
//{
//    Code = regionModel.Code,
//    Name = regionModel.Name,
//    RegionImageUrl = regionModel.RegionImageUrl
//};
using Microsoft.AspNetCore.Mvc;

using newZealandWalksAPI.Data;
using newZealandWalksAPI.Models.DTO;
using newZealandWalksAPI.Models.Domain;

namespace newZealandWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    #region Fields
    {
        private readonly NZWalksDbContext _nZWalksDbContext;
        #endregion

        #region Constructors
        public RegionsController(NZWalksDbContext nZWalksDbContext)
        {
            _nZWalksDbContext = nZWalksDbContext;
        }
        #endregion
        #region Endpoints
        [HttpGet]
        public IActionResult GetAllRegions()
        {
            // Get data from the database
            var regionModel = _nZWalksDbContext.Regions.ToList();

            // Map domain models to DTOs
            var dtoModel = new List<RegionDTO>();
            foreach (var region in regionModel)
            {
                dtoModel.Add(new RegionDTO
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl
                });
            }

            // return DTOs
            return Ok(dtoModel);
        }

        // Get region by id
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetRegionById([FromRoute] Guid id)
        {
            // Get data from DB
            var regionModel = _nZWalksDbContext.Regions
                .FirstOrDefault(q => q.Id == id);
            if (regionModel == null)
            {
                return NotFound();
            }
            // Convert to DTOs
            var regionDTO = new RegionDTO
            {
                Id = regionModel.Id,
                Code = regionModel.Code,
                Name = regionModel.Name,
                RegionImageUrl = regionModel.RegionImageUrl
            };
            // Return DTOs
            return Ok(regionDTO);
        }

        // Add new region
        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            // Map DTO to domain model
            var regionModel = new Region
            {
                Code = addRegionRequestDTO.Code,
                Name = addRegionRequestDTO.Name,
                RegionImageUrl = addRegionRequestDTO.RegionImageUrl
            };

            // Add newly created record to the DB and save
            _nZWalksDbContext.Regions.Add(regionModel);
            _nZWalksDbContext.SaveChanges();


            // Map Domain model back to DTO
            var regionDTO = new RegionDTO
            {
                Id = regionModel.Id,
                Code = regionModel.Code,
                Name = regionModel.Name,
                RegionImageUrl = regionModel.RegionImageUrl
            };

            return CreatedAtAction(
                actionName: nameof(GetRegionById),
                routeValues: new { id = regionDTO.Id },
                value: regionDTO);
        }

        // Update an existing region
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            // Check if region does exist
            var regionModel = _nZWalksDbContext.Regions.FirstOrDefault(q => q.Id == id);

            if (regionModel == null)
            {
                return NotFound();
            }
            // Map back to domain model
            regionModel.Code = updateRegionRequestDTO.Code;
            regionModel.Name = updateRegionRequestDTO.Name;
            regionModel.RegionImageUrl = updateRegionRequestDTO.RegionImageUrl;
            _nZWalksDbContext.SaveChanges();

            // Map to DTO
            var regionDTO = new RegionDTO
            {
                Code = regionModel.Code,
                Name = regionModel.Name,
                RegionImageUrl = regionModel.RegionImageUrl
            };
            return Ok(regionDTO);
        }

        // Delete a region
        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var regionModel = _nZWalksDbContext.Regions
                .FirstOrDefault(q => q.Id == id);

            if (regionModel == null)
            {
                return NotFound();
            }
            _nZWalksDbContext.Regions.Remove(regionModel);
            _nZWalksDbContext.SaveChanges();

            return Ok();
        }
    }
    #endregion
}


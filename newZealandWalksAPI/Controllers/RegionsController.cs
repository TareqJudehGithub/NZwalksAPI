using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using newZealandWalksAPI.Data;
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
            var model = _nZWalksDbContext.Regions.ToList();
            return Ok(model);
        }
        // Get region by id
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetRegionById([FromRoute] Guid id)
        {
            var region = _nZWalksDbContext.Regions
                .FirstOrDefault(q => q.Id == id);

            if (region == null)
            {
                return NotFound();
            }
            return Ok(region);
        }
    }
    #endregion
}


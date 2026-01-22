using Microsoft.EntityFrameworkCore;
using newZealandWalksAPI.Data;
using newZealandWalksAPI.Models.Domain;
using newZealandWalksAPI.Models.DTO;

namespace newZealandWalksAPI.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        #region Fields
        private readonly NZWalksDbContext _nZWalksDbContext;
        #endregion

        #region Constructors
        public SQLRegionRepository(NZWalksDbContext nZWalksDbContext)
        {
            _nZWalksDbContext = nZWalksDbContext;
        }
        #endregion

        #region Methods
        public async Task<List<Region>> GetAllRegionsAsync()
        {
            var domainModel = await _nZWalksDbContext.Regions.ToListAsync();
            return domainModel;
        }
        public async Task<Region?> GetRegionByIDAsync(Guid id)
        {
            // Get region from DB
            var regionModel = await _nZWalksDbContext.Regions
                .FirstOrDefaultAsync(q => q.Id == id);

            return regionModel;
        }
        public async Task<Region> CreateRegionAsync(Region region)
        {
            await _nZWalksDbContext.Regions.AddAsync(region);
            await _nZWalksDbContext.SaveChangesAsync();

            return region;
        }
        public async Task<Region?> UpdateRegionAsync(Guid id, Region region)
        {
            // Get region from DB
            var existingRegionModel = await _nZWalksDbContext.Regions
                .FirstOrDefaultAsync(q => q.Id == id);

            if (existingRegionModel == null)
            {
                return null;
            }
            // Update properties and save
            existingRegionModel.Code = region.Code;
            existingRegionModel.Name = region.Name;
            existingRegionModel.RegionImageUrl = region.RegionImageUrl;

            await _nZWalksDbContext.SaveChangesAsync();

            return existingRegionModel;
        }
        public async Task<Region?> DeleteRegionAsync(Guid id)
        {
            // Get region from DB
            var existingRegionModel = await _nZWalksDbContext.Regions
                .FirstOrDefaultAsync(q => q.Id == id);

            if (existingRegionModel == null)
            {
                return null;
            }
            _nZWalksDbContext.Regions.Remove(existingRegionModel);
            await _nZWalksDbContext.SaveChangesAsync();

            return existingRegionModel;
        }

        #endregion
    }
}

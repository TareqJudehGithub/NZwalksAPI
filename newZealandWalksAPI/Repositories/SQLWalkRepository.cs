using Microsoft.EntityFrameworkCore;
using newZealandWalksAPI.Data;
using newZealandWalksAPI.Models.Domain;

namespace newZealandWalksAPI.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        #region Fields
        private readonly NZWalksDbContext _nZWalksDbContext;
        #endregion

        #region Constructors
        public SQLWalkRepository(NZWalksDbContext nZWalksDbContext)
        {
            _nZWalksDbContext = nZWalksDbContext;
        }
        #endregion
        #region Methods
        public async Task<List<Walk>> GetAllWalksAsync()
        {
            var walksModel = await _nZWalksDbContext.Walks.ToListAsync();
            return walksModel;
        }
        public async Task<Walk> CreateWalkAsync(Walk walk)
        {
            await _nZWalksDbContext.Walks.AddAsync(walk);
            await _nZWalksDbContext.SaveChangesAsync();

            return walk;
        }
        public async Task<Walk?> GetWalkByIdAsync(Guid id)
        {
            // Get walk by id
            var walkModel = await _nZWalksDbContext.Walks.FirstOrDefaultAsync(q => q.Id == id);

            if (walkModel != null)
            {
                return walkModel;
            }
            return null;
        }
        public async Task<Walk?> UpdateWalkAsync(Guid id, Walk walk)
        {
            // Get walk by id
            var walkModel = await _nZWalksDbContext.Walks.FirstOrDefaultAsync(q => q.Id == id);

            if (walkModel == null)
            {
                return null;
            }
            // Update properties and save
            walkModel.Name = walk.Name;
            walkModel.Description = walk.Description;
            walkModel.LengthInKm = walk.LengthInKm;
            walkModel.WalkImageUrl = walk.WalkImageUrl;
            walkModel.DifficultyId = walk.DifficultyId;
            walkModel.RegionId = walk.RegionId;

            await _nZWalksDbContext.SaveChangesAsync();

            return walkModel;
        }
        public async Task<Walk?> DeleteWalk(Guid id)
        {
            // Get walk by id
            var walkModel = await _nZWalksDbContext.Walks.FirstOrDefaultAsync(q => q.Id == id);

            if (walkModel == null)
            {
                return null;
            }
            _nZWalksDbContext.Walks.Remove(walkModel);
            await _nZWalksDbContext.SaveChangesAsync();

            return walkModel;
        }

        #endregion
    }
}


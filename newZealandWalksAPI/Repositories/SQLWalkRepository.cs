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
        public async Task<Walk> CreateWalkAsync(Walk walk)
        {
            await _nZWalksDbContext.Walks.AddAsync(walk);
            await _nZWalksDbContext.SaveChangesAsync();

            return walk;
        }
    }
}

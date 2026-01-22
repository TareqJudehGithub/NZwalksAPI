using newZealandWalksAPI.Models.Domain;

namespace newZealandWalksAPI.Repositories
{
    public interface IWalkRepository
    {
        Task<List<Walk>> GetAllWalksAsync();
        Task<Walk?> GetWalkByIdAsync(Guid id);
        Task<Walk> CreateWalkAsync(Walk walk);
        Task<Walk?> UpdateWalkAsync(Guid id, Walk walk);
        Task<Walk?> DeleteWalk(Guid id);
    }
}

using newZealandWalksAPI.Models.Domain;

namespace newZealandWalksAPI.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateWalkAsync(Walk walk);
    }
}

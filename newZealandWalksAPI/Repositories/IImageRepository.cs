using newZealandWalksAPI.Models.Domain;

namespace newZealandWalksAPI.Repositories
{
    public interface IImageRepository
    {
        Task<Image> ImageUpload(Image image);
    }
}

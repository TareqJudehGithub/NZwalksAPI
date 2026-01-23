using AutoMapper;

using newZealandWalksAPI.Models.Domain;
using newZealandWalksAPI.Models.DTO;

namespace newZealandWalksAPI.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        #region Constructor        
        public AutoMapperProfiles()
        {
            // Regions
            CreateMap<Region, RegionDTO>();
            CreateMap<Region, AddRegionRequestDTO>().ReverseMap();
            CreateMap<Region, UpdateRegionRequestDTO>().ReverseMap();

            // Walks
            CreateMap<Walk, WalkDTO>();
            CreateMap<Walk, AddWalkRequestDTO>().ReverseMap();
            CreateMap<Walk, UpdateWalkRequestDTO>().ReverseMap();

            // Difficulty
            CreateMap<Difficulty, DifficultyDTO>().ReverseMap();
        }
        #endregion
    }
}

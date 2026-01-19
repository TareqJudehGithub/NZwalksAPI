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
            CreateMap<Region, RegionDTO>();
            CreateMap<Region, AddRegionRequestDTO>().ReverseMap();
            CreateMap<Region, UpdateRegionRequestDTO>().ReverseMap();
        }
        #endregion
    }
}

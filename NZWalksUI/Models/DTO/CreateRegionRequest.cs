using System.ComponentModel.DataAnnotations;

namespace NZWalksUI.Models.DTO
{
    public class CreateRegionRequest
    {

        //[Display(Name = "Region Area Code")]
        public string Code { get; set; }


        //[Display(Name = "Region Name")]
        public string Name { get; set; }


        //[Display(Name = "Region Image")]
        public string RegionImageUrl { get; set; }
    }
}

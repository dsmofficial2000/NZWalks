using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class updateRegionDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Enter Minimum 3 character")]
        [MaxLength(3, ErrorMessage = "Enter Maximum 3 character")]        
        public string Code { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Enter Minimum 5 character")]
        [MaxLength(20, ErrorMessage = "Enter Maximum 20 character")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}

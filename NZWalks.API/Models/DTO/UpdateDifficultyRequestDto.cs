using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class UpdateDifficultyRequestDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be a minimum of 100 characters")]
        public string Name { get; set; }
    }
}

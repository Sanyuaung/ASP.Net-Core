namespace NZWalks.API.Models.DTO
{
    public class UpdateRegionRequest
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}

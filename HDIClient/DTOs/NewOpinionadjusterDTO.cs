using System.ComponentModel.DataAnnotations;

namespace HDIClient.DTOs
{
    public class NewOpinionadjusterDTO
    {
        public DateTime? CreationDate { get; set; }

        public string? Description { get; set; }

        public string? IdAccident { get; set; } = null!;

        public string? IdOpinionAdjuster { get; set; } = null!;
    }
}

namespace HDIClient.DTOs
{
    public class NewReport
    {
        public string? Location { get; set; }

        public string? Longitude { get; set; }

        public string? Latitude { get; set; }
        public string? ReportStatus { get; set; }

        public DateTime? AccidentDate { get; set; }

        public string IdDriverClient { get; set; } = null!;

        public string IdVehicleClient { get; set; } = null!;
        public List<string> Images { get; set; }

        public List<InvolvedDTO> Involveds { get; set; } = new List<InvolvedDTO>();
    }
}

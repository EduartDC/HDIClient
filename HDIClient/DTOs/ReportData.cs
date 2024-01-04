namespace HDIClient.DTOs
{
    public class ReportData
    {
        public List<InvolvedData> InvolvedDataList { get; set; }
        public List<string> ImageByteList { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Address { get; set; }
        public string Idcar { get; set; }
    }
}
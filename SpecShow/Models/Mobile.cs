using System.ComponentModel.DataAnnotations;

namespace SpecShow.Models
{
    public class Mobile
    {
        [Key]
        public int MobileID { get; set; }
        public string? MobileName { get; set; }
		public string? Model { get; set; }
		public int Price { get; set; }
        public string? Brand { get; set; }
		[DataType(DataType.Url)]
        public string? ImageUrl { get; set; }
		[DataType(DataType.Url)]
        public string? AmazonUrl { get; set; }
		[DataType(DataType.Url)]
        public string? FlipkartUrl { get; set; }
		public string? ScreenSize { get; set; }
		public string? FrontCamera { get; set; }
		public string? BackCameras { get; set; }
		public string? OS { get; set; }
		public string? Sim { get; set; }
		public string? Sensors { get; set; }
		public string? BatteryCapacity { get; set; }
		public string? Colors { get; set; }
		public string? Variants { get; set; }
		[DataType(DataType.DateTime)]
        public DateTime ReleaseDate { get; set; }
        public double Weight { get; set; }
        public string? Material { get; set; }
		public string? ResistanceCertificate { get; set; }
		public string? ScreenType { get; set; }
		public string? AspectRatio { get; set; }
		public string? Resolution { get; set; }
		public string? OtherFeatures { get; set; }
		public string? Processor { get; set; }
		public string? Nanometer { get; set; }
		public string? GPU { get; set; }
		public int AntutuScores { get; set; }
        public string? Bluetooth { get; set; }
		public double ChargingCapacity { get; set; }
        public double Rating { get; set; }
    }
}

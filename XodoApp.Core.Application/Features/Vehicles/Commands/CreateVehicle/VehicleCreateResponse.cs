namespace XodoApp.Core.Application.Features.Vehicles.Commands.CreateVehicle
{
    public class VehicleCreateResponse
    {
        public int Id { get; set; }
        public string? VIN { get; set; }
        public string CarMake { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string? Color { get; set; }
        public decimal? Price { get; set; }
        public string? EngineType { get; set; }
        public string TransmissionType { get; set; }
        public int? Mileage { get; set; }
        public string Description { get; set; }
        public int DealershipId { get; set; }
        public string VehicleType { get; set; }
    }
}

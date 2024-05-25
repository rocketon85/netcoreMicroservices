namespace NetCoreMicroservices.Cars.Domains
{
    public class CarDomain : VehicleDomain
    {
        public int Doors { get; set; }
        public bool HasGraffiti { get; set; }

        public int AirBags { get; set; }
    }
}

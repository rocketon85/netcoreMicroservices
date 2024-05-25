using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NetCoreMicroservices.Cars.Domains
{
    public class VehicleDomain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double MaxSpeed { get; set; }

        [ForeignKey("Fuel")]
        public int FuelId { get; set; }
        //public FuelDomain Fuel { get; set; }

        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        //public BrandDomain Brand { get; set; }

        [ForeignKey("Model")]
        public int ModelId { get; set; }
        //public ModelDomain Model { get; set; }
    }
}

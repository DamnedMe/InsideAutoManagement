using InsideAutoManagement.Enums;

namespace InsideAutoManagement.DTO
{
    public record CarDTO
    {
        public long Id { get; set; } = 0;

        /// <summary>
        /// Marca
        /// </summary>
        /// <value></value>
        public BrandId BrandId { get; set; }

        /// <summary>
        /// Modello
        /// </summary>
        /// <value></value>
        public string Model { get; set; } = null!;

        /// <summary>
        /// Mese prima immatricolazione
        /// </summary>
        /// <value></value>
        public int? Month { get; set; }

        /// <summary>
        /// Anno immatricolazione
        /// </summary>
        /// <value></value>
        public int Year { get; set; }

        /// <summary>
        /// Cilindrata
        /// </summary>
        /// <value></value>
        public long Capacity { get; set; }

        /// <summary>
        /// Carburante
        /// </summary>
        /// <value></value>
        public FuelTypeId FuelId { get; set; }

        /// <summary>
        /// Targa
        /// </summary>
        /// <value></value>
        public string Plate { get; set; } = null!;

        /// <summary>
        /// Kilometri auto
        /// </summary>
        /// <value></value>
        public string Km { get; set; } = null!;

        /// <summary>
        /// Descrizione
        /// </summary>
        /// <value></value>
        public string? Description { get; set; }

        /// <summary>
        /// Tipo categoria (es. berlina, stationwagon, coupe...)
        /// </summary>
        /// <value></value>
        public CategoryTypeId CategoryId { get; set; }

        /// <summary>
        /// Tipo di cambio (automatico, manuale)
        /// </summary>
        /// <value></value>
        public GearShiftTypeId GearShiftId { get; set; }

        /// <summary>
        /// Stato di vendita dell'auto
        /// </summary>
        /// <value></value>
        public SaleStatusId SaleStatusId { get; set; }

        /// <summary>
        /// Prezzo di acquisto dell'auto
        /// </summary>
        /// <value></value>
        public double StartPrice { get; set; }

        /// <summary>
        /// Concessionario
        /// </summary>
        /// <value></value>
        public CarDealerDTO CarDealer { get; set; } = null!;
    }
}
namespace InsideAutoManagement.Model
{
    public class CarDealer
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Nome concessionario
        /// </summary>
        /// <value></value>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Partita iva concessionario
        /// </summary>
        /// <value></value>
        public string PIVA { get; set; } = null!;
        
        /// <summary>
        /// Numero di telefono
        /// </summary>
        /// <value></value>
        public string? Phone { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        /// <value></value>
        public string? Email { get; set; }

        /// <summary>
        /// Citta
        /// </summary>
        /// <value></value>
        public string? City { get;set; }

        /// <summary>
        /// Country
        /// </summary>
        /// <value></value>
        public string? Country { get;set; }

        /// <summary>
        /// Indirizzo
        /// </summary>
        /// <value></value>
        public string? Adress { get;set; }

        /// <summary>
        /// Orari di apertura
        /// </summary>
        /// <value></value>
        public List<OpeningHoursShift>? OpeningHoursShifts { get;set; }

        /// <summary>
        /// Documentazione
        /// </summary>
        /// <value></value>
        public List<Document>? Documents { get; set; }
    }
}
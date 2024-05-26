namespace InsideAutoManagement.Models
{
    public class FolderCategory
    {
        public long Id { get;set; }

        /// <summary>
        /// Nome cartella
        /// </summary>
        /// <value></value>
        public string FolderName { get; set; } = null!;

        /// <summary>
        /// Breve Descrizione
        /// </summary>
        /// <value></value>
        public string? Description { get; set; }

        /// <summary>
        /// Concessionario
        /// </summary>
        /// <value></value>
        public CarDealer CarDealer { get; set; } = null!;
    }
}
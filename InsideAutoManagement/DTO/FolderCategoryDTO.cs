namespace InsideAutoManagement.DTO
{
    public class FolderCategoryDTO
    {
        public Guid Id { get;set; }

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
        public CarDealerDTO CarDealer { get; set; } = null!;
    }
}
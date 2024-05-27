using InsideAutoManagement.Enums;

namespace InsideAutoManagement.DTO
{
    /// <summary>
    /// Documentazione (foto e documenti)
    /// </summary>
    public record DocumentDTO
    {
        public Guid Id { get; set; }

        /// <summary>
        /// File Name
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// DocumentType: Image / Document
        /// </summary>
        public DocumentTypeId DocumentTypeId { get; set; }

        /// <summary>
        /// Path immagine
        /// </summary>
        /// <value></value>
        public string Path { get; set; } = null!;

        /// <summary>
        /// Informazioni Cartella
        /// </summary>
        /// <value></value>
        public FolderCategoryDTO FolderCategory { get; set; } = null!;

        /// <summary>
        /// Descrizione documento
        /// </summary>
        /// <value></value>
        public string? Description { get; set; }

        /// <summary>
        /// order in wich show image or document, -1 = hide
        /// </summary>
        public int Ranking { get; set; } = -1;

        /// <summary>
        /// last update
        /// </summary>
        public DateTime? LastUpdate { get; set; }

        /// <summary>
        /// Concessionario
        /// </summary>
        /// <value></value>
        public CarDealerDTO CarDealer { get; set; } = null!;       
    }
}
using InsideAutoManagement.Enums;

namespace InsideAutoManagement.Models
{
    public class Document
    {
        public long Id { get; set; }
        
        public DocumentTypeId DocumentTypeId { get; set; }
       
        /// <summary>
        /// Path immagine
        /// </summary>
        /// <value></value>
        public string Path { get; set; }  = null!;

        /// <summary>
        /// Informazioni Cartella
        /// </summary>
        /// <value></value>
        public FolderCategory FolderCategory { get; set; } = null!;

        /// <summary>
        /// Descrizione documento
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
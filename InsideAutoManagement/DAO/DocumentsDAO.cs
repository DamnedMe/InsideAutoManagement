using InsideAutoManagement.Data;
using InsideAutoManagement.Enums;
using InsideAutoManagement.Model;
using InsideAutoManagement.RequestModel;
using InsideAutoManagement.ResponseModel;
using Microsoft.EntityFrameworkCore;

namespace InsideAutoManagement.DAO
{
    /// <summary>
    /// used for crud operations on Documents
    /// </summary>
    public class DocumentsDAO
    {
        private readonly InsideAutoManagementContext _context = null!;
        private CarDealer _carDealer = null!;

        public DocumentsDAO(InsideAutoManagementContext context, CarDealer carDealer)
        {
            _context = context;
            _carDealer = carDealer;
        }

        public async Task<IEnumerable<ResponseGetDocuments>> GetDocumentsResponse()
        {
            if (_context.Documents == null || _context.Documents.Count() == 0)
                return new List<ResponseGetDocuments>();

            IEnumerable<Document> documents = await GetDocuments();

            List<ResponseGetDocuments> responseDocuments = new List<ResponseGetDocuments>(documents.Count());

            for (int i = 0; i < documents.Count(); i++)
            {
                responseDocuments[i] = await GetDocumentResponse(documents.ElementAt(i).Id, documents.ElementAt(i))
                    ?? throw new InvalidOperationException();
            }

            return responseDocuments;
        }

        /// <summary>
        /// Gets all documents.
        /// </summary>
        /// <param name="carDealer"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Document>> GetDocuments(DocumentTypeId? documentTypeId = null)
        {
            if (_context.Documents == null || _context.Documents.Count() == 0)
                return new List<Document>();

            return await _context.Documents
                .Where(d => d.CarDealer == _carDealer
                    && (documentTypeId == null || d.DocumentTypeId == documentTypeId))
                .ToListAsync();
        }

        /// <summary>
        /// Gets a document (object and file).
        /// </summary>
        /// <param name="id"></param>
        /// <param name="carDealer"></param>
        /// <param name="documentTypeId"></param>
        /// <returns></returns>
        public async Task<ResponseGetDocuments?> GetDocumentResponse(Guid id, Document? document = null)
        {
            if (document == null)
                document = await GetDocument(id);

            if (document == null)
                return null;

            byte[]? documentBytes = await GetDocumentBytes(document.Id);

            if (documentBytes == null)
                return null;

            ResponseGetDocuments response = new ResponseGetDocuments
            {
                Document = document,
                DocumentBytes = documentBytes
            };

            return response;
        }

        /// <summary>
        /// Gets a document.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="carDealer"></param>
        /// <param name="documentTypeId"></param>
        /// <returns></returns>
        public async Task<Document?> GetDocument(Guid id)
        {
            if (_context.Documents == null || _context.Documents.Count() == 0)
                return null;

            var document = await _context.Documents
                .Where(d => d.CarDealer == _carDealer)
                .FirstOrDefaultAsync();

            if (document == null)
                return null;

            return document;
        }

        /// <summary>
        /// Edits the document.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task EditDocument(Guid id, Document document)
        {
            if (id != document.Id)
                throw new ArgumentException("Invalid document");

            if (_context.Documents == null || _context.Documents.Count() == 0)
                throw new InvalidOperationException("There are no documents");

            if (DocumentExists(document.Id) == false)
                throw new ArgumentException($"The input document plate {document.Id}-{document.Description} doesn't exist");

            try
            {
                _context.Entry(document).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Saves the documents.
        /// </summary>
        /// <param name="documents"></param>
        /// <param name="checkIfExisting">true: more slow but check if document already exists, false: faster but doesn't check</param>
        /// <returns></returns>
        public async Task SaveDocuments(IEnumerable<RequestSaveDocuments> documents, bool checkIfExisting = false)
        {
            try
            {
                foreach (RequestSaveDocuments document in documents)
                {
                    if (document.Document.Path == null)
                        document.Document.Path = GetDocumentPath(document.Document);
                    if (checkIfExisting)
                        await SaveDocument(document.Document);
                    await SaveDocumentBytesByPath(document.Document.Path, document.DocumentBytes);
                }

                if (checkIfExisting == false)
                    _context.Documents.AddRange(documents.Select(d => d.Document));

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Saves the document.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task SaveDocument(Document document)
        {
            if (document == null)
                throw new ArgumentNullException();

            var existingDocuments = await GetDocument(document.Id);

            if (existingDocuments != null)
            {
                Guid lastId = existingDocuments.Id;
                existingDocuments = document;
                existingDocuments.Id = lastId;

                _context.Entry(existingDocuments).State = EntityState.Modified;
            }
            else
                _context.Documents.Add(document);
        }

        /// <summary>
        /// Deletes the document based on the provided id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="carDealer"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task DeleteDocument(Guid id, CarDealer carDealer)
        {
            var document = await _context.Documents
                .Where(d => d.CarDealer == carDealer
                    && d.Id == id).FirstOrDefaultAsync();
            if (document == null)
                throw new InvalidOperationException($"there aren't documents with id = {id}");

            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes the document based on the provided path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task DeleteDocument(string path)
        {
            var document = await _context.Documents.Where(cd => cd.Path == path).FirstAsync();
            if (document == null)
                throw new InvalidOperationException($"there aren't documents with path = {path}");

            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Checks if the document exists based on the provided id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DocumentExists(Guid id)
        {
            if (_context.Documents == null || _context.Documents.Count() == 0)
                return false;

            return _context.Documents.Any(e => e.CarDealer == _carDealer && e.Id == id);
        }

        /// <summary>
        /// Checks if the document exists based on the provided path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool DocumentExists(string path)
        {
            if (_context.Documents == null || _context.Documents.Count() == 0)
                return false;

            return _context.Documents.Any(e => e.CarDealer == _carDealer && e.Path == path);
        }

        /// <summary>
        /// Gets the document bytes from the provided path.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="carDealer"></param>
        /// <returns></returns>
        public async Task<byte[]?> GetDocumentBytes(Guid id)
        {
            string? path = (await GetDocument(id))?.Path;

            if (string.IsNullOrEmpty(path))
                return null;

            return await GetDocumentBytesByPath(path);
        }

        /// <summary>
        /// Saves the document bytes to the provided path.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="carDealer"></param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task SaveDocumentBytes(Guid id, byte[] bytes)
        {
            string? path = (await GetDocument(id))?.Path;

            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("invalid document id");

            await SaveDocumentBytesByPath(path, bytes);
        }

        /// <summary>
        /// Gets the document bytes from the provided path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<byte[]?> GetDocumentBytesByPath(string path)
        {
            if (!File.Exists(path))
                return null;

            return await File.ReadAllBytesAsync(path);
        }

        /// <summary>
        /// Saves the document bytes to the provided path.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public async Task SaveDocumentBytesByPath(string path, byte[] bytes)
        {
            if (File.Exists(path))
                File.Delete(path);

            await File.WriteAllBytesAsync(path, bytes);
        }

        /// <summary>
        /// Creates a document path based on the provided document.
        /// </summary>
        /// <param name="document">The document for which the path is to be created.</param>
        /// <returns>The created document path.</returns>
        public string GetDocumentPath(Document document)
        {
            return Path.Combine(document.CarDealer.Name, document.FolderCategory.FolderName, document.DocumentTypeId.ToString(), document.Name);
        }
    }
}
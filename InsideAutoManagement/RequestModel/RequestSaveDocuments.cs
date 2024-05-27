using InsideAutoManagement.Model;

namespace InsideAutoManagement.RequestModel
{
    public class RequestSaveDocuments
    {
        public Document Document { get; set; } = null!;
        public byte[] DocumentBytes { get; set; } = null!;
    }
}

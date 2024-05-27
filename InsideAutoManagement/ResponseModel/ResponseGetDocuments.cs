using InsideAutoManagement.Model;

namespace InsideAutoManagement.ResponseModel
{
    public class ResponseGetDocuments
    {
        public Document Document { get; set; } = null!;
        public byte[] DocumentBytes { get; set; } = null!;
    }
}

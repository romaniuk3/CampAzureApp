namespace CampAzureApp.Services.Abstract
{
    public interface IDocumentService
    {
        void UploadDocumentToAzure(IFormFile file, string userEmail);
    }
}

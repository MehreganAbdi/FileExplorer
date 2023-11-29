using CloudinaryDotNet.Actions;

namespace FileExplorer.IService
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
        Task<bool> UploadFileToServer(IFormFile formFile);
    }
}

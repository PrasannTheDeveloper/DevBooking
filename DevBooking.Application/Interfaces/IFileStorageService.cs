namespace DevBooking.Application.Interfaces;

public interface IFileStorageService
{
    // Returns the URL/path to access the uploaded file
    Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType);
    Task DeleteFileAsync(string fileUrl);
}
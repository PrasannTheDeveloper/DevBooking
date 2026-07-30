using DevBooking.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace DevBooking.Infrastructure.Files;

public class LocalFileStorageService : IFileStorageService
{
    private readonly IWebHostEnvironment _environment;
    private const string UploadsFolder = "uploads/profile-images";

    public LocalFileStorageService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType)
    {
        var uniqueFileName = $"{Guid.NewGuid()}_{fileName}";
        var folderPath = Path.Combine(_environment.WebRootPath, UploadsFolder);

        Directory.CreateDirectory(folderPath); // no-op if it already exists

        var filePath = Path.Combine(folderPath, uniqueFileName);

        using (var fileOutput = new FileStream(filePath, FileMode.Create))
        {
            await fileStream.CopyToAsync(fileOutput);
        }

        // This is the relative URL the frontend will use to display the image
        return $"/{UploadsFolder}/{uniqueFileName}";
    }

    public Task DeleteFileAsync(string fileUrl)
    {
        var fileName = Path.GetFileName(fileUrl);
        var filePath = Path.Combine(_environment.WebRootPath, UploadsFolder, fileName);

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        return Task.CompletedTask;
    }
}
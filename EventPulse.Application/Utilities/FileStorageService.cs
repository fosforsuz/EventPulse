using EventPulse.Application.Interfaces;
using EventPulse.Application.Modals;
using Microsoft.Extensions.Options;

namespace EventPulse.Application.Utilities;

public class FileStorageService : IFileStorageService
{
    private readonly FileStorageSettings _fileStorageSettings;

    public FileStorageService(IOptions<FileStorageSettings> fileStorageSettings)
    {
        _fileStorageSettings =
            fileStorageSettings.Value ?? throw new ArgumentNullException(nameof(fileStorageSettings));
    }

    public string SaveFile(int eventId, Stream fileStream)
    {
        CheckDirectory(_fileStorageSettings.BasePath);
        var uniqueFileName = $"{eventId}_{Guid.NewGuid()}";
        var filePath = Path.Combine(_fileStorageSettings.BasePath, uniqueFileName);

        using (var file = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        {
            fileStream.CopyTo(file);
        }

        return Path.Combine(_fileStorageSettings.BasePath, uniqueFileName);
    }

    private void CheckDirectory(string directory)
    {
        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
    }
}
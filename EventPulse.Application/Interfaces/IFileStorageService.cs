using System;

namespace EventPulse.Application.Interfaces;

public interface IFileStorageService
{
    string SaveFile(int eventId, Stream fileStream);
}

using DigitalEducation.Entities;

namespace DigitalEducation.Interfaces;

public interface IImageFileStore
{
    public Task<ImageFile> SaveFileAsync(Stream fileStream, CancellationToken cancellationToken = default);

    public Task<byte[]> GetFileBytesAsync(int id, CancellationToken cancellationToken = default);
}
using System.Reflection;
using System.Text;
using DigitalEducation.Data;
using DigitalEducation.Entities;
using DigitalEducation.Interfaces;
using Saritasa.Tools.EFCore;

namespace DigitalEducation.Services;

public class LocalDriveImageFileStore : IImageFileStore
{
    private readonly ApplicationDbContext dbContext;
    private const string FilesDirectoryName = "files";

    public LocalDriveImageFileStore(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
        if (!Directory.Exists(FilesDirectoryName))
        {
            Directory.CreateDirectory(FilesDirectoryName);
        }
    }
    /// <summary>
    /// Saves uploaded file to the local drive.
    /// </summary>
    /// <param name="fileStream">Stream of file that is need to be uploaded.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Saved file's name</returns>
    public async Task<ImageFile> SaveFileAsync(Stream fileStream, CancellationToken cancellationToken = default)
    {
        var pictureFileName = Path.GetRandomFileName();
        await using var createdFileStream = File.Create(Path.Combine(FilesDirectoryName, pictureFileName));
        await fileStream.CopyToAsync(createdFileStream, cancellationToken);
        await createdFileStream.FlushAsync(cancellationToken);
        await createdFileStream.DisposeAsync();
        var imageFile = new ImageFile
        {
            Name = pictureFileName
        };
        await dbContext.AddAsync(imageFile, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return imageFile;
    }

    public async Task<byte[]> GetFileBytesAsync(int id, CancellationToken cancellationToken = default)
    {
        var imageFile = await dbContext.ImageFiles.GetAsync(i => i.Id == id, cancellationToken);
        return 
            await File.ReadAllBytesAsync(Path.Combine(FilesDirectoryName, imageFile.Name), cancellationToken);
        
    }
}
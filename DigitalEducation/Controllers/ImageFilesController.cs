using DigitalEducation.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DigitalEducation.Controllers;

public class ImageFilesController : Controller
{
    private readonly IImageFileStore imageFileStore;

    public ImageFilesController(IImageFileStore imageFileStore)
    {
        this.imageFileStore = imageFileStore;
    }

    public async Task<FileResult> GetImageByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var bytes = await imageFileStore.GetFileBytesAsync(id, cancellationToken);
        var base64String = Convert.ToBase64String(bytes);
        return File(Convert.FromBase64String(base64String), "image/jpg");
    }
}